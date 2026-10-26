using DrugStore.Domain.Entities; 
using DrugStore.Persistence;
using System.Linq;

namespace DrugStore.Persistence
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            if (context.Customers.Any()) return;

            var customers = new[]
            {
                new Customer { FullName = "علی احمدی", Email = "ali@test.com" },
                new Customer { FullName = "سارا محمدی", Email = "sara@test.com" }
            };
            context.Customers.AddRange(customers);

            var drugs = new[]
            {
                new Drug { Name = "استامینوفن", Price = 10000 },
                new Drug { Name = "آموکسی‌سیلین", Price = 25000 }
            };
            context.Drugs.AddRange(drugs);

            context.SaveChanges();
        }
    }
}
