using OrderManagementSystemTask.DAL.Entities;
using OrderManagementSystemTask.DAL.Presistance.Repostories._Generic;

namespace OrderManagementSystemTask.DAL.Presistance.Repostories.InvoiceRepositoies
{
    public interface IInvoiceRepository : IGenericRepository<Invoice, int>
    {
    }
}
