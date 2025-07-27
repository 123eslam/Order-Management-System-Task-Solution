using OrderManagementSystemTask.BLL.Dtos.InvoiceDtos;

namespace OrderManagementSystemTask.BLL.Services.InvoiceServices
{
    public interface IInvoiceService
    {
        Task<InvoiceResultDto> GetInvoiceByIdAsync(int id);
        Task<IEnumerable<InvoiceResultDto>> GetAllInvoicesAsync();
    }
}
