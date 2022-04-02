using Delivery.Core.ViewModels.Menu;
using Delivery.Core.ViewModels.Products;

namespace Delivery.Core.Contracts
{
    public interface IMenuService
    {
        Task<MenuViewModel> GetCategoriesWithProdutsAsync(string categoryId = null);
        Task<ProductDetailsViewModel> GetProductDetailsAsync(string productId);
    }
}
