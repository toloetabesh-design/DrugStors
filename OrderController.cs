using DrugStore.Application.BusinessServices.Interfaces;
using DrugStore.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_orderService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_orderService.GetById(id));
        }

        [HttpPost]
        public IActionResult Insert(OrderDto order)
        {
            _orderService.Insert(order);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(OrderDto order)
        {
            _orderService.Update(order);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _orderService.Delete(id);
            return Ok();
        }
    }
}