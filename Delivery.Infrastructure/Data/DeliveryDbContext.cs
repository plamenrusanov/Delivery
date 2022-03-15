using Delivery.Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Infrastructure.Data
{
    public class DeliveryDbContext : IdentityDbContext<DeliveryUser, DeliveryRole, string>
    {
        private DbSet<Allergen> Allergens { get; set; }
        private DbSet<AllergensProducts> AllergensProducts { get; set; }
        private DbSet<Category> Categories { get; set; }
        private DbSet<DeliveryAddress> DeliveryAddresses { get; set; }
        private DbSet<DeliveryTax> DeliveryTaxes { get; set; }
        private DbSet<Extra> Extras { get; set; }
        private DbSet<ExtraItem> ExtraItems { get; set; }
        private DbSet<Order> Orders { get; set; }
        private DbSet<Package> Packages { get; set; }
        private DbSet<Product> Products { get; set; }
        private DbSet<Settings> Settings { get; set; }
        private DbSet<ShoppingCart> ShoppingCarts { get; set; }
        private DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
