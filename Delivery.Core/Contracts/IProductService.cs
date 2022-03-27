using Delivery.Core.ViewModels.Products;

namespace Delivery.Core.Contracts
{
    public interface IProductService
    {
        ProductInputModel AddDropdownsCollections(ProductInputModel model);
        Task CreateProductAsync(ProductInputModel model);
        Task<List<ProductAdminListViewModel>> GetListWithProductsAsync();
        Task<ProductEditModel> CreateEditModelAsync(string id);
        Task EditProductAsync(ProductEditModel model);
    }
}
