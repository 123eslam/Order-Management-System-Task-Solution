using OrderManagementSystemTask.DAL.Presistance.Data;
using OrderManagementSystemTask.DAL.Presistance.Repostories.CustomerRepostories;
using OrderManagementSystemTask.DAL.Presistance.Repostories.InvoiceRepositoies;
using OrderManagementSystemTask.DAL.Presistance.Repostories.OredrRepostories;
using OrderManagementSystemTask.DAL.Presistance.Repostories.ProductRepostories;

namespace OrderManagementSystemTask.DAL.Presistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrderManagementDbContext _dbContext;

        public UnitOfWork(OrderManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IProductRepostory ProductRepostory => new ProductRepostory(_dbContext);

        public IOrderRepository OrderRepository => new OrderRepository(_dbContext);

        public ICustomerRepostory CustomerRepostory => new CustomerRepostory(_dbContext);

        public IInvoiceRepository InvoiceRepository => new InvoiceRepository(_dbContext);

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }
    }
}
