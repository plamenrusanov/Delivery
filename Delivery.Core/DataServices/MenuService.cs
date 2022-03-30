using Delivery.Core.Contracts;
using Delivery.Core.ViewModels.Categories;
using Delivery.Core.ViewModels.Menu;
using Delivery.Core.ViewModels.Products;
using Delivery.Infrastructure.Models;
using Delivery.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Core.DataServices
{
    public class MenuService : IMenuService
    {
        private readonly IRepository<Product> productRepo;
        private readonly IRepository<Category> categoryRepo;

        public MenuService(IRepository<Product> productRepo,
            IRepository<Category> categoryRepo)
        {
            this.productRepo = productRepo;
            this.categoryRepo = categoryRepo;
        }
        public async Task<MenuViewModel> GetCategoriesWithProdutsAsync(string categoryId = null)
        {
            MenuViewModel model = new();

            model.Categories = await categoryRepo
                .All()
                .Where(x => !x.IsDeleted)
                .Select(x => new CategoryViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToListAsync();

            if (string.IsNullOrWhiteSpace(categoryId))
            {
                
            }
            else
            {
                model.Products = await productRepo
                    .All()
                    .Where(x => !x.IsDeleted && x.CategoryId == categoryId)
                    .Select(x => new ProductMenuViewModel()
                    {
                        Id=x.Id,
                        Name=x.Name,
                        ImageUrl = x.ImageUrl,
                        Price = x.Price,
                        Weight = x.Weight
                    }).ToListAsync();
            }

            return model;
        }
    }
}
