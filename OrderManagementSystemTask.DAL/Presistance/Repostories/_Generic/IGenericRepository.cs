using OrderManagementSystemTask.DAL.Entities;

namespace OrderManagementSystemTask.DAL.Presistance.Repostories._Generic
{
    public interface IGenericRepository<T, TKey> where T : ModelBase<TKey>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(TKey id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
