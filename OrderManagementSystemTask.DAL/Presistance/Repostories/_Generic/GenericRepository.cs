using Microsoft.EntityFrameworkCore;
using OrderManagementSystemTask.DAL.Entities;
using OrderManagementSystemTask.DAL.Presistance.Data;

namespace OrderManagementSystemTask.DAL.Presistance.Repostories._Generic
{
    public class GenericRepository<T, TKey> : IGenericRepository<T, TKey> where T : ModelBase<TKey>
    {
        private protected readonly OrderManagementDbContext _dbContext;

        public GenericRepository(OrderManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
        public async Task<T?> GetByIdAsync(TKey id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }
    }
}
