using Delivery.Core.Contracts;
using Delivery.Core.DataServices;
using Delivery.Infrastructure.Repositories;

namespace Delivery
{
    public static class StartupServices
    {
        public static IServiceCollection AddStartupServices(this IServiceCollection services)
        {
            services.AddSignalR(
               options =>
               {
                   options.EnableDetailedErrors = true;
               });
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }
    }
}
