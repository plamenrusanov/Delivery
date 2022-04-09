using Delivery.Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Infrastructure.Data
{
    public class DeliveryDbContext : IdentityDbContext<DeliveryUser, DeliveryRole, string>
    {
        public DeliveryDbContext(DbContextOptions<DeliveryDbContext> options)
          : base(options) { }

        public DbSet<Allergen>? Allergens { get; set; }
        public DbSet<AllergensProducts>? AllergensProducts { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<DeliveryAddress>? DeliveryAddresses { get; set; }
        public DbSet<Extra>? Extras { get; set; }
        public DbSet<ExtraItem>? ExtraItems { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<Package>? Packages { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Settings>? Settings { get; set; }
        public DbSet<ShoppingCartItem>? ShoppingCartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
