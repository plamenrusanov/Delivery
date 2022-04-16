using Delivery.Core.Contracts;
using Delivery.Core.DataServices;
using Delivery.Core.ViewModels.AllergensProducts;
using Delivery.Core.ViewModels.Products;
using Delivery.Infrastructure.Models;
using Delivery.Infrastructure.Repositories;
using Delivery.Test.FakeObjects;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Delivery.Test.DataServicesTest
{
    public class ProductServiceTest
    {
        [Fact]
        public async Task AddDropdownsCollectionsAsync_ShouldReturnModel()
        {

            ProductInputModel model = new ProductInputModel()
            {
                Allergens = new List<AllergensProductsInputModel>()
                {
                    new AllergensProductsInputModel(){ AllergenId = "0df8be95-5c9b-4693-80e0-cb7c97216ed9", IsCheked = true},
                    new AllergensProductsInputModel(){ AllergenId = "3439585e-324d-4b2c-a921-5e7705f287f2", IsCheked = true},
                    new AllergensProductsInputModel(){ AllergenId = "9e07962d-9c64-4c27-b2ab-71ca8e592715", IsCheked = false},
                    new AllergensProductsInputModel(){ AllergenId = "a9edd26f-fb4e-4a0e-861d-c73e7a850973", IsCheked = false},
                }
            };
            var seedData = new List<Allergen>()
            {
                new Allergen() { Id = "0df8be95-5c9b-4693-80e0-cb7c97216ed9", Name = "Рибе" },
                new Allergen() { Id = "3439585e-324d-4b2c-a921-5e7705f287f2", Name = "Мляко" },
                new Allergen() { Id = "9e07962d-9c64-4c27-b2ab-71ca8e592715", Name = "Сусам" },
                new Allergen() { Id = "a9edd26f-fb4e-4a0e-861d-c73e7a850973", Name = "Ядки" },
            }.AsQueryable<Allergen>();

            var mockSet = Fake.MockQueryable(seedData);

            var AlRepo = new Mock<IRepository<Allergen>>();

            AlRepo.Setup(x => x.All()).Returns(mockSet.Object);

            var seedPData = new List<Package>()
            {
                new Package() { Id = 1, Name = "Deleted", IsDeleted = true },
                new Package() { Id = 2, Name = "", IsDeleted = false },
                new Package() { Id = 3, Name = "Deleted", IsDeleted = true },
                new Package() { Id = 4, Name = "", IsDeleted = false },
            }.AsQueryable<Package>();

            var mockPSet = Fake.MockQueryable(seedPData);

            var PRepo = new Mock<IRepository<Package>>();

            PRepo.Setup(x => x.All()).Returns(mockPSet.Object);

            var seedCData = new List<Category>()
            {
                new Category() { Id = "0df8be95-5c9b-4693-80e0-cb7c97216ed9" },
                new Category() { Id = "3439585e-324d-4b2c-a921-5e7705f287f2" },
                new Category() { Id = "9e07962d-9c64-4c27-b2ab-71ca8e592715" },
                new Category() { Id = "a9edd26f-fb4e-4a0e-861d-c73e7a850973" },
            }.AsQueryable<Category>();

            var mockCSet = Fake.MockQueryable(seedCData);

            var CRepo = new Mock<IRepository<Category>>();

            CRepo.Setup(x => x.All()).Returns(mockCSet.Object);

            var service = new ProductService(AlRepo.Object, PRepo.Object, CRepo.Object, null, null);

            var result = await service.AddDropdownsCollectionsAsync(model);

            var expectedCategories = 4;
            var expectedPackages = 2;
            var expectedAllergens = 4;
            var expectedCheckedAllergens = 2;
            Assert.NotNull(result);
            Assert.Equal(result.Categories.Count, expectedCategories);
            Assert.Equal(result.Packages.Count, expectedPackages);
            Assert.Equal(result.Allergens.Count, expectedAllergens);
            Assert.Equal(result.Allergens.Where(x => x.IsCheked).Count(), expectedCheckedAllergens);
        }

        [Fact]
        public async Task CreateEditModelAsync_ShouldCreateModel()
        {

            ProductInputModel model = new ProductInputModel()
            {
                Allergens = new List<AllergensProductsInputModel>()
                {
                    new AllergensProductsInputModel(){ AllergenId = "0df8be95-5c9b-4693-80e0-cb7c97216ed9", IsCheked = true},
                    new AllergensProductsInputModel(){ AllergenId = "3439585e-324d-4b2c-a921-5e7705f287f2", IsCheked = true},
                    new AllergensProductsInputModel(){ AllergenId = "9e07962d-9c64-4c27-b2ab-71ca8e592715", IsCheked = false},
                    new AllergensProductsInputModel(){ AllergenId = "a9edd26f-fb4e-4a0e-861d-c73e7a850973", IsCheked = false},
                }
            };
            var seedData = new List<Allergen>()
            {
                new Allergen() { Id = "0df8be95-5c9b-4693-80e0-cb7c97216ed9", Name = "Рибе" },
                new Allergen() { Id = "3439585e-324d-4b2c-a921-5e7705f287f2", Name = "Мляко" },
                new Allergen() { Id = "9e07962d-9c64-4c27-b2ab-71ca8e592715", Name = "Сусам" },
                new Allergen() { Id = "a9edd26f-fb4e-4a0e-861d-c73e7a850973", Name = "Ядки" },
            }.AsQueryable();

            var mockSet = Fake.MockQueryable(seedData);

            var AlRepo = new Mock<IRepository<Allergen>>();

            AlRepo.Setup(x => x.All()).Returns(mockSet.Object);

            var seedPData = new List<Package>()
            {
                new Package() { Id = 1, Name = "Deleted", IsDeleted = true },
                new Package() { Id = 2, Name = "Кутия за салата", IsDeleted = false },
                new Package() { Id = 3, Name = "Deleted", IsDeleted = true },
                new Package() { Id = 4, Name = "", IsDeleted = false },
            }.AsQueryable();

            var mockPSet = Fake.MockQueryable(seedPData);

            var PRepo = new Mock<IRepository<Package>>();

            PRepo.Setup(x => x.All()).Returns(mockPSet.Object);

            var seedCData = new List<Category>()
            {
                new Category() { Id = "0df8be95-5c9b-4693-80e0-cb7c97216ed9" },
                new Category() { Id = "437e08ae-9dc5-4160-bd83-d5d235e2b5bf" },
                new Category() { Id = "9e07962d-9c64-4c27-b2ab-71ca8e592715" },
                new Category() { Id = "a9edd26f-fb4e-4a0e-861d-c73e7a850973" },
            }.AsQueryable();

            var mockCSet = Fake.MockQueryable(seedCData);

            var CRepo = new Mock<IRepository<Category>>();

            CRepo.Setup(x => x.All()).Returns(mockCSet.Object);


            var productId = "8e669251-0287-484a-88d6-618f5efb4a17";
            var prodData = new List<Product>()
            {
                new Product() { Id = "FakeId", Allergens = new List<AllergensProducts>(), Package = new Package() { Name = "Кутия за салата"} },
                new Product(){
                    Id = productId,
                    Name = "Цезар",
                    Description = "салата",
                    CategoryId = "437e08ae-9dc5-4160-bd83-d5d235e2b5bf",
                    Category = new Category(),
                    HasExtras = true,
                    ImageUrl = "vw5yvoappuqkuku6pkkk.jpg",
                    MaxProductsInPackage = 1,
                    Price = 9.60m,
                    Weight = 300,
                    PackageId = 2,
                    Package = new Package() { Name = "Кутия за салата"},
                    Allergens = new List<AllergensProducts>()
                    {
                        new AllergensProducts(){ AllergenId = "0df8be95-5c9b-4693-80e0-cb7c97216ed9", ProductId = productId, Allergen = new Allergen(){ Name = "Some Allergen" } },
                        new AllergensProducts(){ AllergenId = "3439585e-324d-4b2c-a921-5e7705f287f2", ProductId = productId, Allergen = new Allergen(){ Name = "Some Allergen" } },
                    },
                }
            }.AsQueryable();

            var mockProductSet = Fake.MockQueryable(prodData);

            var ProdRepo = new Mock<IRepository<Product>>();

            ProdRepo.Setup(x => x.All()).Returns(mockProductSet.Object);

            var service = new ProductService(AlRepo.Object, PRepo.Object, CRepo.Object, null, ProdRepo.Object);

            var result = await service.CreateEditModelAsync(productId);

            var expectedCategories = 4;
            var expectedPackages = 2;
            var expectedAllergens = 4;
            var expectedCheckedAllergens = 2;
            Assert.NotNull(result);
            Assert.Equal(result.Categories.Count, expectedCategories);
            Assert.Equal(result.Packages.Count, expectedPackages);
            Assert.Equal(result.Allergens.Count, expectedAllergens);
            Assert.Equal(result.Allergens.Where(x => x.IsCheked).Count(), expectedCheckedAllergens);
        }


        [Fact]
        public async Task CreateEditModelAsync_ShouldThrow()
        {
            var productId = "FakeId";
            var prodData = new List<Product>()
            {
                new Product() { Id = "a9edd26f-fb4e-4a0e-861d-c73e7a850973", Allergens = new List<AllergensProducts>(), Package = new Package() { Name = "Кутия за салата"} },
                new Product(){
                    Id = "8e669251-0287-484a-88d6-618f5efb4a17",
                    Name = "Цезар",
                    Description = "салата",
                    CategoryId = "437e08ae-9dc5-4160-bd83-d5d235e2b5bf",
                    Category = new Category(),
                    HasExtras = true,
                    ImageUrl = "vw5yvoappuqkuku6pkkk.jpg",
                    MaxProductsInPackage = 1,
                    Price = 9.60m,
                    Weight = 300,
                    PackageId = 2,
                    Package = new Package() { Name = "Кутия за салата"},
                    Allergens = new List<AllergensProducts>()
                    {
                        new AllergensProducts(){ AllergenId = "0df8be95-5c9b-4693-80e0-cb7c97216ed9", ProductId = productId, Allergen = new Allergen(){ Name = "Some Allergen" } },
                        new AllergensProducts(){ AllergenId = "3439585e-324d-4b2c-a921-5e7705f287f2", ProductId = productId, Allergen = new Allergen(){ Name = "Some Allergen" } },
                    },
                }
            }.AsQueryable();

            var mockProductSet = Fake.MockQueryable(prodData);

            var ProdRepo = new Mock<IRepository<Product>>();

            ProdRepo.Setup(x => x.All()).Returns(mockProductSet.Object);

            var service = new ProductService(null, null, null, null, ProdRepo.Object);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.CreateEditModelAsync(productId));

            var expectedMessage = "Няма такъв продукт";

            Assert.Equal(ex.Message, expectedMessage);
        }

        [Fact]
        public async Task CreateProductAsync_ShouldCreate()
        {
            var expectedImageUrl = "z5dusk1lrrqwmclxujpf.png";
            var cloudService = new Mock<ICloudinaryService>();
            cloudService.Setup(x => x.UploadImageAsync(It.IsAny<IFormFile>()).Result).Returns(expectedImageUrl);

            Product product = null;

            var prodRepo = new Mock<IRepository<Product>>();

            prodRepo.Setup(x => x.AddAsync(It.IsAny<Product>())).Callback<Product>(x => product = x);

            var service = new ProductService(null, null, null, cloudService.Object, prodRepo.Object);

            var mockIFormFile = new Mock<IFormFile>();

            var model = new ProductInputModel()
            {
                Name = "Цезар",
                Description = "салата",
                CategoryId = "437e08ae-9dc5-4160-bd83-d5d235e2b5bf",
                HasExtras = true,
                Image = mockIFormFile.Object,
                MaxProductsInPackage = 1,
                Price = 9.60m,
                Weight = 300,
                PackageId = 2,
                Allergens = new List<AllergensProductsInputModel>()
                    {
                        new AllergensProductsInputModel(){ AllergenId = "0df8be95-5c9b-4693-80e0-cb7c97216ed9", Name = "9e07962d-9c64-4c27-b2ab-71ca8e592715", IsCheked = true },
                        new AllergensProductsInputModel(){ AllergenId = "3439585e-324d-4b2c-a921-5e7705f287f2", Name = "a9edd26f-fb4e-4a0e-861d-c73e7a850973", IsCheked = true }
                    },
            };

            await service.CreateProductAsync(model);

            Assert.NotNull(product);
            Assert.Equal(product.Name, model.Name);
            Assert.Equal(product.Description, model.Description);
            Assert.Equal(product.CategoryId, model.CategoryId);
            Assert.Equal(product.HasExtras, model.HasExtras);
            Assert.Equal(product.ImageUrl, expectedImageUrl);
            Assert.Equal(product.MaxProductsInPackage, model.MaxProductsInPackage);
            Assert.Equal(product.Price, model.Price);
            Assert.Equal(product.Weight, model.Weight);
            Assert.Equal(product.PackageId, model.PackageId);
            Assert.NotNull(model.Allergens);
            var expectedAllergensCount = 2;
            Assert.Equal(product.Allergens.Count, expectedAllergensCount);

            prodRepo.Verify(x => x.AddAsync(It.IsAny<Product>()), Times.Once());
            prodRepo.Verify(x => x.SaveChangesAsync(), Times.Once());
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldThrow()
        {
            var prodRepo = new Mock<IRepository<Product>>();

            var query = Fake.MockQueryable(new List<Product>().AsQueryable());

            prodRepo.Setup(x => x.All()).Returns(query.Object);

            var fakeId = Guid.NewGuid().ToString();

            var service = new ProductService(null, null, null, null, prodRepo.Object);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.DeleteProductAsync(fakeId));

            var message = "Няма такъв продукт";

            Assert.Equal(message, ex.Message);
        }



        [Fact]
        public async Task DeleteProductAsync_ShouldDelete()
        {
            var productId = Guid.NewGuid().ToString();

            var prodRepo = new Mock<IRepository<Product>>();

            var seedData = new List<Product>()
            {
                new Product() { Id = productId },
            }.AsQueryable();

            var query = Fake.MockQueryable(seedData);

            prodRepo.Setup(x => x.All()).Returns(query.Object);

            var service = new ProductService(null, null, null, null, prodRepo.Object);

            await service.DeleteProductAsync(productId);

            prodRepo.Verify(x => x.SaveChangesAsync(), Times.Once());
        }

        [Fact]
        public async Task GetListWithProductsAsync_ShouldReturn()
        {
            var prodData = new List<Product>()
            {
                new Product() { Id = "", Name = "", IsDeleted = false, Category = new Category { Name = "" } },
                new Product() { Id = "", Name = "", IsDeleted = true, Category = new Category { Name = "" } },
                new Product() { Id = "", Name = "", IsDeleted = false, Category = new Category { Name = "" } },
            }.AsQueryable();

            var mockSet = Fake.MockQueryable(prodData);

            var prodRepo = new Mock<IRepository<Product>>();

            prodRepo.Setup(x => x.All()).Returns(mockSet.Object);

            var service = new ProductService(null, null, null, null, prodRepo.Object);

            var result = await service.GetListWithProductsAsync();

            Assert.NotNull(result);
            var expectedProductsCount = 2;
            Assert.Equal(result.Count, expectedProductsCount);
        }

        [Fact]
        public async Task EditProductAsync_ShouldEdit()
        {
            var expectedImageUrl = "z5dusk1lrrqwmclxujpf.png";
            var cloudService = new Mock<ICloudinaryService>();
            cloudService.Setup(x => x.UploadImageAsync(It.IsAny<IFormFile>()).Result).Returns(expectedImageUrl);

            var productId = Guid.NewGuid().ToString();
            var mockIFormFile = new Mock<IFormFile>();

            var model = new ProductEditModel()
            {
                Id = productId,
                Name = "Цезар",
                Description = "салата",
                CategoryId = "437e08ae-9dc5-4160-bd83-d5d235e2b5bf",
                HasExtras = true,
                Image = mockIFormFile.Object,
                MaxProductsInPackage = 1,
                Price = 9.60m,
                Weight = 300,
                PackageId = 2,
                Allergens = new List<AllergensProductsInputModel>()
                    {
                        new AllergensProductsInputModel(){ AllergenId = "0df8be95-5c9b-4693-80e0-cb7c97216ed9", Name = "9e07962d-9c64-4c27-b2ab-71ca8e592715", IsCheked = true },
                        new AllergensProductsInputModel(){ AllergenId = "3439585e-324d-4b2c-a921-5e7705f287f2", Name = "a9edd26f-fb4e-4a0e-861d-c73e7a850973", IsCheked = true }
                    },
            };

            var prodData = new List<Product>()
            {
                new Product() { Id = "FakeId", Allergens = new List<AllergensProducts>(), Package = new Package() { Name = "Кутия за салата"} },
                new Product(){ Id = productId }
            }.AsQueryable();

            var mockProductSet = Fake.MockQueryable(prodData);

            var prodRepo = new Mock<IRepository<Product>>();

            prodRepo.Setup(x => x.All()).Returns(mockProductSet.Object);

            Product product = null;

            prodRepo.Setup(x => x.Update(It.IsAny<Product>())).Callback<Product>(x => product = x);

            var service = new ProductService(null, null, null, cloudService.Object, prodRepo.Object);

            await service.EditProductAsync(model);

            Assert.NotNull(product);
            Assert.Equal(product.Name, model.Name);
            Assert.Equal(product.Description, model.Description);
            Assert.Equal(product.CategoryId, model.CategoryId);
            Assert.Equal(product.HasExtras, model.HasExtras);
            Assert.Equal(product.ImageUrl, expectedImageUrl);
            Assert.Equal(product.MaxProductsInPackage, model.MaxProductsInPackage);
            Assert.Equal(product.Price, model.Price);
            Assert.Equal(product.Weight, model.Weight);
            Assert.Equal(product.PackageId, model.PackageId);
            Assert.NotNull(model.Allergens);
            var expectedAllergensCount = 2;
            Assert.Equal(product.Allergens.Count, expectedAllergensCount);

            prodRepo.Verify(x => x.Update(It.IsAny<Product>()), Times.Once());
            prodRepo.Verify(x => x.SaveChangesAsync(), Times.Once());

            cloudService.Verify(x => x.UploadImageAsync(It.IsAny<IFormFile>()), Times.Once());
            cloudService.Verify(x => x.DeleteImageAsync(It.IsAny<string>()), Times.Once());
        }
        
        [Fact]
        public async Task EditProductAsync_ShouldThrow()
        {
            var productId = Guid.NewGuid().ToString();

            var model = new ProductEditModel(){ Id = productId };

            var prodData = new List<Product>().AsQueryable();

            var mockProductSet = Fake.MockQueryable(prodData);

            var prodRepo = new Mock<IRepository<Product>>();

            prodRepo.Setup(x => x.All()).Returns(mockProductSet.Object);

            var service = new ProductService(null, null, null, null, prodRepo.Object);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.EditProductAsync(model));

            var message = "Няма такъв продукт";

            Assert.Equal(message, ex.Message);
        }
    }
}
