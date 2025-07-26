using Microsoft.EntityFrameworkCore;
using OrderManagementSystemTask.DAL.Entities;
using OrderManagementSystemTask.DAL.Presistance.Data;
using OrderManagementSystemTask.DAL.Presistance.Repostories._Generic;

namespace OrderManagementSystemTask.DAL.Presistance.Repostories.OredrRepostories
{
    public class OrderRepository : GenericRepository<Order, int> , IOrderRepository
    {
        public OrderRepository(OrderManagementDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Order>> GetAllCustomerOrderAsync(int CustomerId)
        {
            return await _dbContext.Orders
                .Where(o => o.CustomerId == CustomerId)
                .ToListAsync();
        }
    }
}
