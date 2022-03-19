using Delivery.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T>, IDisposable
        where T : class
    {
        private readonly DeliveryDbContext context;
        private readonly DbSet<T> dbSet;

        public Repository(DeliveryDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.dbSet = this.context.Set<T>();
        }
        public IQueryable<T> All() => this.context.Set<T>();

        public Task AddAsync(T entity) => this.context.Set<T>().AddAsync(entity).AsTask();

        public void Update(T entity)
        {
            var entry = this.context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.context.Set<T>().Attach(entity);
            }
            entry.State = EntityState.Modified;
        }
        public void Delete(T entity) => this.context.Set<T>().Remove(entity);

        public Task<int> SaveChangesAsync() => this.context.SaveChangesAsync();

        public void Dispose()
        {
            this.context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
