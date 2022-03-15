using Delivery.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delivery.Infrastructure.Configurations
{
    public class ExtraItemConfiguration : IEntityTypeConfiguration<ExtraItem>
    {
        public void Configure(EntityTypeBuilder<ExtraItem> builder)
        {
            builder
                .HasKey(x => new { x.ShopingCartItemId, x.ExtraId });
        }
    }
}
