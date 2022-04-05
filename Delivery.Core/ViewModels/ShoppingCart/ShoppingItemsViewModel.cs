using Delivery.Core.ViewModels.ExtraItems;

namespace Delivery.Core.ViewModels.ShoppingCart
{
    public class ShoppingItemsViewModel
    {
        public ShoppingItemsViewModel()
        {
            this.Extras = new List<ExtraCartItemModel>();
        }

        public int Id { get; set; }

        public string? ProductId { get; set; }

        public string? ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public int Quantity { get; set; }

        public decimal ItemPrice => (this.ProductPrice * this.Quantity) + this.Extras.Sum(x => x.Price * x.Quantity);

        public string? Description { get; set; }

        public byte Rating { get; set; }

        public List<ExtraCartItemModel> Extras { get; set; }
    }
}
