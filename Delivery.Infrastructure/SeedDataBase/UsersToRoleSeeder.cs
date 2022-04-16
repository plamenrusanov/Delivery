using Delivery.Infrastructure.Constants;
using Delivery.Infrastructure.Data;
using Delivery.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.Infrastructure.SeedDataBase
{
    public class UsersToRoleSeeder : ISeeder
    {
        public async Task SeedAsync(DeliveryDbContext dbContext, IServiceProvider serviceProvider)
        {
            using var serviceScope = serviceProvider.CreateScope();
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<DeliveryUser>>();

            await this.AddUserToRole(
                GlobalConstants.AdministratorName,
                GlobalConstants.AdministratorName,
                userManager);
            await this.AddUserToRole(
                GlobalConstants.AdministratorName,
                GlobalConstants.UserNameAsString,
                userManager);
        }

        private async Task AddUserToRole(string userName, string role, UserManager<DeliveryUser> userManager)
        {
            var user = await userManager.FindByNameAsync(userName);

            if (await userManager.IsInRoleAsync(user, role))
            {
                return;
            }

            await userManager.AddToRoleAsync(user, role);
        }
    }
}
