using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TranslationManagement.Api.Interfaces;

namespace TranslationManagement.Api.Controlers
{
    [ApiController]
    [Route("api/TranslatorsManagement/[action]")]
    public class TranslatorManagementController : ControllerBase
    {
        private IRepository _repository;

        public TranslatorManagementController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<TranslatorModel>> GetTranslators()
        {
            return await _repository.GetTranslatorsAsync();
        }

        [HttpGet]
        public async Task<IActionResult> GetTranslatorsByName(string name)
        {
            var result = await _repository.GetTranslatorsByNameAsync(name);
            if (result.Any())
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddTranslator(TranslatorModel translator)
        {
            var result = await _repository.AddTranslatorAsync(translator);
            return CreatedAtAction(nameof(AddTranslator), new {id = translator.Id}, translator );
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTranslatorStatus(int Translator, string newStatus = "")
        {
            var result  = await _repository.UpdateTranslatorStatusAsync(Translator, newStatus);
            if(result)
            {
                return Ok();
            }
            else 
            {
                return BadRequest();
            }
        }
    }
}