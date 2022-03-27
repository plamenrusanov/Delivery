using Delivery.Core.ViewModels.Allergens;

namespace Delivery.Core.Contracts
{
    public interface IAllergensService
    {
        Task<List<AllergenViewModel>> GetAllergensWhitoutDeletedAsync();
        Task CreateAllergenAsync(AllergenInputModel model);
        Task<AllergenEditModel> GetAllergenEditModelAsync(string id);
        Task UpdateAllergenAsync(AllergenEditModel model);
        Task DeleteAllergenAsync(string id);
    }
}
