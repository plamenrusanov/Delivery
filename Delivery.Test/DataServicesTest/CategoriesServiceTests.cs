using AutoMapper;
using Delivery.Core.DataServices;
using Delivery.Core.ViewModels.Categories;
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
    public class CategoriesServiceTests
    {

        [Fact]
        public async Task CreateCategoryAsync_ShouldCreate()
        {
            var mockMapper = new Mock<IMapper>();
            var mockRepo = new Mock<IRepository<Category>>();
            var service = new CategoriesService(mockRepo.Object, mockMapper.Object);
            CategoryInputModel model = new ();

            await service.CreateCategoryAsync(model);

            mockRepo.Verify(x => x.AddAsync(It.IsAny<Category>()), Times.Once());

            mockRepo.Verify(x => x.SaveChangesAsync(), Times.Once());
        }

        [Fact]
        public async Task DeleteCategoryAsync_ShouldThrow()
        {
            var categoryId = "3439585e-324d-4b2c-a921-5e7705f287f2";

            var service = new CategoriesService(Fake.CreateRepository<Category>(new List<Category>().AsQueryable()), null);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.DeleteCategoryAsync(categoryId));

            var expectedErrorMessage = "Невалидни параметри!";

            Assert.Equal(ex.Message, expectedErrorMessage);
        }

        [Fact]
        public async Task DeleteCategoryAsync_ShouldDelete()
        {
            var expectedId = "3439585e-324d-4b2c-a921-5e7705f287f2";
            var seedData = new List<Category>()
            {
                new Category() { Id = "0df8be95-5c9b-4693-80e0-cb7c97216ed9" },
                new Category() { Id = expectedId, Name = "Пица" },
                new Category() { Id = "9e07962d-9c64-4c27-b2ab-71ca8e592715" },
                new Category() { Id = "a9edd26f-fb4e-4a0e-861d-c73e7a850973" },
            }.AsQueryable<Category>();

            var mockCategoryRepo = new Mock<IRepository<Category>>();

            var provider = new TestAsyncQueryProvider<Category>(seedData.Provider);

            var mockSet = new Mock<IQueryable<Category>>();
            mockSet.As<IAsyncEnumerable<Category>>()
                .Setup(m => m.GetAsyncEnumerator(default))
                .Returns(new TestAsyncEnumerator<Category>(seedData.GetEnumerator()));
            mockSet.As<IQueryable<Category>>()
                .Setup(m => m.Provider)
                .Returns(provider);
            mockSet.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(seedData.Expression);
            mockSet.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(seedData.ElementType);
            mockSet.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(() => seedData.GetEnumerator());

            mockCategoryRepo.Setup(x => x.All()).Returns(mockSet.Object);

            var service = new CategoriesService(mockCategoryRepo.Object, null);

            await service.DeleteCategoryAsync(expectedId);

            mockCategoryRepo.Verify(x => x.Delete(It.IsAny<Category>()), Times.Once());

            mockCategoryRepo.Verify(x => x.SaveChangesAsync(), Times.Once());


        }

    }
}
