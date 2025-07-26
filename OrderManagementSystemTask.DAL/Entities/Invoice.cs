namespace OrderManagementSystemTask.DAL.Entities
{
    public class Invoice : ModelBase<int>
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime InvoiceDate { get; set; }
    }
}
