using OrderManagementSystemTask.DAL.Entities;
using OrderManagementSystemTask.DAL.Presistance.Data;
using OrderManagementSystemTask.DAL.Presistance.Repostories._Generic;

namespace OrderManagementSystemTask.DAL.Presistance.Repostories.InvoiceRepositoies
{
    public class InvoiceRepository : GenericRepository<Invoice, int>, IInvoiceRepository
    {
        public InvoiceRepository(OrderManagementDbContext dbContext) : base(dbContext)
        {
        }
    }
}
