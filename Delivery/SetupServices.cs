using Delivery.Core.Contracts;
using Delivery.Core.DataServices;
using Delivery.Core.NetworkServices;
using Delivery.Infrastructure.Repositories;

namespace Delivery
{
    public static class SetupServices
    {
        public static IServiceCollection AddStartupServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSignalR(
               options =>
               {
                   options.EnableDetailedErrors = true;
               });

            //Data Services
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddScoped<IPackagesService, PackagesService>();
            services.AddScoped<IAllergensService, AllergensService>();
            services.AddScoped<IExtrasService, ExtrasService>();
            services.AddScoped<IMenuService, MenuService>();

            //Network Services
            services.AddScoped<ICloudinaryService>(x => new CloudinaryService(
              configuration.GetSection("CloudSettings:CloudName").Value,
              configuration.GetSection("CloudSettings:ApiKey").Value,
              configuration.GetSection("CloudSettings:ApiSecret").Value));

            //Repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}
