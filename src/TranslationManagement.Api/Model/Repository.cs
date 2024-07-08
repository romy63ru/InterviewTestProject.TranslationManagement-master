using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using External.ThirdParty.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TranslationManagement.Api.Interfaces;
using TranslationManagement.Api.Model;

namespace TranslationManagement.Api
{
    public partial class Repository : IRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<Repository> _logger;

        private readonly INotificationServiceProvider _notificationServiceProvideer;
        private readonly IBusinessRules _businessRules;

        public Repository(IServiceScopeFactory scopeFactory, ILogger<Repository> logger, INotificationServiceProvider notificationServiceProvider, IBusinessRules businessRules)
        {
            _context = scopeFactory.CreateScope().ServiceProvider.GetService<AppDbContext>();
            _logger = logger;
            _notificationServiceProvideer = notificationServiceProvider;
            _businessRules = businessRules;
        }

        private async Task<bool> TrySendNotificationAsync(string message)
        {
            return await _notificationServiceProvideer.SendNotificationWithRetryAsync(message);
        }

        public async Task<TranslatorModel> AddTranslatorAsync(TranslatorModel translator)
        {
            _context.Translators.Add(translator);
            await _context.SaveChangesAsync();
            return translator;
        }
    
        public async Task<TranslationJob> CreateJobWithFileAsync(IFormFile file, string customer)
        {
            var reader = new StreamReader(file.OpenReadStream());
            string content;
    
            //todo extract to separate service
    
            if (file.FileName.EndsWith(".txt"))
            {
                content = reader.ReadToEnd();
            }
            else if (file.FileName.EndsWith(".xml"))
            {
                var xdoc = XDocument.Parse(reader.ReadToEnd());
                content = xdoc.Root.Element("Content").Value;
                customer = xdoc.Root.Element("Customer").Value.Trim();
            }
            else
            {
                throw new NotSupportedException("unsupported file");
            }
    
            var newJob = new TranslationJob()
            {
                OriginalContent = content,
                TranslatedContent = "",
                CustomerName = customer,
            };
    
            await SetPriceAsync(newJob);
    
            await CreateJobAsync(newJob); 
    
            return newJob; 
        }

        public async Task<IEnumerable<TranslationJob>> GetJobsAsync()
        {
            return await _context.TranslationJobs.ToListAsync<TranslationJob>();
        }

        public async Task<IEnumerable<TranslatorModel>> GetTranslatorsAsync()
        {
            return await _context.Translators.ToListAsync<TranslatorModel>();
        }

        const double PricePerCharacter = 0.01;
        public async Task<bool> SetPriceAsync (TranslationJob job)
        {
            var updateJob = await _context.TranslationJobs.FirstOrDefaultAsync(x => x.Id == job.Id);
            if (updateJob != null)
            {
                updateJob.Price = job.OriginalContent.Length * PricePerCharacter;
                return await _context.SaveChangesAsync() > 0;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateJobStatusAsync(int jobId, int translatorId, string newStatus = "")
        {
            _logger.LogInformation("Job status update request received: " + newStatus + " for job " + jobId.ToString() + " by translator " + translatorId);
            if (typeof(JobStatuses).GetProperties().Count(prop => prop.Name == newStatus) == 0)
            {
                return false;
            }

            var job = await _context.TranslationJobs.SingleAsync(j => j.Id == jobId);
            var translator = await _context.Translators.SingleAsync(t => t.Id == translatorId);

            if (!_businessRules.IsValid(job, translator, newStatus))
            {
                return false;
            }

            job.Status = newStatus;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateTranslatorStatusAsync(int translatorId, string newStatus = "")
        {
            _logger.LogInformation("User status update request: " + newStatus + " for user " + translatorId.ToString());
            if (!Enum.IsDefined(typeof(TranslatorStatus), newStatus))
            {
                   _logger.LogInformation("Unknown status: " + newStatus);
                   return false;
            }

            var translator = await _context.Translators.SingleAsync(t => t.Id == translatorId);
            translator.Status = (TranslatorStatus)Enum.Parse(typeof(TranslatorStatus), newStatus);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<TranslatorModel>> GetTranslatorsByNameAsync(string name)
        {
            return await _context.Translators.Where(t => t.Name == name).ToListAsync<TranslatorModel>();
        }

        public async Task<TranslationJob> CreateJobAsync(TranslationJob job)
        {
            job.Status = "New";
            await SetPriceAsync(job);
            _context.TranslationJobs.Add(job);
            bool success = _context.SaveChanges() > 0;
            if (success)
            {
                if (await TrySendNotificationAsync("Job created: " + job.Id))
                {
                    _logger.LogInformation("New job notification sent");
                    return job;
                }
                else
                {
                    _logger.LogInformation("Problem with external service");
                    return null;
                }
            }
            return null;
        }


    }
}
