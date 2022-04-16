using Delivery.Infrastructure.Data;

namespace Delivery.Infrastructure.SeedDataBase
{
    public interface ISeeder
    {
        Task SeedAsync(DeliveryDbContext dbContext, IServiceProvider serviceProvider);
    }
}
