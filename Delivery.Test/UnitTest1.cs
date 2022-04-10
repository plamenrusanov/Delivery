using Delivery.Core.DataServices;
using Delivery.Infrastructure.Data;
using Delivery.Infrastructure.Models;
using Delivery.Infrastructure.Repositories;
using Delivery.Test.FakeObjects;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Delivery.Test
{
    public class UnitTest1
    {
        [Fact]
        public async Task GetCompanyProductURLAsync_ReturnsNullForInvalidCompanyProduct()
        {
            var companyProducts = Enumerable.Empty<Allergen>().AsQueryable();

            var mockSet = new Mock<DbSet<Allergen>>();

            mockSet.As<IAsyncEnumerable<Allergen>>()
                .Setup(m => m.GetAsyncEnumerator(default))
                .Returns(new TestAsyncEnumerator<Allergen>(companyProducts.GetEnumerator()));

            mockSet.As<IQueryable<Allergen>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<Allergen>(companyProducts.Provider));

            mockSet.As<IQueryable<Allergen>>().Setup(m => m.Expression).Returns(companyProducts.Expression);
            mockSet.As<IQueryable<Allergen>>().Setup(m => m.ElementType).Returns(companyProducts.ElementType);
            mockSet.As<IQueryable<Allergen>>().Setup(m => m.GetEnumerator()).Returns(() => companyProducts.GetEnumerator());

            var contextOptions = new DbContextOptions<DeliveryDbContext>();
            var mockContext = new Mock<DeliveryDbContext>(contextOptions);
            mockContext.Setup(c => c.Set<Allergen>()).Returns(mockSet.Object);

            var entityRepository = new Repository<Allergen>(mockContext.Object);

            var service = new AllergensService(null, entityRepository);

            var result = await service.GetAllergensWhitoutDeletedAsync();

            Assert.Null(result);
        }
    }
}