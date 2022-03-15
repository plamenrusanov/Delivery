using Delivery.Infrastructure.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery.Infrastructure.Models
{
    public class ShoppingCart : BaseEntity<string>
    {
        public ShoppingCart()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CartItems = new List<ShoppingCartItem>();
        }

        [Key]
        public override string Id { get; set; }

        [StringLength(36)]
        public string? DeliveryUserId { get; set; }

        [NotMapped]
        public decimal TotalPrice { get; set; }

        public ICollection<ShoppingCartItem> CartItems { get; set; }
    }
}
