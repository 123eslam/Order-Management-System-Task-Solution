namespace OrderManagementSystemTask.BLL.Dtos.OrderDto
{
    public class OrderRequestDto
    {
        public int CustomerId { get; set; }
        public string PaymentMethod { get; set; }
        public ICollection<OrderItemRequestDto> OrderItems { get; set; } = new List<OrderItemRequestDto>();
    }
}
