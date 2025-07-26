using OrderManagementSystemTask.DAL.Presistance.Repostories.CustomerRepostories;
using OrderManagementSystemTask.DAL.Presistance.Repostories.InvoiceRepositoies;
using OrderManagementSystemTask.DAL.Presistance.Repostories.OredrRepostories;
using OrderManagementSystemTask.DAL.Presistance.Repostories.ProductRepostories;

namespace OrderManagementSystemTask.DAL.Presistance.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IProductRepostory ProductRepostory { get; }
        public IOrderRepository OrderRepository { get; }
        public ICustomerRepostory CustomerRepostory { get; }
        public IInvoiceRepository InvoiceRepository { get; }
        Task<int> CompleteAsync();
    }
}
