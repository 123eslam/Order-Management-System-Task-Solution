using Microsoft.EntityFrameworkCore;
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
        public async Task<IEnumerable<Invoice>> GetAllInvoicesWithDetailsAsync()
        {
            return await _dbContext.Invoices
                .Include(i => i.Order)
                    .ThenInclude(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                .Include(i => i.Order)
                    .ThenInclude(o => o.Customer)
                .ToListAsync();
        }
        public async Task<Invoice?> GetInvoiceWithDetailsByIdAsync(int id)
        {
            return await _dbContext.Invoices
                .Include(i => i.Order)
                    .ThenInclude(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                .Include(i => i.Order)
                    .ThenInclude(o => o.Customer)
                .FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
