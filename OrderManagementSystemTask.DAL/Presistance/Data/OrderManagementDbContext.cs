using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystemTask.DAL.Entities;

namespace OrderManagementSystemTask.DAL.Presistance.Data
{
    public class OrderManagementDbContext : IdentityDbContext<User>
    {
        public OrderManagementDbContext(DbContextOptions<OrderManagementDbContext> options)
             : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
    }
}
