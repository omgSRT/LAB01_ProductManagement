using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DB"));
            //other way
            //optionsBuilder.UseSqlServer(GetConnectionString());
        }
        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            var strConn = config["ConnectionStrings:DB"];

            return strConn;
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Beverages"},
                new Category { CategoryId = 2, CategoryName = "Condiments"},
                new Category { CategoryId = 3, CategoryName = "Confections"},
                new Category { CategoryId = 4, CategoryName = "Dairy Products"},
                new Category { CategoryId = 5, CategoryName = "Grains/Cereal"},
                new Category { CategoryId = 6, CategoryName = "Meat/Poultry"},
                new Category { CategoryId = 7, CategoryName = "Produce"},
                new Category { CategoryId = 8, CategoryName = "Seafood"}
            );
        }
    }
}
