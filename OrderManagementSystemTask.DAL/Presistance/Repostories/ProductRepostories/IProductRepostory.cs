using OrderManagementSystemTask.DAL.Entities;
using OrderManagementSystemTask.DAL.Presistance.Repostories._Generic;

namespace OrderManagementSystemTask.DAL.Presistance.Repostories.ProductRepostories
{
    public interface IProductRepostory : IGenericRepository<Product ,int>
    {
        Task<IEnumerable<Product>> GetByIdsAsync(List<int> ids);
    }
}
