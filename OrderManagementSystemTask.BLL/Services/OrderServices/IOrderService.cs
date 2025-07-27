using OrderManagementSystemTask.BLL.Dtos.OrderDto;

namespace OrderManagementSystemTask.BLL.Services.OrderServices
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResultDto>> GetAllOrdersAsync();
        Task<OrderResultDto> GetOrderByIdAsync(int orderId);
        Task<OrderResultDto> CreateOrderAsync(OrderRequestDto orderRequest);
        Task<OrderResultDto> UpdateOrderStatusAsync(int orderId, string newStatus);
    }
}
