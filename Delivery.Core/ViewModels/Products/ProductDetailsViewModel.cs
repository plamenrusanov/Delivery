using Delivery.Core.ViewModels.AllergensProducts;
using Delivery.Core.ViewModels.Extras;

namespace Delivery.Core.ViewModels.Products
{
    public class ProductDetailsViewModel
    {
        public ProductDetailsViewModel()
        {
            Allergens = new List<AllergensProductsViewModel>();
            Extras = new List<ExtraViewModel>();
        }
        public string? Id { get; set; }

        public string? Name { get; set; }

        public string? ImageUrl { get; set; }

        public decimal? Price { get; set; }

        public int? Weight { get; set; }

        public string? Description { get; set; }

        public string? ShoppingCartItemDescription { get; set; }

        public string? CategoryId { get; set; }

        public bool? HasExtras { get; set; }

        public int? MaxProductsInPackage { get; set; }

        public int? PackageId { get; set; }

        public decimal? PackagePrice { get; set; }

        public IList<AllergensProductsViewModel>? Allergens { get; set; }

        public IList<ExtraViewModel>? Extras { get; set; }

        public decimal? SubTotal => Price + PackagePrice;

    }
}
