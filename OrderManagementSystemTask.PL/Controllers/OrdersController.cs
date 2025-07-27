using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystemTask.BLL.Dtos.OrderDto;
using OrderManagementSystemTask.BLL.Services.OrderServices;
using System.Net;

namespace OrderManagementSystemTask.PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController(IOrderService orderService) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<OrderResultDto>> CreateOrder(OrderRequestDto orderRequest)
        {
            var order = await orderService.CreateOrderAsync(orderRequest);
            return Ok(order);
        }
        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderResultDto>> GetOrderById(int orderId)
        {
            var order = await orderService.GetOrderByIdAsync(orderId);
            return Ok(order);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<OrderResultDto>>> GetAllOrders()
        {
            var orders = await orderService.GetAllOrdersAsync();
            return Ok(orders);
        }
        [HttpPut("{orderId}/status")]
        [Authorize(Roles = "Admin")] 
        [ProducesResponseType(typeof(OrderResultDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<OrderResultDto>> UpdateOrderStatus(int orderId, string newStatus)
        {
            var updatedOrder = await orderService.UpdateOrderStatusAsync(orderId, newStatus);
            return Ok(updatedOrder);
        }
    }
}
