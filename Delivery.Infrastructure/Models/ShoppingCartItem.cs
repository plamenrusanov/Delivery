namespace Delivery.Infrastructure.Models
{
    public class ShoppingCartItem
    {
        public ShoppingCartItem()
        {
            ExtraItems = new List<ExtraItem>();
        }
        public string? ProductId { get; set; }

        public Product? Product { get; set; }

        public string? ShopingCartId { get; set; }

        public ShoppingCart? Cart { get; set; }

        public int Quantity { get; set; }

        public byte Rating { get; set; }

        public string? Description { get; set; }

        public ICollection<ExtraItem> ExtraItems { get; set; }
    }
}
