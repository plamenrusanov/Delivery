using Delivery.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delivery.Infrastructure.Configurations
{
    public class DeliveryUserConfiguration : IEntityTypeConfiguration<DeliveryUser>
    {
        public void Configure(EntityTypeBuilder<DeliveryUser> builder)
        {
            builder
               .HasMany(e => e.UserRoles)
               .WithOne()
               .HasForeignKey(e => e.UserId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
