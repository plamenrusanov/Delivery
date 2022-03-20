using Delivery.Core.ViewModels.Categories;

namespace Delivery.Core.Contracts
{
    public interface ICategoriesService
    {
        Task CreateCategoryAsync(CategoryInputModel model);
        Task<List<CategoryViewModel>> GetCategoriesWhitoutDeleted();
        Task<CategoryEditModel> GetCategoryEditModelAsync(string id);
        Task UpdateCategoryAsync(CategoryEditModel model);
        Task DeleteCategoryAsync(string id);
    }
}
