using Delivery.Infrastructure.Constants;
using Delivery.Infrastructure.Data;
using Delivery.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.Infrastructure.SeedDataBase
{
    internal class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(DeliveryDbContext dbContext, IServiceProvider serviceProvider)
        {
            using var serviceScope = serviceProvider.CreateScope();
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<DeliveryRole>>();

            await SeedRoleAsync(roleManager, GlobalConstants.AdministratorName);
            await SeedRoleAsync(roleManager, GlobalConstants.UserNameAsString);

        }

        private static async Task SeedRoleAsync(RoleManager<DeliveryRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var result = await roleManager.CreateAsync(new DeliveryRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
