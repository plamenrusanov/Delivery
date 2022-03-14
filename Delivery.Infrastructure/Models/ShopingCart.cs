using Delivery.Infrastructure.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery.Infrastructure.Models
{
    public class ShopingCart : BaseEntity<string>
    {
        public ShopingCart()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CartItems = new HashSet<ShopingCartItem>();
        }

        [Key]
        public override string Id { get; set; }

        public string? DeliveryUserId { get; set; }

        [NotMapped]
        public decimal TotalPrice { get; set; }

        public ICollection<ShopingCartItem>? CartItems { get; set; }
    }
}
