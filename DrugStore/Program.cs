using DrugStore.Application.BusinessServices;
using DrugStore.Application.BusinessServices.Interfaces;
using DrugStore.Application.Profiles;
using DrugStore.Persistence.Repositories;
using DrugStore.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;


var logger = LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    // ۲. پیکربندی NLog برای ASP.NET Core
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // ۳. تنظیمات دیتابیس (SQL Server)
    builder.Services.AddDbContext<DrugStore.Persistence.AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    // ۴. ثبت ریپازیتوری‌ها (Persistence Layer)
    // ثبت تمامی ریپازیتوری‌های مربوط به Drug, Customer, Order و Agent
    builder.Services.AddScoped<IDrugRepository, DrugRepository>();
    builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
    builder.Services.AddScoped<IOrderRepository, OrderRepository>();
    
    // ۵. ثبت سرویس‌ها (Application Layer)
    // ثبت تمامی سرویس‌های مربوط به Drug, Customer, Order و Agent
    builder.Services.AddScoped<IDrugService, DrugService>();
    builder.Services.AddScoped<ICustomerService, CustomerService>();
    builder.Services.AddScoped<IOrderService, OrderService>();
    

    // ۶. ثبت AutoMapper (برای تبدیل Entity به DTO)
    builder.Services.AddAutoMapper(typeof(DrugProfile));

    // ۷. تنظیمات Swagger برای تست API
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // ۸. تنظیمات Middleware (ترتیب اجرا بسیار حیاتی است)
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Application terminated unexpectedly during startup");
    throw;
}
finally
{

    LogManager.Shutdown();
}

