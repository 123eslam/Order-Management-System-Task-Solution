namespace OrderManagementSystemTask.DAL.Entities
{
    public class Customer : ModelBase<int>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
