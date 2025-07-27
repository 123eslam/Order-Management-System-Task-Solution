using OrderManagementSystemTask.DAL.Entities;
using OrderManagementSystemTask.DAL.Presistance.Repostories._Generic;

namespace OrderManagementSystemTask.DAL.Presistance.Repostories.OredrRepostories
{
    public interface IOrderRepository : IGenericRepository<Order ,int>
    {
        Task<IEnumerable<Order>> GetAllCustomerOrderAsync(int CustomerId);
        Task<IEnumerable<Order>> GetAllOrdersWithDetailsAsync();
        Task<Order?> GetOrderWithDetailsByIdAsync(int id);
    }
}
