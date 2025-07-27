using OrderManagementSystemTask.DAL.Entities;

namespace OrderManagementSystemTask.DAL.Presistance.Repostories.CustomerRepostories
{
    public interface ICustomerRepostory 
    {
        void Add(Customer entity);
        Task<Customer?> GetByIdAsync(int id);
    }
}
