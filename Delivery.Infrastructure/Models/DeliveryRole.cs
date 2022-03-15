using Microsoft.AspNetCore.Identity;

namespace Delivery.Infrastructure.Models
{
    public class DeliveryRole : IdentityRole
    {
        public DeliveryRole(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
