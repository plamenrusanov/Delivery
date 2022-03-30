using Delivery.Core.ViewModels.Menu;

namespace Delivery.Core.Contracts
{
    public interface IMenuService
    {
        Task<MenuViewModel> GetCategoriesWithProdutsAsync(string categoryId = null);
    }
}
