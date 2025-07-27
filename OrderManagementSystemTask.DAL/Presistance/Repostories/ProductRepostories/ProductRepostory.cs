using Microsoft.EntityFrameworkCore;
using OrderManagementSystemTask.DAL.Entities;
using OrderManagementSystemTask.DAL.Presistance.Data;
using OrderManagementSystemTask.DAL.Presistance.Repostories._Generic;

namespace OrderManagementSystemTask.DAL.Presistance.Repostories.ProductRepostories
{
    public class ProductRepostory : GenericRepository<Product ,int>, IProductRepostory
    {
        public ProductRepostory(OrderManagementDbContext dbContext) : base(dbContext) { }
        public async Task<IEnumerable<Product>> GetByIdsAsync(List<int> ids)
        {
            return await _dbContext.Products.Where(p => ids.Contains(p.Id)).ToListAsync();
        }
    }
}
