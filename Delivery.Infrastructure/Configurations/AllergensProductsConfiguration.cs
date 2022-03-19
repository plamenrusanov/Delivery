using Delivery.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delivery.Infrastructure.Configurations
{
    public class AllergensProductsConfiguration : IEntityTypeConfiguration<AllergensProducts>
    {
        public void Configure(EntityTypeBuilder<AllergensProducts> builder)
        {
            builder
               .HasKey(x => new { x.AllergenId, x.ProductId });

            builder.HasOne(x => x.Allergen)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.AllergenId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.Allergens)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
