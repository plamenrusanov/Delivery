using Delivery.Infrastructure.Constants;
using Delivery.Infrastructure.Data;
using Delivery.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.Infrastructure.SeedDataBase
{
    public class AdminSeeder : ISeeder
    {
        public async Task SeedAsync(DeliveryDbContext dbContext, IServiceProvider serviceProvider)
        {
            using var serviceScope = serviceProvider.CreateScope();
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<DeliveryUser>>();

            await this.CreateUserAsync(GlobalConstants.AdministratorName, GlobalConstants.EmailAdministrator, userManager);
        }

        private async Task CreateUserAsync(string username, string email, UserManager<DeliveryUser> userManager)
        {
            var user = await userManager.FindByNameAsync(username);

            if (user != null)
            {
                return;
            }

           var result = await userManager.CreateAsync(
                new DeliveryUser()
                {
                    UserName = username,
                    Email = email,
                    EmailConfirmed = true,
                }, $"{username}5");
        }

    }
}
