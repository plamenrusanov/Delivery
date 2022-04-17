using Delivery.Core.DataServices;
using Delivery.Infrastructure.Models;
using Delivery.Infrastructure.Repositories;
using Delivery.Test.FakeObjects;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Delivery.Test.DataServicesTest
{
    public class MenuServiceTest
    {
        [Fact]
        public async Task GetCategoriesWithProdutsAsync_ShouldReturnOnlyCategories()
        {
            var seedData = new List<Category>()
            {
                new Category() { Id = "0df8be95-5c9b-4693-80e0-cb7c97216ed9", IsDeleted = false  },
                new Category() { Id = "3439585e-324d-4b2c-a921-5e7705f287f2", IsDeleted = true },
                new Category() { Id = "9e07962d-9c64-4c27-b2ab-71ca8e592715", IsDeleted = false  },
                new Category() { Id = "a9edd26f-fb4e-4a0e-861d-c73e7a850973", IsDeleted = false  },
            }.AsQueryable<Category>();

            var fakeRepo = Fake.CreateRepository<Category>(seedData);

            var mockProductsRepo = new Mock<IRepository<Product>>();

            var mockExtraRepo = new Mock<IRepository<Extra>>();

            var service = new MenuService(mockProductsRepo.Object, fakeRepo, mockExtraRepo.Object, null);

            var result = await service.GetCategoriesWithProdutsAsync(string.Empty);

            var expectedCategories = 3;
            var expectedProducts = 0;

            Assert.NotNull(result);
            Assert.Equal(result.Categories.Count, expectedCategories);
            Assert.Equal(result.Products.Count, expectedProducts);
        }
        
        
        [Fact]
        public async Task GetCategoriesWithProdutsAsync_ShouldReturnCategoriesAndProducts()
        {
            var selectedCategoryId = "9e07962d-9c64-4c27-b2ab-71ca8e592715";
            var seedData = new List<Category>()
            {
                new Category() { Id = "0df8be95-5c9b-4693-80e0-cb7c97216ed9", IsDeleted = false  },
                new Category() { Id = "3439585e-324d-4b2c-a921-5e7705f287f2", IsDeleted = true },
                new Category() { Id = selectedCategoryId, IsDeleted = false  },
                new Category() { Id = "a9edd26f-fb4e-4a0e-861d-c73e7a850973", IsDeleted = false  },
            }.AsQueryable<Category>();

            var fakeRepo = Fake.CreateRepository<Category>(seedData);

            var seedProducts = new List<Product>()
            {
                new Product() { Id = "0df8be95-5c9b-4693-80e0-cb7237216ed9", CategoryId = selectedCategoryId, IsDeleted = false  },
                new Product() { Id = "3439585e-324d-4b2c-a921-5e7235f287f2", CategoryId = selectedCategoryId, IsDeleted = true },
                new Product() { Id = "9e07962d-9c64-4c27-b2ab-71c23e592715", CategoryId = "3439585e-324d-4b2c-a921-5e7705f287f2", IsDeleted = false  },
                new Product() { Id = "a9edd26f-fb4e-4a0e-861d-c7323a850973", CategoryId = "3439585e-324d-4b2c-a921-5e7705f287f2", IsDeleted = false  },
            }.AsQueryable<Product>();

            var fakeProductsRepo = Fake.CreateRepository<Product>(seedProducts);

            var mockExtraRepo = new Mock<IRepository<Extra>>();
            
            var seedItems = new List<ShoppingCartItem>()
            {
                new ShoppingCartItem() { ProductId =  "0df8be95-5c9b-4693-80e0-cb7237216ed9" , Rating = 1 },
                new ShoppingCartItem() { ProductId =  "0df8be95-5c9b-4693-80e0-cb7237216ed9" , Rating = 5 },
                new ShoppingCartItem() { ProductId =  "0df8be95-5c9b-4693-80e0-cb7237216ed9" , Rating = 0  },
                new ShoppingCartItem() { ProductId =  "9e07962d-9c64-4c27-b2ab-71c23e592715" , Rating = 1  },
            }.AsQueryable<ShoppingCartItem>();

            var fakeItemsRepo = Fake.MockQueryable(seedItems);

            var mockItemRepo = new Mock<IRepository<ShoppingCartItem>>();

            mockItemRepo.Setup(x => x.All()).Returns(fakeItemsRepo.Object);

            var service = new MenuService(fakeProductsRepo, fakeRepo, mockExtraRepo.Object, mockItemRepo.Object);

            var result = await service.GetCategoriesWithProdutsAsync(selectedCategoryId);

            var expectedCategories = 3;
            var expectedProducts = 1;
            var expectedRating = 3;

            Assert.NotNull(result);
            Assert.Equal(result.Categories.Count, expectedCategories);
            Assert.Equal(result.Products.Count, expectedProducts);
            Assert.Equal(result.Products.First().Rating, expectedRating);
        }

        [Fact]
        public async Task GetProductDetailsAsync_ShouldThrowNull()
        {
            var mockProductsRepo = new Mock<IRepository<Product>>();

            var mockExtraRepo = new Mock<IRepository<Extra>>();

            var mockCategoryRepo = new Mock<IRepository<Category>>();

            var service = new MenuService(mockProductsRepo.Object, mockCategoryRepo.Object, mockExtraRepo.Object, null);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.GetProductDetailsAsync(string.Empty));

            var expectedErrorMessage = "Неверни данни";

            Assert.Equal(ex.Message, expectedErrorMessage);
        }
        
        [Fact]
        public async Task GetProductDetailsAsync_ShouldThrowNotExist()
        {
            var seedProducts = new List<Product>()
            {
                new Product() { Id = "0df8be95-5c9b-4693-80e0-cb7237216ed9", IsDeleted = false  },
                new Product() { Id = "3439585e-324d-4b2c-a921-5e7235f287f2", IsDeleted = true },
                new Product() { Id = "9e07962d-9c64-4c27-b2ab-71c23e592715", IsDeleted = false  },
                new Product() { Id = "a9edd26f-fb4e-4a0e-861d-c7323a850973", IsDeleted = false  },
            }.AsQueryable<Product>();

            var mockDbSet = Fake.MockQueryable<Product>(seedProducts);

            var mockProductsRepo = new Mock<IRepository<Product>>();

            mockProductsRepo.Setup(x => x.All()).Returns(mockDbSet.Object);

            var mockExtraRepo = new Mock<IRepository<Extra>>();

            var mockCategoryRepo = new Mock<IRepository<Category>>();

            var service = new MenuService(mockProductsRepo.Object, mockCategoryRepo.Object, mockExtraRepo.Object, null);

            var id = "someId";

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.GetProductDetailsAsync(id));

            var expectedErrorMessage = "Няма такъв продукт";

            Assert.Equal(ex.Message, expectedErrorMessage);
        }
        
        
        [Fact]
        public async Task GetProductDetailsAsync_ShouldReturnModel()
        {

            var selectedProductId = "9e07962d-9c64-4c27-b2ab-71c23e592715";
            var seedProducts = new List<Product>()
            {
                new Product() { Id = "0df8be95-5c9b-4693-80e0-cb7237216ed9", IsDeleted = false  },
                new Product() { Id = "3439585e-324d-4b2c-a921-5e7235f287f2", IsDeleted = true },
                new Product() { Id = selectedProductId, IsDeleted = false,
                    Allergens = new List<AllergensProducts>(),
                    Package = new Package() { Price = 1.20m } },
                new Product() { Id = "a9edd26f-fb4e-4a0e-861d-c7323a850973", IsDeleted = false  },
            }.AsQueryable<Product>();

            var mockDbSet = Fake.MockQueryable<Product>(seedProducts);

            var mockProductsRepo = new Mock<IRepository<Product>>();

            mockProductsRepo.Setup(x => x.All()).Returns(mockDbSet.Object);

            var seedExtra = new List<Extra>()
            {
                new Extra() { Id = 1, Name = "", IsDeleted = true },
                new Extra() { Id = 2, Name = "", IsDeleted = false },
                new Extra() { Id = 3, Name = "", IsDeleted = true },
                new Extra() { Id = 4, Name = "", IsDeleted = false },
            }.AsQueryable<Extra>();


            var setExtras = Fake.MockQueryable(seedExtra);

            var mockExtraRepo = new Mock<IRepository<Extra>>();

            mockExtraRepo.Setup(x => x.All()).Returns(setExtras.Object);

            var mockCategoryRepo = new Mock<IRepository<Category>>();

            var service = new MenuService(mockProductsRepo.Object, mockCategoryRepo.Object, mockExtraRepo.Object, null);

            var result = await service.GetProductDetailsAsync(selectedProductId);
            var expectedAllergenCount = 0;
            var expectedExtraCount = 2;
            Assert.NotNull(result);
            Assert.Equal(result.Allergens?.Count, expectedAllergenCount);
            Assert.Equal(result.Extras?.Count, expectedExtraCount);

        }

    }
}
