namespace OrderManagementSystemTask.BLL.Dtos.OrderDto
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ProductName { get; set; }
        public decimal Discount { get; set; }
    }
}
