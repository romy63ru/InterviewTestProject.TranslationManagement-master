using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TranslationManagement.Api.Interfaces;

namespace TranslationManagement.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/jobs/[action]")]
    public class TranslationJobController : ControllerBase
    {

        private readonly IRepository _repository;

        public TranslationJobController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<TranslationJob>> GetJobs()
        {
            return await _repository.GetJobsAsync();
        }

        [HttpPost]
        public async Task<IActionResult> CreateJob(TranslationJob job)
        {
            var result = await _repository.CreateJobAsync(job);
            if (result != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Problem with external service");
            }
        }
        
        [HttpPost]
        public async Task<TranslationJob> CreateJobWithFile(IFormFile file, string customer)
        {
            return await _repository.CreateJobWithFileAsync(file, customer);
        }

        [HttpPost]
        public async Task<bool> UpdateJobStatus(int jobId, int translatorId, string newStatus = "")
        {
            return await _repository.UpdateJobStatusAsync(jobId, translatorId, newStatus);
        }
    }
}