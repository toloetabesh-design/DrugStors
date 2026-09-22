using DrugStore.Application.BusinessServices;
using DrugStore.Application.BusinessServices.Interfaces;
using DrugStore.Application.Profiles;
using DrugStore.Persistence.Repositories;
using DrugStore.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<DrugStore.Persistence.AppDbContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")
            ));


        // ۲. ثبت سرویس‌ها و ریپازیتوری‌ها
        builder.Services.AddControllers();
        builder.Services.AddScoped<IDrugRepository, DrugRepository>(); 
        builder.Services.AddAutoMapper(typeof(DrugProfile));
        builder.Services.AddScoped<IDrugService, DrugService>();
        builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
        builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
        
        // ۳. تنظیمات Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // ۴. تنظیمات Middleware
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
}

 