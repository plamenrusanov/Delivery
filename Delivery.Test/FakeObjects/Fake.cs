using Delivery.Infrastructure.Data;
using Delivery.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace Delivery.Test.FakeObjects
{
    public class Fake
    {
        public static Repository<T> CreateRepository<T>(IQueryable<T> seedData) where T : class
        {
            var provider = new TestAsyncQueryProvider<T>(seedData.Provider);
            //var mockProvider = new Mock<TestAsyncQueryProvider<T>>(seedData.Provider);
            //mockProvider.Setup(x => x.CreateQuery(It.IsAny<Expression>())).Returns(provider.CreateQuery(seedData.Expression));
            //mockProvider.Setup(x => x.CreateQuery<T>(It.IsAny<Expression>())).Returns(provider.CreateQuery<T>(seedData.Expression));
            //mockProvider.Setup(x => x.Execute(It.IsAny<Expression>())).Returns(provider.Execute(seedData.Expression));
            //mockProvider.Setup(x => x.Execute<T>(It.IsAny<Expression>())).Returns(provider.Execute<T>(seedData.Expression));
            //mockProvider.Setup(x => x.ExecuteAsync<T>(It.IsAny<Expression>(), It.IsAny<CancellationToken>()).Result)
            //    .Returns(provider.Execute<T>(seedData.Expression));


            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IAsyncEnumerable<T>>()
                .Setup(m => m.GetAsyncEnumerator(default))
                .Returns(new TestAsyncEnumerator<T>(seedData.GetEnumerator()));
            mockSet.As<IQueryable<T>>()
                .Setup(m => m.Provider)
                .Returns(provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(seedData.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(seedData.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => seedData.GetEnumerator());

            var contextOptions = new DbContextOptions<DeliveryDbContext>();
            var mockContext = new Mock<DeliveryDbContext>(contextOptions);
            mockContext.Setup(c => c.Set<T>()).Returns(mockSet.Object);

            var mockRepo = new Repository<T>(mockContext.Object);
            return mockRepo;
        }
    }
}
