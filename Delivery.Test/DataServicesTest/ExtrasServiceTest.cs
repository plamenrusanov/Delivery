using AutoMapper;
using Delivery.Core.DataServices;
using Delivery.Core.ViewModels.Extras;
using Delivery.Infrastructure.Models;
using Delivery.Infrastructure.Repositories;
using Delivery.Test.FakeObjects;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Delivery.Test.DataServicesTest
{
    public class ExtrasServiceTest
    {
        [Fact]
        public async Task CreateExtraAsync_ShouldCreate()
        {
            var mockMapper = new Mock<IMapper>();

            var mockRepo = new Mock<IRepository<Extra>>();

            var service = new ExtrasService(mockRepo.Object, mockMapper.Object);

            ExtraInpitModel model = new();

            await service.AddExtraAsync(model);

            mockRepo.Verify(x => x.AddAsync(It.IsAny<Extra>()), Times.Once());

            mockRepo.Verify(x => x.SaveChangesAsync(), Times.Once());
        }

        [Fact]
        public async Task DeleteExtraAsync_ShouldThrow()
        {
            var extraId = 1;

            var mockMapper = new Mock<IMapper>();

            var service = new ExtrasService(Fake.CreateRepository<Extra>(new List<Extra>().AsQueryable()), mockMapper.Object);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.DeleteAsync(extraId));

            var expectedErrorMessage = "Добавката не съществува";

            Assert.Equal(ex.Message, expectedErrorMessage);
        }

        [Fact]
        public async Task DeleteExtraAsync_ShouldDelete()
        {
            var expectedId = 2;
            var seedData = new List<Extra>()
            {
                new Extra() { Id = 1 },
                new Extra() { Id = expectedId, Name = "Piper" },
                new Extra() { Id = 3 },
                new Extra() { Id = 4 },
            }.AsQueryable<Extra>();

            var mockCategoryRepo = new Mock<IRepository<Extra>>();

           var mockSet = Fake.MockQueryable(seedData);

            mockCategoryRepo.Setup(x => x.All()).Returns(mockSet.Object);

            var mockMapper = new Mock<IMapper>();

            var service = new ExtrasService(mockCategoryRepo.Object, mockMapper.Object);

            await service.DeleteAsync(expectedId);

            mockCategoryRepo.Verify(x => x.Delete(It.IsAny<Extra>()), Times.Once());

            mockCategoryRepo.Verify(x => x.SaveChangesAsync(), Times.Once());
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnsCorrectEntities()
        {
            var seedData = new List<Extra>()
            {
                new Extra() { Id = 1, Name = "", IsDeleted = true },
                new Extra() { Id = 2, Name = "", IsDeleted = false },
                new Extra() { Id = 3, Name = "", IsDeleted = true },
                new Extra() { Id = 4, Name = "", IsDeleted = false },
            }.AsQueryable<Extra>();

            Repository<Extra> mockRepo = Fake.CreateRepository<Extra>(seedData);
            var mockMapper = new Mock<IMapper>();
            var service = new ExtrasService(mockRepo, mockMapper.Object);

            var actualResult = await service.AllAsync();
            var expectedResult = 2;
            Assert.Equal(expectedResult, actualResult.Count);
        }

        [Fact]
        public async Task GetExtraEditModelAsync_ShouldCreateModel()
        {
            var expectedId = 2;
            var expectedName = "Пипер";
            var seedData = new List<Extra>()
            {
                new Extra() { Id = 1 },
                new Extra() { Id = expectedId, Name = expectedName },
                new Extra() { Id = 3 },
                new Extra() { Id = 4 },
            }.AsQueryable<Extra>();

            Repository<Extra> mockRepo = Fake.CreateRepository<Extra>(seedData);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<ExtraEditModel>(It.IsAny<Extra>()))
                 .Returns((Extra source) => new ExtraEditModel()
                 {
                     Id = source.Id,
                     Name = source.Name
                 });

            var service = new ExtrasService(mockRepo, mockMapper.Object);

            var actualResult = await service.GetEditModelAsync(expectedId);

            Assert.NotNull(actualResult);
            Assert.Equal(actualResult.Id, expectedId);
            Assert.Equal(actualResult.Name, expectedName);
        }


        [Fact]
        public async Task GetExtraEditModelAsync_ShouldThrowArgumentEx()
        {
            var Id = 5;
            var seedData = new List<Extra>()
            {
                new Extra() { Id = 1 },
                new Extra() { Id = 2 },
                new Extra() { Id = 3 },
                new Extra() { Id = 4 },
            }.AsQueryable<Extra>();

            Repository<Extra> mockRepo = Fake.CreateRepository<Extra>(seedData);
            var mockMapper = new Mock<IMapper>();
            var service = new ExtrasService(mockRepo, mockMapper.Object);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.GetEditModelAsync(Id));

            var expectedErrorMessage = "Добавката не съществува";

            Assert.Equal(ex.Message, expectedErrorMessage);

        }

        [Fact]
        public async Task UpdateExtraAsync_ShouldUpdate()
        {

            ExtraEditModel model = new()
            {
                Id = 2,
                Name = "Пица",
                Price = 1.20m,
                Weight = 50
            };

            var mockAllergenRepo = new Mock<IRepository<Extra>>();

            var mockMapper = new Mock<IMapper>();

            var service = new ExtrasService(mockAllergenRepo.Object, mockMapper.Object);

            await service.UpdateExtraAsync(model);

            mockAllergenRepo.Verify(x => x.Update(It.IsAny<Extra>()), Times.Once());

            mockAllergenRepo.Verify(x => x.SaveChangesAsync(), Times.Once());
        }
    }
}
