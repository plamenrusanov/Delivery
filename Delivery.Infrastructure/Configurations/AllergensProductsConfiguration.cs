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
        }
    }
}
