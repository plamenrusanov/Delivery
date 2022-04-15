using AutoMapper;
using Delivery.Core.DataServices;
using Delivery.Core.ViewModels.Packagies;
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
    public class PackageServiceTest
    {
        [Fact]
        public async Task CreatePackageAsync_ShouldCreate()
        {
            var mockMapper = new Mock<IMapper>();
            var mockRepo = new Mock<IRepository<Package>>();
            var service = new PackagesService(mockRepo.Object, mockMapper.Object);
            PackageInputModel model = new();

            await service.CreatePackageAsync(model);

            mockRepo.Verify(x => x.AddAsync(It.IsAny<Package>()), Times.Once());

            mockRepo.Verify(x => x.SaveChangesAsync(), Times.Once());
        }

        [Fact]
        public async Task DeletePackageAsync_ShouldThrow()
        {
            var packageId = 1;

            var service = new PackagesService(Fake.CreateRepository<Package>(new List<Package>().AsQueryable()), null);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.DeletePackageAsync(packageId));

            var expectedErrorMessage = "Невалидна стойност";

            Assert.Equal(ex.Message, expectedErrorMessage);
        }

        [Fact]
        public async Task DeletePackageAsync_ShouldDelete()
        {
            var expectedId = 2;
            var seedData = new List<Package>()
            {
                new Package() { Id = 1 },
                new Package() { Id = expectedId, Name = "Кутия за пица" },
                new Package() { Id = 3 },
                new Package() { Id = 4 },
            }.AsQueryable<Package>();

            var mockSet = Fake.MockQueryable(seedData);

            var mockPackageRepo = new Mock<IRepository<Package>>();

            mockPackageRepo.Setup(x => x.All()).Returns(mockSet.Object);

            var service = new PackagesService(mockPackageRepo.Object, null);

            await service.DeletePackageAsync(expectedId);

            mockPackageRepo.Verify(x => x.SaveChangesAsync(), Times.Once());
        }

        [Fact]
        public async Task GetPackagesWhitoutDeletedAsync_ShouldReturnsCorrectEntities()
        {
            var seedData = new List<Package>()
            {
                new Package() { Id = 1, Name = "", IsDeleted = true },
                new Package() { Id = 2, Name = "", IsDeleted = false },
                new Package() { Id = 3, Name = "", IsDeleted = true },
                new Package() { Id = 4, Name = "", IsDeleted = false },
            }.AsQueryable<Package>();

            Repository<Package> mockRepo = Fake.CreateRepository<Package>(seedData);

            var service = new PackagesService(mockRepo, null);

            var actualResult = await service.GetPackagesWhitoutDeletedAsync();
            var expectedResult = 2;
            Assert.Equal(expectedResult, actualResult.Count);
        }

        [Fact]
        public async Task GetPackageEditModelAsync_ShouldCreateModel()
        {
            var expectedId = 2;
            var expectedName = "Опаковка за супа";
            var expectedPrice = 0.50m;
            var seedData = new List<Package>()
            {
                new Package() { Id = 1 },
                new Package() { Id = expectedId, Name = expectedName, Price = expectedPrice },
                new Package() { Id = 3 },
                new Package() { Id = 4 },
            }.AsQueryable<Package>();
               
            Repository<Package> mockRepo = Fake.CreateRepository<Package>(seedData);
            
            var service = new PackagesService(mockRepo, null);

            var model = await service.GetPackageEditModelAsync(2);

            Assert.NotNull(model);
            Assert.Equal(model.Id, expectedId);
            Assert.Equal(model.Name, expectedName);
            Assert.Equal(model.Price, expectedPrice);
        }

        [Fact]
        public async Task UpdatePackageAsync_ShouldUpdate()
        {

            PackageEditModel model = new()
            {
                Id = 1,
                Name = "Кутия за пица",
            };

            var mockPackageRepo = new Mock<IRepository<Package>>();

            var mockMapper = new Mock<IMapper>();

            var service = new PackagesService(mockPackageRepo.Object, mockMapper.Object);

            await service.UpdatePackageAsync(model);

            mockPackageRepo.Verify(x => x.Update(It.IsAny<Package>()), Times.Once());

            mockPackageRepo.Verify(x => x.SaveChangesAsync(), Times.Once());
        }
    }
}
