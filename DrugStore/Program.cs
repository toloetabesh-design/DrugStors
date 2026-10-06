using DrugStore.Application.BusinessServices;
using DrugStore.Application.BusinessServices.Interfaces;
using DrugStore.Application.Profiles;
using DrugStore.Persistence.Repositories;
using DrugStore.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.Web;
using System;

public class Program
{
    // ایجاد Logger برای ثبت خطاهای احتمالی در لحظه شروع برنامه (قبل از ساخت Builder)
    private static readonly NLog.Logger Logger = LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();

    public static void Main(string[] args)
    {
        // شروع بلاک Try برای مدیریت خطاهای بحرانی در هنگام بالا آمدن اپلیکیشن
        try
        {
            var builder = WebApplication.CreateBuilder(args);

            // --- تنظیمات NLog برای ASP.NET Core ---
            builder.Logging.ClearProviders(); // حذف لاگرهای پیش‌فرض مایکروسافت
            builder.Host.UseNLog();           // معرفی NLog به عنوان لاگر اصلی

            // --- تنظیمات دیتابیس ---
            builder.Services.AddDbContext<DrugStore.Persistence.AppDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                ));

            // --- ثبت سرویس‌ها و ریپازیتوری‌ها (Dependency Injection) ---
            builder.Services.AddControllers();

            // ثبت ریپازیتوری‌ها
            builder.Services.AddScoped<IDrugRepository, DrugRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

            // ثبت سرویس‌ها
            builder.Services.AddScoped<IDrugService, DrugService>();

            // ثبت AutoMapper
            builder.Services.AddAutoMapper(typeof(DrugProfile));

            // --- تنظیمات Swagger ---
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // --- تنظیمات Middleware (ترتیب قرارگیری بسیار مهم است) ---

            // ۱. لاگ کردن درخواست‌های HTTP (اگر در nlog.config تنظیم کرده باشی)

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            // اجرای اپلیکیشن
            app.Run();
        }
        catch (Exception exception)
        {
            // اگر برنامه در هنگام شروع با خطا مواجه شد (مثلاً خطای دیتابیس)، آن را لاگ کن
            Logger.Error(exception, "Application terminated unexpectedly during startup");
            throw; // پرتاب مجدد خطا برای مشاهده در کنسول
        }
        finally
        {
            // اطمینان از اینکه تمام لاگ‌ها قبل از بسته شدن کامل برنامه در فایل ذخیره می‌شوند
            LogManager.Shutdown();
        }
    }
}
