using DrugStore.Application.BusinessServices.Interfaces;
using DrugStore.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // اضافه شد

namespace DrugStore.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        // ۱. تعریف لاگر
        private readonly ILogger<OrderController> _logger;

        // ۲. تزریق لاگر از طریق سازنده
        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("درخواست دریافت لیست تمام سفارش‌ها.");
            return Ok(_orderService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            _logger.LogInformation("درخواست دریافت سفارش با آی‌دی: {Id}", id);
            try
            {
                var result = _orderService.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در یافتن سفارش با آی‌دی: {Id}", id);
                return NotFound($"سفارش با آی‌دی {id} یافت نشد.");
            }
        }

        [HttpPost]
        public IActionResult Insert(OrderDto order)
        {
            try
            {
                _logger.LogInformation("درخواست ثبت سفارش جدید: {@Order}", order);
                _orderService.Insert(order);
                _logger.LogInformation("سفارش جدید با موفقیت ثبت شد.");
                return Ok();
            }
            catch (Exception ex)
            {
                // اینجا اگر مثلاً ID مشتری اشتباه باشد، دلیل دقیق در لاگ ثبت می‌شود
                _logger.LogError(ex, "خطا در ثبت سفارش جدید: {@Order}", order);
                return BadRequest("خطا در ثبت سفارش. لطفا اطلاعات را بررسی کنید.");
            }
        }

        [HttpPut]
        public IActionResult Update(OrderDto order)
        {
            try
            {
                _logger.LogInformation("درخواست بروزرسانی سفارش با آی‌دی: {Id}", order.Id);
                _orderService.Update(order);
                _logger.LogInformation("سفارش با آی‌دی {Id} با موفقیت بروزرسانی شد.", order.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در بروزرسانی سفارش با آی‌دی: {Id}", order.Id);
                return BadRequest("خطا در بروزرسانی اطلاعات سفارش.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _logger.LogInformation("درخواست حذف سفارش با آی‌دی: {Id}", id);
                _orderService.Delete(id);
                _logger.LogInformation("سفارش با آی‌دی {Id} حذف شد.", id);
                return Ok();
            }
            catch (Exception ex)
            {
                // در سفارش‌ها، احتمال خطای Foreign Key بسیار بالا است (مثلاً سفارش متصل به یک محصول است)
                _logger.LogError(ex, "خطای بحرانی در حذف سفارش با آی‌دی: {Id}", id);
                return StatusCode(500, "خطا در عملیات حذف سفارش. ممکن است این سفارش با داده‌های دیگر در ارتباط باشد.");
            }
        }
    }
}
