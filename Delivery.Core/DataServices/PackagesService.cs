using AutoMapper;
using Delivery.Core.Contracts;
using Delivery.Core.ViewModels.Packagies;
using Delivery.Infrastructure.Models;
using Delivery.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Core.DataServices
{
    public class PackagesService : IPackagesService
    {
        private readonly IRepository<Package> packageRepo;
        private readonly IMapper mapper;

        public PackagesService(IRepository<Package> packageRepo, IMapper mapper)
        {
            this.packageRepo = packageRepo;
            this.mapper = mapper;
        }
        public async Task CreatePackageAsync(PackageInputModel model)
        {
            var package = mapper.Map<Package>(model);
            await packageRepo.AddAsync(package);
            await packageRepo.SaveChangesAsync();
        }

        public async Task DeletePackageAsync(int id)
        {
            var package = await packageRepo.All().FirstOrDefaultAsync(x => x.Id == id);

            if (package is null)
            {
                throw new ArgumentException("Невалидна стойност");
            }

            package.IsDeleted = true;
            package.DeletedOn = DateTime.Now;

            await packageRepo.SaveChangesAsync();
        }

        public Task<PackageEditModel> GetPackageEditModelAsync(int id)
            => packageRepo
                .All()
                .Where(x => x.Id == id)
                .Select(x => new PackageEditModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price
                })
                .FirstAsync();

        public Task<List<PackageViewModel>> GetPackagesWhitoutDeletedAsync()
            => packageRepo
                .All()
                .Where(x => !x.IsDeleted)
                .Select(x => new PackageViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price
                })
                .ToListAsync();
        public async Task UpdatePackageAsync(PackageEditModel model)
        {
            var package = mapper.Map<Package>(model);
            packageRepo.Update(package);
            await packageRepo.SaveChangesAsync();
        }
    }
}
