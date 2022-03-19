using Delivery.Infrastructure.Common;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Delivery.Infrastructure.Models
{
    public class DeliveryUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public DeliveryUser()
        {
            Id = Guid.NewGuid().ToString();
            UserRoles = new List<IdentityUserRole<string>>();
            Orders = new List<Order>();
        }

        [Key]
        [StringLength(36)]
        public override string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
