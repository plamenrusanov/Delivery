namespace Delivery.Infrastructure.Models
{
    public class ExtraItem
    {
        public int ExtraId { get; set; }

        public Extra? Extra { get; set; }

        public int ShopingCartItemId { get; set; }

        public ShopingCartItem? ShopingCartItem { get; set; }

        public int Quantity { get; set; }
    }
}
