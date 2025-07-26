namespace OrderManagementSystemTask.DAL.Entities
{
    public class Product : ModelBase<int>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
