using Delivery.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Delivery.Test.FakeObjects
{
    internal class FakeDbContext<T> where T : class
    {
        public FakeDbContext(IQueryable<T> source)
        {
            //var mockSet = new Mock<DbSet<T>>();

            //mockSet.As<IAsyncEnumerable<T>>()
            //    .Setup(m => m.GetAsyncEnumerator(default))
            //    .Returns(new TestAsyncEnumerator<T>(source.GetEnumerator()));

            //mockSet.As<IQueryable<T>>()
            //    .Setup(m => m.Provider)
            //    .Returns(new TestAsyncQueryProvider<T>(source.Provider));

            //mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(source.Expression);
            //mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(source.ElementType);
            //mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => source.GetEnumerator());

            //var contextOptions = new DbContextOptions<DeliveryDbContext>();
            //var mockContext = new Mock<DeliveryDbContext>(contextOptions);
            //mockContext.Setup(c => c.Set<T>()).Returns(mockSet.Object);
        }
    }
}
