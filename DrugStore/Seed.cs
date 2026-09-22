using Microsoft.EntityFrameworkCore;
using DrugStore.Domain.Entities;

public class Seed
{
    public static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Drug>().HasData(
            new Drug { Id = 1, Name = "استامینوفن", Price = 50000 },
            new Drug { Id = 2, Name = "ایبوپروفن", Price = 70000 },
            new Drug { Id = 3, Name = "آموکسی‌سیلین", Price = 90000 }
        );
    }
}