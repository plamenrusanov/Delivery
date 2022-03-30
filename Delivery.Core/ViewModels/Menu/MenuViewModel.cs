using Delivery.Core.ViewModels.Categories;
using Delivery.Core.ViewModels.Products;

namespace Delivery.Core.ViewModels.Menu
{
    public class MenuViewModel
    {
        public MenuViewModel()
        {
            Products = new List<ProductMenuViewModel>();
            Categories = new List<CategoryViewModel>(); 
        }

        public ICollection<ProductMenuViewModel> Products { get; set; }

        public ICollection<CategoryViewModel> Categories { get; set; }
    }
}
