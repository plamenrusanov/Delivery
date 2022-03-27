using Delivery.Infrastructure.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery.Infrastructure.Models
{
    public class ShoppingCart : BaseEntity<string>
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ShoppingCart()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            this.Id = Guid.NewGuid().ToString();
            this.CartItems = new List<ShoppingCartItem>();
        }

        [Key]
        public override string Id { get; set; }

        [StringLength(36)]
        public string DeliveryUserId { get; set; }

        public virtual DeliveryUser DeliveryUser { get; set; }

        [NotMapped]
        public decimal TotalPrice { get; set; }

        public virtual ICollection<ShoppingCartItem> CartItems { get; set; }
    }
}
