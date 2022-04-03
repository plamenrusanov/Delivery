namespace Delivery.Core.ViewModels.Orders
{
    public class ShoppingCartItemInputModel
    {
        public ShoppingCartItemInputModel()
        {
            Extras = new List<ExtraItemInputModel>();
        }
        public decimal SubTotal { get; set; }

        public decimal ProductPrice { get; set; }

        public decimal PackagePrice { get; set; }

        public int MaxProducts { get; set; }

        public string PName { get; set; } = String.Empty;

        public string PId { get; set; } = String.Empty;

        public int Qty { get; set; }

        public string? Description { get; set; }

        public ICollection<ExtraItemInputModel> Extras { get; set; }
    }
}
