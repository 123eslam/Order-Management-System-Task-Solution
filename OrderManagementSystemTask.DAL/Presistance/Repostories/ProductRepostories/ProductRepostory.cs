using OrderManagementSystemTask.DAL.Entities;
using OrderManagementSystemTask.DAL.Presistance.Data;
using OrderManagementSystemTask.DAL.Presistance.Repostories._Generic;

namespace OrderManagementSystemTask.DAL.Presistance.Repostories.ProductRepostories
{
    public class ProductRepostory : GenericRepository<Product ,int>, IProductRepostory
    {
        public ProductRepostory(OrderManagementDbContext dbContext) : base(dbContext) { }
    }
}
