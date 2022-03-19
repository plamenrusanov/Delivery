namespace Delivery.Infrastructure.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> All();

        Task AddAsync(T entity);

        void Update(T entity);

        void Delete(T entity);

        Task<int> SaveChangesAsync();
    }
}
