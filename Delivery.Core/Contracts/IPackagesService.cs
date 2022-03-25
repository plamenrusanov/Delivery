using Delivery.Core.ViewModels.Packagies;

namespace Delivery.Core.Contracts
{
    public interface IPackagesService
    {
        Task DeletePackageAsync(int id);
        Task UpdatePackageAsync(PackageEditModel model);
        Task<PackageEditModel> GetPackageEditModelAsync(int id);
        Task CreatePackageAsync(PackageInputModel model);
        Task<List<PackageViewModel>> GetPackagesWhitoutDeletedAsync();
    }
}
