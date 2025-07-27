namespace OrderManagementSystemTask.BLL.Dtos.ProductDto
{
    public class CreateOrUpdateProductDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
