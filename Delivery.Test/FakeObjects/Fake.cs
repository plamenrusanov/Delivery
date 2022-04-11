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
            var context = CreateDbContext(seedData);
            var mockRepo = new Repository<T>(context);
            return mockRepo;
        }
        
        public static Mock<Repository<T>> MockRepository<T>(IQueryable<T> seedData) where T : class
        {
            var mockContext = MockDbContext<T>(seedData);
            var mockRepo = new Mock<Repository<T>>(mockContext);
           
            return mockRepo;
        }

        public static DeliveryDbContext CreateDbContext<T>(IQueryable<T> seedData) where T : class
        {
            var provider = new TestAsyncQueryProvider<T>(seedData.Provider);

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

            return mockContext.Object;
        }
        
        public static Mock<DeliveryDbContext> MockDbContext<T>(IQueryable<T> seedData) where T : class
        {
            var provider = new TestAsyncQueryProvider<T>(seedData.Provider);

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

            return mockContext;
        }
    }
}
