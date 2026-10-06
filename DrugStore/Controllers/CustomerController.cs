using DrugStore.Application.BusinessServices.Interfaces;
using DrugStore.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // اضافه کردن این برای ILogger

namespace DrugStore.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        // ۱. تعریف لاگر اختصاصی برای این کنترلر
        private readonly ILogger<CustomerController> _logger;

        // ۲. دریافت لاگر از طریق سازنده (Dependency Injection)
        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("درخواست دریافت همه مشتریان دریافت شد.");
            var result = _customerService.Get();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            _logger.LogInformation("درخواست دریافت مشتری با آی‌دی: {Id}", id);
            try
            {
                var result = _customerService.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در هنگام یافتن مشتری با آی‌دی: {Id}", id);
                return NotFound($"مشتری با آی‌دی {id} یافت نشد.");
            }
        }

        [HttpPost]
        public IActionResult Insert(CustomerDto customer)
        {
            try
            {
                _logger.LogInformation("درخواست ثبت مشتری جدید: {@Customer}", customer);
                _customerService.Insert(customer);
                _logger.LogInformation("مشتری با موفقیت ثبت شد.");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در هنگام ثبت مشتری جدید: {@Customer}", customer);
                return BadRequest("خطا در ثبت اطلاعات مشتری.");
            }
        }

        [HttpPut]
        public IActionResult Update(CustomerDto customer)
        {
            try
            {
                _logger.LogInformation("درخواست بروزرسانی مشتری با آی‌دی: {Id}", customer.Id);
                _customerService.Update(customer);
                _logger.LogInformation("اطلاعات مشتری با موفقیت بروزرسانی شد.");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در هنگام بروزرسانی مشتری با آی‌دی: {Id}", customer.Id);
                return BadRequest("خطا در بروزرسانی اطلاعات.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _logger.LogInformation("درخواست حذف مشتری با آی‌دی: {Id}", id);
                _customerService.Delete(id);
                _logger.LogInformation("مشتری با آی‌دی {Id} با موفقیت حذف شد.", id);
                return Ok();
            }
            catch (Exception ex)
            {
                // اینجا همان جایی است که قبلاً با خطای ۵00 مواجه می‌شدی. 
                // حالا با این لاگ، دلیل دقیق خطا (مثلاً Foreign Key) در فایل Log ذخیره می‌شود.
                _logger.LogError(ex, "خطای بحرانی در حذف مشتری با آی‌دی: {Id}", id);
                return StatusCode(500, "خطای داخلی سرور در هنگام حذف.");
            }
        }
    }
}
