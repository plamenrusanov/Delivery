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
        private readonly string exMessage  = "Невалидни параметри!";
        private readonly IRepository<Category> categoryRepo;
        private readonly IMapper mapper;

        public CategoriesService(IRepository<Category> categoryRepo, IMapper mapper)
        {
            this.categoryRepo = categoryRepo;
            this.mapper = mapper;
        }

        public async Task CreateCategoryAsync(CategoryInputModel model)
        {
            var category = mapper.Map<Category>(model);

            await categoryRepo.AddAsync(category);

            await categoryRepo.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(string id)
        {
            var category = await categoryRepo.All().FirstOrDefaultAsync(x => x.Id == id);
            if (category is null)
            {
                throw new ArgumentException(exMessage);
            }

            categoryRepo.Delete(category);

            await categoryRepo.SaveChangesAsync();
        }

        public Task<List<CategoryViewModel>> GetCategoriesWhitoutDeletedAsync()
            => categoryRepo
            .All()
            .Where(x => !x.IsDeleted)
            .Select(x => new CategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name,
            })
            .ToListAsync();

        public async Task<CategoryEditModel> GetCategoryEditModelAsync(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(exMessage);
            }

           var model = await categoryRepo
                .All()
                .Where(x => x.Id == id)
                .Select(x => new CategoryEditModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                })
               .FirstOrDefaultAsync();

            if (model is null)
            {
                throw new ArgumentException(exMessage);
            }

            return model;
        }
        public async Task UpdateCategoryAsync(CategoryEditModel model)
        {
            var category = mapper.Map<Category>(model);

            categoryRepo.Update(category);

            await categoryRepo.SaveChangesAsync();
        }
    }
}
