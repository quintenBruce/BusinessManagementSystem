using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Security.Claims;
using System.Security.Policy;

namespace InventoryManagementSystem.Models
{
    public class OrdersContext : IdentityDbContext
    {
        public OrdersContext(DbContextOptions<OrdersContext> options): base(options)
        { }

        public OrdersContext()
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=inventory_management;Integrated Security=True;").LogTo(Console.WriteLine);
        }
    }
}




















