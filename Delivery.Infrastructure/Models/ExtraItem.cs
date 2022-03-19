using System.ComponentModel.DataAnnotations;

namespace Delivery.Infrastructure.Models
{
    public class ExtraItem
    {
        public int ExtraId { get; set; }

        public virtual Extra? Extra { get; set; }

        public int ShopingCartItemId { get; set; }

        public virtual ShoppingCartItem ShopingCartItem { get; set; }

        [Range(0, 100)]
        public int Quantity { get; set; }
    }
}
