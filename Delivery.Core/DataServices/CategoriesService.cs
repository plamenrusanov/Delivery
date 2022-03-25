using AutoMapper;
using Delivery.Core.Contracts;
using Delivery.Core.ViewModels.Categories;
using Delivery.Infrastructure.Models;
using Delivery.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Core.DataServices
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository<Category> categoryRepo;
        private readonly IMapper mapper;

        public CategoriesService(IRepository<Category> categoryRepo, IMapper mapper)
        {
            this.categoryRepo = categoryRepo;
            this.mapper = mapper;
        }

        public async Task CreateCategoryAsync(CategoryInputModel model)
        {
            var position = (await categoryRepo.All().MaxAsync(x => (int?)x.Position)) ?? 0;

            var category = mapper.Map<Category>(model);
            category.Position = ++position;
            await categoryRepo.AddAsync(category);

            await categoryRepo.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(string id)
        {
            var category = await categoryRepo.All().FirstOrDefaultAsync(x => x.Id == id);
            if (category is null)
            {
                throw new ArgumentException("Невалидни параметри!");
            }

            categoryRepo.Delete(category);

            await categoryRepo.SaveChangesAsync();
        }

        public Task<List<CategoryViewModel>> GetCategoriesWhitoutDeleted()
            => categoryRepo
            .All()
            .Where(x => !x.IsDeleted)
            .Select(x => new CategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Position = x.Position,
            })
            .ToListAsync();

        public Task<CategoryEditModel> GetCategoryEditModelAsync(string id)
            => categoryRepo
                .All()
                .Where(x => x.Id == id)
                .Select(x => new CategoryEditModel()
                {
                    Id = x.Id,
                    Name= x.Name,
                    Position= x.Position,
                })
               .FirstAsync();

        public async Task UpdateCategoryAsync(CategoryEditModel model)
        {
            var category = mapper.Map<Category>(model);

            categoryRepo.Update(category);

            await categoryRepo.SaveChangesAsync();
        }
    }
}
