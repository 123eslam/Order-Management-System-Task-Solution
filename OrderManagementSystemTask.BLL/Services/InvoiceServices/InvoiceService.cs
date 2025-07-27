using OrderManagementSystemTask.BLL.Dtos.ErrorDtos;
using OrderManagementSystemTask.BLL.Dtos.InvoiceDtos;
using OrderManagementSystemTask.BLL.Dtos.OrderDto;
using OrderManagementSystemTask.DAL.Presistance.UnitOfWork;

namespace OrderManagementSystemTask.BLL.Services.InvoiceServices
{
    public class InvoiceService(IUnitOfWork unitOfWork) : IInvoiceService
    {
        public async Task<IEnumerable<InvoiceResultDto>> GetAllInvoicesAsync()
        {
            var invoices = await unitOfWork.InvoiceRepository.GetAllInvoicesWithDetailsAsync();
            return invoices.Select(i => new InvoiceResultDto
            {
                Id = i.Id,
                TotalAmount = i.TotalAmount,
                InvoiceDate = i.InvoiceDate,
                Order = new OrderResultDto
                {
                    Id = i.Order.Id,
                    CustomerName = i.Order.Customer.Name,
                    OrderDate = i.Order.OrderDate,
                    PaymentMethod = i.Order.PaymentMethod,
                    Status = i.Order.Status,
                    CustomerEmail = i.Order.Customer.Email,
                    OrderItems = i.Order.OrderItems.Select(oi => new OrderItemDto
                    {
                        Id = oi.Id,
                        ProductName = oi.Product.Name,
                        Quantity = oi.Quantity,
                        Price = oi.UnitPrice
                    }).ToList()
                },
            });
        }

        public async Task<InvoiceResultDto> GetInvoiceByIdAsync(int id)
        {
            var invoice = await unitOfWork.InvoiceRepository.GetInvoiceWithDetailsByIdAsync(id);
            if (invoice == null)
            {
                new NotFoundException($"Invoice with ID {id} not found.");
            }
            return new InvoiceResultDto
            {
                Id = invoice.Id,
                TotalAmount = invoice.TotalAmount,
                InvoiceDate = invoice.InvoiceDate,
                Order = new OrderResultDto
                {
                    Id = invoice.Order.Id,
                    CustomerName = invoice.Order.Customer.Name,
                    OrderDate = invoice.Order.OrderDate,
                    PaymentMethod = invoice.Order.PaymentMethod,
                    Status = invoice.Order.Status,
                    CustomerEmail = invoice.Order.Customer.Email,
                    OrderItems = invoice.Order.OrderItems.Select(oi => new OrderItemDto
                    {
                        Id = oi.Id,
                        ProductName = oi.Product.Name,
                        Quantity = oi.Quantity,
                        Price = oi.UnitPrice
                    }).ToList()
                },
            };
        }
    }
}
