using Delivery.Core.ViewModels.Extras;

namespace Delivery.Core.Contracts
{
    public interface IExtrasService
    {

        Task<List<ExtraViewModel>> All();
        Task UpdateExtraAsync(ExtraEditModel model);
        Task DeleteAsync(int id);
        Task AddExtraAsync(ExtraInpitModel model);
        Task<ExtraEditModel> GetEditModelAsync(int id);
    }
}
