using DrugStore.Application.BusinessServices.Interfaces;
using DrugStore.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_customerService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_customerService.GetById(id));
        }

        [HttpPost]
        public IActionResult Insert(CustomerDto customer)
        {
            _customerService.Insert(customer);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(CustomerDto customer)
        {
            _customerService.Update(customer);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _customerService.Delete(id);
            return Ok();
        }
    }
}