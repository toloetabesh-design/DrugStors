using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DragStore.Domain.Entities;

namespace DragStore.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Drug> Drugs { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        internal void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }

    public class DbContextOptions<T>
    {
    }

    public class DbContext
    {
        public DbContext(DbContextOptions<AppDbContext> options)
        {
            Options = options;
        }

        public DbContextOptions<AppDbContext> Options { get; }
    }
}