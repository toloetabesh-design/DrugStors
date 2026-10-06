using DrugStore.Application.BusinessServices.Interfaces;
using DrugStore.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // اضافه شد

namespace DrugStore.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugController : ControllerBase
    {
        private readonly IDrugService _drugService;
        // ۱. تعریف لاگر
        private readonly ILogger<DrugController> _logger;

        // ۲. تزریق لاگر از طریق سازنده
        public DrugController(IDrugService drugService, ILogger<DrugController> logger)
        {
            _drugService = drugService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("درخواست دریافت لیست تمام داروها.");
            return Ok(_drugService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            _logger.LogInformation("درخواست دریافت اطلاعات دارو با آی‌دی: {Id}", id);
            try
            {
                var result = _drugService.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در یافتن دارو با آی‌دی: {Id}", id);
                return NotFound($"دارو با مشخصات مورد نظر یافت نشد.");
            }
        }

        [HttpPost]
        public IActionResult Insert(DrugDto drug)
        {
            try
            {
                _logger.LogInformation("درخواست ثبت داروی جدید: {@Drug}", drug);
                _drugService.Insert(drug);
                _logger.LogInformation("دارو با موفقیت ثبت گردید.");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در هنگام ثبت داروی جدید: {@Drug}", drug);
                return BadRequest("خطا در ثبت اطلاعات دارو.");
            }
        }

        [HttpPut]
        public IActionResult Update(DrugDto drug)
        {
            try
            {
                _logger.LogInformation("درخواست بروزرسانی دارو با آی‌دی: {Id}", drug.Id);
                _drugService.Update(drug);
                _logger.LogInformation("اطلاعات دارو با موفقیت بروزرسانی شد.");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در بروزرسانی دارو با آی‌دی: {Id}", drug.Id);
                return BadRequest("خطا در بروزرسانی اطلاعات دارو.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _logger.LogInformation("درخواست حذف دارو با آی‌دی: {Id}", id);
                _drugService.Delete(id);
                _logger.LogInformation("دارو با آی‌دی {Id} حذف شد.", id);
                return Ok();
            }
            catch (Exception ex)
            {
                // این بخش برای پیدا کردن دلیل خطای ۵00 (مثل رابطه کلید خارجی) حیاتی است
                _logger.LogError(ex, "خطای بحرانی در حذف دارو با آی‌دی: {Id}", id);
                return StatusCode(500, "خطای داخلی سرور در عملیات حذف.");
            }
        }
    }
}
