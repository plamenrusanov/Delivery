using Delivery.Core.ViewModels.Products;

namespace Delivery.Core.Contracts
{
    public interface IProductService
    {
        Task<ProductInputModel> AddDropdownsCollectionsAsync(ProductInputModel model);
        Task CreateProductAsync(ProductInputModel model);
        Task<List<ProductAdminListViewModel>> GetListWithProductsAsync();
        Task<ProductEditModel> CreateEditModelAsync(string id);
        Task EditProductAsync(ProductEditModel model);
        Task DeleteProductAsync(string id);
    }
}
