using DrugStore.Application.BusinessServices.Interfaces;
using DrugStore.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugController : ControllerBase
    {
        private readonly IDrugService _drugService;

        public DrugController(IDrugService drugService)
        {
            _drugService = drugService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_drugService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_drugService.GetById(id));
        }

        [HttpPost]
        public IActionResult Insert(DrugDto drug)
        {
            _drugService.Insert(drug);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(DrugDto drug)
        {
            _drugService.Update(drug);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _drugService.Delete(id);
            return Ok();
        }
    }
}