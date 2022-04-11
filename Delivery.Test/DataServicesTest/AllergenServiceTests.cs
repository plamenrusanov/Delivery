using Delivery.Core.Contracts;
using Delivery.Core.DataServices;
using Delivery.Core.ViewModels.Allergens;
using Delivery.Infrastructure.Models;
using Delivery.Infrastructure.Repositories;
using Delivery.Test.FakeObjects;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Delivery.Test.DataServicesTest
{
    public class AllergenServiceTests
    {
        [Fact]
        public async Task DeleteAllergenAsync_ShouldThrow()
        {
            var allergenId = "3439585e-324d-4b2c-a921-5e7705f287f2";

            var service = new AllergensService(null, Fake.CreateRepository<Allergen>(new List<Allergen>().AsQueryable()));

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.DeleteAllergenAsync(allergenId));

            var expectedErrorMessage = "Алергена не е намерен в базата!";

            Assert.Equal(ex.Message, expectedErrorMessage);
        }

        [Fact]
        public async Task DeleteAllergenAsync_ShouldDelete()
        {
            var expectedId = "3439585e-324d-4b2c-a921-5e7705f287f2";
            var expectedName = "Мляко";
            var expectedImageUrl = "z5dusk1lrrqwmclxujpf.png";
            var seedData = new List<Allergen>()
            {
                new Allergen() { Id = "0df8be95-5c9b-4693-80e0-cb7c97216ed9" },
                new Allergen() { Id = expectedId, Name = expectedName, ImageUrl = expectedImageUrl },
                new Allergen() { Id = "9e07962d-9c64-4c27-b2ab-71ca8e592715" },
                new Allergen() { Id = "a9edd26f-fb4e-4a0e-861d-c73e7a850973" },
            }.AsQueryable<Allergen>();

            var mockCloudinaryService = new Mock<ICloudinaryService>();

            mockCloudinaryService.Setup(x => x.DeleteImageAsync(It.IsAny<string>())).Returns(Task.CompletedTask);

            var service = new Mock<AllergensService>(mockCloudinaryService.Object, Fake.CreateRepository(seedData));

            await service.Object.DeleteAllergenAsync(expectedId);

            mockCloudinaryService.Verify(x => x.DeleteImageAsync(It.IsAny<string>()), Times.Once());

        }

        [Fact]
        public async Task CreateAllergenAsync_ShouldCreate()
        {
            var imageUrl = "z5dusk1lrrqwmclxujpf.png";

            AllergenInputModel model = new()
            {
                Name = "Мляко",
                Image = new Mock<IFormFile>().Object
            };

            var mockCloudinaryService = new Mock<ICloudinaryService>();

            mockCloudinaryService.Setup(x => x.UploadImageAsync(It.IsAny<IFormFile>()).Result).Returns(imageUrl);

            var mockAllergenRepo = new Mock<IRepository<Allergen>>();

            Allergen allergen = null;

            mockAllergenRepo.Setup(x => x.AddAsync(It.IsAny<Allergen>()))
                .Callback<Allergen>(x => allergen = x)
                .Returns(Task.CompletedTask);

            mockAllergenRepo.Setup(x => x.SaveChangesAsync().Result).Returns(1);


            var service = new AllergensService(mockCloudinaryService.Object, mockAllergenRepo.Object);

            await service.CreateAllergenAsync(model);

            mockCloudinaryService.Verify(x => x.UploadImageAsync(It.IsAny<IFormFile>()), Times.Once());

            mockAllergenRepo.Verify(x => x.AddAsync(It.IsAny<Allergen>()), Times.Once());

            mockAllergenRepo.Verify(x => x.SaveChangesAsync(), Times.Once());

            Assert.NotNull(allergen);

            Assert.Equal(allergen?.Name, model.Name);

            Assert.Equal(allergen?.ImageUrl, imageUrl);
        }

        [Fact]
        public async Task UpdateAllergenAsync_ShouldUpdate()
        {
            var expectedId = "3439585e-324d-4b2c-a921-5e7705f287f2";
            var expectedName = "Мляко";
            var expectedImageUrl = "z5dusk1lrrqwmclxujpf.png";
            var seedData = new List<Allergen>()
            {
                new Allergen() { Id = "0df8be95-5c9b-4693-80e0-cb7c97216ed9" },
                new Allergen() { Id = expectedId, Name = expectedName, ImageUrl = expectedImageUrl },
                new Allergen() { Id = "9e07962d-9c64-4c27-b2ab-71ca8e592715" },
                new Allergen() { Id = "a9edd26f-fb4e-4a0e-861d-c73e7a850973" },
            }.AsQueryable<Allergen>();

            var expectedImage = "expectedUrl";

            AllergenEditModel model = new()
            {
                Id = "3439585e-324d-4b2c-a921-5e7705f287f2",
                Name = "Мляко",
                FormFile = new Mock<IFormFile>().Object,
                ImageUrl = "z5dusk1lrrqwmclxujpf.png"
            };

            var mockCloudinaryService = new Mock<ICloudinaryService>();

            mockCloudinaryService.Setup(x => x.UploadImageAsync(It.IsAny<IFormFile>()).Result).Returns(expectedImage);

            var mockAllergenRepo = new Mock<IRepository<Allergen>>();

            var provider = new TestAsyncQueryProvider<Allergen>(seedData.Provider);

            var mockSet = new Mock<IQueryable<Allergen>>();
            mockSet.As<IAsyncEnumerable<Allergen>>()
                .Setup(m => m.GetAsyncEnumerator(default))
                .Returns(new TestAsyncEnumerator<Allergen>(seedData.GetEnumerator()));
            mockSet.As<IQueryable<Allergen>>()
                .Setup(m => m.Provider)
                .Returns(provider);
            mockSet.As<IQueryable<Allergen>>().Setup(m => m.Expression).Returns(seedData.Expression);
            mockSet.As<IQueryable<Allergen>>().Setup(m => m.ElementType).Returns(seedData.ElementType);
            mockSet.As<IQueryable<Allergen>>().Setup(m => m.GetEnumerator()).Returns(() => seedData.GetEnumerator());

            mockAllergenRepo.Setup(x => x.All()).Returns(mockSet.Object);

            mockAllergenRepo
                .As<IRepository<Allergen>>()
                .Setup(x => x.Update(It.IsAny<Allergen>()))
                .Callback((Allergen a) => { return; })
                .Verifiable();

            var service = new AllergensService(mockCloudinaryService.Object, mockAllergenRepo.Object);

            await service.UpdateAllergenAsync(model);

            mockAllergenRepo.Verify(x => x.Update(It.IsAny<Allergen>()), Times.Once());

            mockAllergenRepo.Verify(x => x.SaveChangesAsync(), Times.Once());

            mockCloudinaryService.Verify(x => x.UploadImageAsync(It.IsAny<IFormFile>()), Times.Once());

            mockCloudinaryService.Verify(x => x.DeleteImageAsync(It.IsAny<string>()), Times.Once());
        }
         [Fact]
        public async Task UpdateAllergenAsync_ShouldThrow()
        {
         
            AllergenEditModel model = new()
            {
                Id = "3439585e-324d-4b2c-a921-5e7705f287f2",
                Name = "Мляко",
                FormFile = new Mock<IFormFile>().Object,
                ImageUrl = "z5dusk1lrrqwmclxujpf.png"
            };

            var service = new AllergensService(null, Fake.CreateRepository(new List<Allergen>().AsQueryable()));

            var ex =  await Assert.ThrowsAsync<ArgumentException>(() => service.UpdateAllergenAsync(model));

            var expectedErrorMessage = "Алергена не е намерен в базата!";

            Assert.Equal(ex.Message, expectedErrorMessage);
        }
        
        [Fact]
        public async Task GetAllergenEditModelAsync_ReturnsCorectValue()
        {
            var expectedId = "3439585e-324d-4b2c-a921-5e7705f287f2";
            var expectedName = "Мляко";
            var expectedImageUrl = "z5dusk1lrrqwmclxujpf.png";
            var seedData = new List<Allergen>()
            {
                new Allergen() { Id = "0df8be95-5c9b-4693-80e0-cb7c97216ed9" },
                new Allergen() { Id = expectedId, Name = expectedName, ImageUrl = expectedImageUrl },
                new Allergen() { Id = "9e07962d-9c64-4c27-b2ab-71ca8e592715" },
                new Allergen() { Id = "a9edd26f-fb4e-4a0e-861d-c73e7a850973" },
            }.AsQueryable<Allergen>();


            Repository<Allergen> mockRepo = Fake.CreateRepository<Allergen>(seedData);

            var service = new AllergensService(null, mockRepo);

            var actualResult = await service.GetAllergenEditModelAsync(expectedId);

            Assert.NotNull(actualResult);
            Assert.Equal(actualResult.Id, expectedId);
            Assert.Equal(actualResult.Name, expectedName);
            Assert.Equal(actualResult.ImageUrl, expectedImageUrl);
            Assert.Null(actualResult.FormFile);
        }

        [Fact]
        public async Task GetAllergenEditModelAsync_ShouldThrow()
        {
            var allergenId = "3439585e-324d-4b2c-a921-5e7705f287f2";

            var service = new AllergensService(null, Fake.CreateRepository<Allergen>(new List<Allergen>().AsQueryable()));

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.GetAllergenEditModelAsync(allergenId));

            var expectedErrorMessage = "Алергена не е намерен в базата!";

            Assert.Equal(ex.Message, expectedErrorMessage);
        }

        [Fact]
        public async Task GetAllergensWhitoutDeletedAsync_ReturnsCorrectEntities()
        {

            var seedData = new List<Allergen>()
            {
                new Allergen() { IsDeleted = true },
                new Allergen() { IsDeleted = false },
                new Allergen() { IsDeleted = true },
                new Allergen() { IsDeleted = false },
            }.AsQueryable<Allergen>();

            Repository<Allergen> mockRepo = Fake.CreateRepository<Allergen>(seedData);

            var service = new AllergensService(null, mockRepo);

                var actualResult = await service.GetAllergensWhitoutDeletedAsync();
                var expectedResult = 2;
                Assert.Equal(expectedResult, actualResult.Count);


        }
    }
}
