using System.ComponentModel.DataAnnotations;

namespace Delivery.Infrastructure.Models
{
    public class ExtraItem
    {
        public int ExtraId { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public virtual Extra Extra { get; set; }

        public int ShopingCartItemId { get; set; }

        public virtual ShoppingCartItem ShopingCartItem { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [Range(0, 100)]
        public int Quantity { get; set; }
    }
}
