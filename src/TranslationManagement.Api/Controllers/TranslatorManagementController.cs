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
        public IEnumerable<TranslatorModel> GetTranslators()
        {
            return _repository.GetTranslators();
        }

        [HttpGet]
        public IActionResult GetTranslatorsByName(string name)
        {
            var result =  _repository.GetTranslatorsByName(name);
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
        public IActionResult UpdateTranslatorStatus(int Translator, string newStatus = "")
        {
            var result  = _repository.UpdateTranslatorStatus(Translator, newStatus);
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