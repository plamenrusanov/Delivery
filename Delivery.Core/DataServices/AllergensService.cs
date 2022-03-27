using Delivery.Core.Contracts;
using Delivery.Core.ViewModels.Allergens;
using Delivery.Infrastructure.Models;
using Delivery.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Core.DataServices
{
    public class AllergensService : IAllergensService
    {
        private const string AllegenNotFound = "Алергена не е намерен в базата!";
        private readonly ICloudinaryService cloudinaryService;
        private readonly IRepository<Allergen> allergenRepo;

        public AllergensService(ICloudinaryService cloudinaryService, IRepository<Allergen> allergenRepo)
        {
            this.cloudinaryService = cloudinaryService;
            this.allergenRepo = allergenRepo;
        }

        public async Task CreateAllergenAsync(AllergenInputModel model)
        {
            var imageUrl = await cloudinaryService.UploadImageAsync(model.Image);

            await allergenRepo.AddAsync(new Allergen()
            {
                ImageUrl = imageUrl,
                Name = model.Name
            });

            await allergenRepo.SaveChangesAsync();
        }

        public async Task DeleteAllergenAsync(string id)
        {
            var allergen = await allergenRepo.All().Where(x => x.Id == id).FirstOrDefaultAsync();

            if (allergen is null)
            {
                throw new ArgumentException(AllegenNotFound);
            }

            await cloudinaryService.DeleteImageAsync(allergen.ImageUrl);

            allergenRepo.Delete(allergen);

            await allergenRepo.SaveChangesAsync();
        }

        public async Task<AllergenEditModel> GetAllergenEditModelAsync(string id)
        {
            var allergen = await allergenRepo.All().Where(x => x.Id == id).FirstOrDefaultAsync();

            if (allergen is null)
            {
                throw new ArgumentException(AllegenNotFound);
            }

            return new AllergenEditModel()
            {
                Id = allergen.Id,
                Name = allergen.Name,
                ImageUrl = allergen.ImageUrl,
            };
        }

        public Task<List<AllergenViewModel>> GetAllergensWhitoutDeletedAsync()
            => allergenRepo
            .All()
            .Where(x => !x.IsDeleted)
            .Select(x => new AllergenViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                ImageUrl = x.ImageUrl,
            })
            .ToListAsync();

        public async Task UpdateAllergenAsync(AllergenEditModel model)
        {
            var allergen = await allergenRepo.All().Where(x => x.Id == model.Id).FirstOrDefaultAsync();

            if (allergen is null)
            {
                throw new ArgumentException(AllegenNotFound);
            }

            if (model.FormFile is not null)
            {
                await cloudinaryService.DeleteImageAsync(model.ImageUrl);
                model.ImageUrl = await cloudinaryService.UploadImageAsync(model.FormFile);
            }

            allergen.ImageUrl = model.ImageUrl;
            allergen.Name = model.Name;

            allergenRepo.Update(allergen);

            await allergenRepo.SaveChangesAsync();

        }
    }
}
