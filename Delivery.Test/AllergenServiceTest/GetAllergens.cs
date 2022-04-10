using Delivery.Core.DataServices;
using Delivery.Core.ViewModels.Allergens;
using Delivery.Infrastructure.Data;
using Delivery.Infrastructure.Models;
using Delivery.Infrastructure.Repositories;
using Delivery.Test.FakeObjects;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Delivery.Test.AllergenServiceTest
{
    public class GetAllergens
    {
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

            try
            {
                var actualResult = await service.GetAllergensWhitoutDeletedAsync();
                var expectedResult = 2;
                Assert.Equal(expectedResult, actualResult.Count);
            }
            catch (Exception e)
            {

                throw;
            }

        }
    }
}
