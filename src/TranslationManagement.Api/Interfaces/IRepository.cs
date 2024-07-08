using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TranslationManagement.Api.Interfaces
{
    public interface IRepository
    {
        Task<IEnumerable<TranslationJob>> GetJobsAsync();
        Task<bool> SetPriceAsync(TranslationJob job);
        Task<TranslationJob> CreateJobAsync(TranslationJob job);
        Task<TranslationJob> CreateJobWithFileAsync(IFormFile file, string customer);
        Task<bool> UpdateJobStatusAsync(int jobId, int translatorId, string newStatus = "");
        Task<IEnumerable<TranslatorModel>> GetTranslatorsAsync();
        Task<TranslatorModel> AddTranslatorAsync(TranslatorModel translator);
        Task<bool> UpdateTranslatorStatusAsync(int Translator, string newStatus = "");
        Task<IEnumerable<TranslatorModel>> GetTranslatorsByNameAsync(string name);
    }
}

