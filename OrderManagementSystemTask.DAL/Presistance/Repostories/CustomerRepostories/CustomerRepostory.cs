using OrderManagementSystemTask.DAL.Entities;
using OrderManagementSystemTask.DAL.Presistance.Data;

namespace OrderManagementSystemTask.DAL.Presistance.Repostories.CustomerRepostories
{
    public class CustomerRepostory : ICustomerRepostory
    {
        private protected readonly OrderManagementDbContext _dbContext;

        public CustomerRepostory(OrderManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Customer entity)
        {
            _dbContext.Customers.Add(entity);
        }
    }
}
