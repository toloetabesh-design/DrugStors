using DrugStore.Application.BusinessServices;
using DrugStore.Application.BusinessServices.Interfaces;
using DrugStore.Application.Profiles;
using DrugStore.Persistence.Repositories;
using DrugStore.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using DrugStore.Persistence; 

var logger = LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddScoped<IDrugRepository, DrugRepository>();
    builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
    builder.Services.AddScoped<IOrderRepository, OrderRepository>();

    builder.Services.AddScoped<IDrugService, DrugService>();
    builder.Services.AddScoped<ICustomerService, CustomerService>();
    builder.Services.AddScoped<IOrderService, OrderService>();

    builder.Services.AddAutoMapper(typeof(DrugProfile));

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<AppDbContext>();

            context.Database.EnsureCreated();

            // فراخوانی کلاس Seed (باید این کلاس را در لایه Persistence ساخته باشید)
            DrugStore.Persistence.DbInitializer.Seed(context);

            Console.WriteLine("Database seeded successfully!");
        }
        catch (Exception ex)
        {
            var errorLogger = services.GetRequiredService<ILogger<Program>>();
            errorLogger.LogError(ex, "An error occurred while seeding the database.");
        }
    }
    // ==========================================================

    // ۷. تنظیمات Middleware
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
