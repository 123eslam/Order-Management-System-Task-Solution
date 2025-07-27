using OrderManagementSystemTask.BLL.Dtos.OrderDto;

namespace OrderManagementSystemTask.BLL.Dtos.InvoiceDtos
{
    public class InvoiceResultDto
    {
        public int Id { get; set; }
        public OrderResultDto Order { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime InvoiceDate { get; set; }
    }
}
