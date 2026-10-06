using DrugStore.Domain.Entities; // یا هرجایی که موجودیت‌های شماست
using DrugStore.Persistence;
using System.Linq;

namespace DrugStore.Persistence
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            // ۱. بررسی می‌کنیم آیا دیتابیس خالی است یا نه
            if (context.Customers.Any()) return; // اگر دیتابیس دیتا دارد، چیزی اضافه نکن

            // ۲. تعریف داده‌های اولیه برای کاستومر
            var customers = new[]
            {
                new Customer { FullName = "علی احمدی", Email = "ali@test.com" },
                new Customer { FullName = "سارا محمدی", Email = "sara@test.com" }
            };
            context.Customers.AddRange(customers);

            // ۳. تعریف داده‌های اولیه برای دارو
            var drugs = new[]
            {
                new Drug { Name = "استامینوفن", Price = 10000 },
                new Drug { Name = "آموکسی‌سیلین", Price = 25000 }
            };
            context.Drugs.AddRange(drugs);

            // ۴. ذخیره تغییرات
            context.SaveChanges();
        }
    }
}
