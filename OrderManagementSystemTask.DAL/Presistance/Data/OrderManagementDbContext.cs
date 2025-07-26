using Microsoft.EntityFrameworkCore;

namespace OrderManagementSystemTask.DAL.Presistance.Data
{
    public class OrderManagementDbContext : DbContext
    {
        public OrderManagementDbContext(DbContextOptions<OrderManagementDbContext> options)
             : base(options)
        {
        }
    }
}
