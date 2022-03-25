using Delivery.Core.Contracts;
using Delivery.Core.ViewModels.Allergens;
using Delivery.Infrastructure.Models;
using Delivery.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Core.DataServices
{
    public class AllergensService : IAllergensService
    {
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

        public Task DeleteAllergenAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<AllergenEditModel> GetAllergenEditModelAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<AllergenViewModel>> GetAllergensWhitoutDeleted()
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

        public Task UpdateAllergenAsync(AllergenEditModel model)
        {
            throw new NotImplementedException();
        }
    }
}
