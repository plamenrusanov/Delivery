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

namespace Delivery.Test.AllergenServiceTest
{
    public class GetAllergenEditModelAsyncTest
    {
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
    }
}
