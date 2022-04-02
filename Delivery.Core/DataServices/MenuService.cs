using Delivery.Core.Contracts;
using Delivery.Core.ViewModels.AllergensProducts;
using Delivery.Core.ViewModels.Categories;
using Delivery.Core.ViewModels.Extras;
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
        private readonly IRepository<Extra> extraRepo;

        public MenuService(IRepository<Product> productRepo,
            IRepository<Category> categoryRepo,
            IRepository<Extra> extraRepo)
        {
            this.productRepo = productRepo;
            this.categoryRepo = categoryRepo;
            this.extraRepo = extraRepo;
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

        public async Task<ProductDetailsViewModel> GetProductDetailsAsync(string productId)
        {
            if (string.IsNullOrWhiteSpace(productId))
            {
                throw new ArgumentException("Неверни данни");
            }

            var product = await productRepo
                .All()
                .Where(x => x.Id == productId)
                .Select(x => new ProductDetailsViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageUrl= x.ImageUrl,
                    Description = x.Description,
                    CategoryId = x.CategoryId,
                    Weight = x.Weight,
                    Price = x.Price,
                    MaxProductsInPackage = x.MaxProductsInPackage,
                    PackageId = x.PackageId,
                    PackagePrice = x.Package.Price,
                    HasExtras = x.HasExtras,
                    Allergens = x.Allergens.Select(a => new AllergensProductsViewModel()
                    {
                        ImageUrl = a.Allergen.ImageUrl,
                        Name = a.Allergen.Name
                    }).ToArray()
                }).FirstOrDefaultAsync();

            if (product is null)
            {
                throw new ArgumentException("Няма такъв продукт");
            }

            product.Extras = await extraRepo
                .All()
                .Where(x => !x.IsDeleted)
                .Select(x => new ExtraViewModel()
                {
                    Id= x.Id,
                    Name= x.Name,
                    Price = x.Price,
                    Weight = x.Weight
                }).ToListAsync();

            return product;
        }
    }
}
