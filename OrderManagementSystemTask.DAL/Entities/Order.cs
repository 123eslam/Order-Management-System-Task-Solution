namespace OrderManagementSystemTask.DAL.Entities
{
    public class Order : ModelBase<int>
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public string PaymentMethod { get; set; }
        public int PaymentIntentId { get; set; }
        public string Status { get; set; } = "Pending";
    }
}
