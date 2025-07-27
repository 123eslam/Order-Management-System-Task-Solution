using OrderManagementSystemTask.BLL.Dtos.CustomerDto;
using OrderManagementSystemTask.BLL.Dtos.OrderDto;

namespace OrderManagementSystemTask.BLL.Services.CustomerServices
{
    public interface ICustomerService
    {
        Task<CustomerResultDto> CreateCustomerAsync(CustomerResultDto customerDto);
        Task<IEnumerable<OrderResultDto>> GetAllCustomerOrders(int customerId);
    }
}
