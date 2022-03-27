using Delivery.Core.Contracts;
using Delivery.Core.ViewModels.AllergensProducts;
using Delivery.Core.ViewModels.Products;
using Delivery.Infrastructure.Models;
using Delivery.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Core.DataServices
{
    public class ProductService : IProductService
    {
        private const string ProductNotFound = "Няма такъв продукт";
        private readonly IRepository<Allergen> allergenRepo;
        private readonly IRepository<Package> packageRepo;
        private readonly IRepository<Category> categoryRepo;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IRepository<Product> productRepo;

        public ProductService(IRepository<Allergen> allergenRepo,
            IRepository<Package> packageRepo,
            IRepository<Category> categoryRepo,
            ICloudinaryService cloudinaryService,
            IRepository<Product> productRepo)
        {
            this.allergenRepo = allergenRepo;
            this.packageRepo = packageRepo;
            this.categoryRepo = categoryRepo;
            this.cloudinaryService = cloudinaryService;
            this.productRepo = productRepo;
        }

        public ProductInputModel AddDropdownsCollections(ProductInputModel model)
        {
            model.Allergens = allergenRepo
                .All()
                .Where(x => !x.IsDeleted)
                .AsEnumerable()
                .Select(x => new AllergensProductsInputModel()
                {
                    AllergenId = x.Id,
                    Name = x.Name,
                    IsCheked = model.Allergens.Any(a => a.AllergenId ==  x.Id && a.IsCheked),
                }).ToList();

            model.Packages = packageRepo
                .All()
                .Where(x => !x.IsDeleted)
                .AsEnumerable()
                .Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = x.Id == model.PackageId
                }).ToList();

            model.Categories = categoryRepo
                .All()
                .Where(x => !x.IsDeleted)
                .AsEnumerable()
                .Select(x => new SelectListItem()
                {
                    Value = x.Id,
                    Text = x.Name,
                    Selected = x.Id == model.CategoryId
                }).ToList();

            return model;
        }

        public async Task<ProductEditModel> CreateEditModelAsync(string id)
        {
            var model = await productRepo
                .All()
                .Where(x => x.Id == id)
                .Select(x => new ProductEditModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    CategoryId = x.CategoryId,
                    HasExtras = x.HasExtras,
                    ImageUrl = x.ImageUrl,
                    MaxProductsInPackage = x.MaxProductsInPackage,
                    Price = x.Price,
                    Weight = x.Weight,
                    PackageId = x.PackageId,
                    Allergens = x.Allergens
                                .Select(s => new AllergensProductsInputModel()
                                {
                                    AllergenId = s.AllergenId,
                                    Name = s.Allergen.Name,
                                    IsCheked = true
                                }).ToList()
                }).FirstOrDefaultAsync();

            if (model is null)
            {
                throw new ArgumentException(ProductNotFound);
            }

            model = (ProductEditModel)AddDropdownsCollections(model);

            return model;
        }

        public async Task CreateProductAsync(ProductInputModel model)
        {
            var uploadImageTask = cloudinaryService.UploadImageAsync(model.Image);

            var product = new Product()
            {
                Name = model.Name,
                Description = model.Description,
                HasExtras = model.HasExtras,
                CategoryId = model.CategoryId,
                Price = model.Price,
                Weight = model.Weight,
                MaxProductsInPackage = model.MaxProductsInPackage,
                PackageId = model.PackageId,              
            };

            product.Allergens = model.Allergens
                                .Where(x => x.IsCheked)
                                .Select(x => new AllergensProducts()
                                {
                                    AllergenId = x.AllergenId,
                                    ProductId = product.Id
                                }).ToList();

            product.ImageUrl = uploadImageTask.Result;

            await productRepo.AddAsync(product);

            await productRepo.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(string id)
        {
            var product = await productRepo.All().Where(x => x.Id == id).FirstOrDefaultAsync();

            if (product is null)
            {
                throw new ArgumentException(ProductNotFound);
            }

            productRepo.Delete(product);

            await productRepo.SaveChangesAsync();
        }

        public async Task EditProductAsync(ProductEditModel model)
        {
            var product = await productRepo.All().Where(x => x.Id == model.Id).FirstOrDefaultAsync();

            if (product is null)
            {
                throw new ArgumentException(ProductNotFound);
            }

            if (model.Image is not null)
            {
                await cloudinaryService.DeleteImageAsync(model.ImageUrl);
                model.ImageUrl = await cloudinaryService.UploadImageAsync(model.Image);
            }

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.MaxProductsInPackage = model.MaxProductsInPackage;
            product.Weight = model.Weight;
            product.CategoryId = model.CategoryId;
            product.PackageId = model.PackageId;
            product.ImageUrl = model.ImageUrl;
            product.HasExtras = model.HasExtras;

            var allergensToBeRemoved = product.Allergens.Where(x => model.Allergens.Any(a => !a.IsCheked && x.AllergenId == a.AllergenId));
            product.Allergens = product.Allergens.Except(allergensToBeRemoved).ToList();

            var allergensToBeAdded = model
                .Allergens
                .Where(x => x.IsCheked)
                .Where(x => !product.Allergens.Any(a => a.AllergenId == x.AllergenId))
                .Select(x => new AllergensProducts() { AllergenId = x.AllergenId, ProductId = product.Id })
                .ToList();
            allergensToBeAdded.ForEach(x => product.Allergens.Add(x));

            productRepo.Update(product);

            await productRepo.SaveChangesAsync();
        }

        public Task<List<ProductAdminListViewModel>> GetListWithProductsAsync()
            => productRepo
            .All()
            .Where(x => !x.IsDeleted)
            .Select(x => new ProductAdminListViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                CategoryName = x.Category.Name
            }).ToListAsync();
    }
}
