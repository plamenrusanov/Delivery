using Delivery.Core.DataServices;
using Delivery.Core.ViewModels.Orders;
using Delivery.Infrastructure.Models;
using Delivery.Infrastructure.Models.Enums;
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
    public class OrderServiceTest
    {

        [Theory]
        [InlineData("", "", "", "OrderId is null.")]
        [InlineData("", "invalid", "", "OrderId is not valid integer.")]
        [InlineData("", "1", "", "Status is not valid.")]
        [InlineData("Processed", "1", "", "MinutesForDelivery is not valid.")]
        [InlineData("Processed", "1", "40", "Order is not exist.")]
        public async Task ChangeStatusAsync_ShouldThrow(string status, string orderId, string setTime, string expectedResult)
        {
            var mockSet = Fake.MockQueryable(new List<Order>().AsQueryable());

            var orderRepo = new Mock<IRepository<Order>>();

            orderRepo.Setup(x => x.All()).Returns(mockSet.Object);

            var service = new OrdersService(null, null, orderRepo.Object, null, null);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.ChangeStatusAsync(status, orderId, setTime));

            Assert.Equal(expectedResult, ex.Message);
        }

        [Fact]
        public async Task ChangeStatusAsync_ShouldChange()
        {
            var setTime = "40";
            var status = "Processed";
            var expectedUserId = Guid.NewGuid().ToString();
            var orderId = 2;
            var seedData = new List<Order>()
            {
                new Order() { Id = 1 },
                new Order() { Id = orderId, DeliveryUserId = expectedUserId },
                new Order() { Id = 3 },
                new Order() { Id = 4 },
            }.AsQueryable<Order>();

            var mockSet = Fake.MockQueryable(seedData);

            var orderRepo = new Mock<IRepository<Order>>();

            orderRepo.Setup(x => x.All()).Returns(mockSet.Object);

            Order order = null;

            orderRepo.Setup(x => x.Update(It.IsAny<Order>())).Callback<Order>(x => order = x);

            var service = new OrdersService(null, null, orderRepo.Object, null, null);

            var result = await service.ChangeStatusAsync(status, orderId.ToString(), setTime);
            var expectedStatus = OrderStatus.Processed;
            Assert.NotNull(result);
            Assert.Equal(result, expectedUserId);
            Assert.NotNull(order);
            Assert.NotNull(order!.ProcessingTime);
            Assert.Equal(order!.Status, expectedStatus);

            orderRepo.Verify(x => x.Update(It.IsAny<Order>()), Times.Once());
            orderRepo.Verify(x => x.SaveChangesAsync(), Times.Once());
        }

        [Fact]
        public async Task CreateOrderAsync_ShouldCreate()
        {
            DeliveryUser user = new () { Id = Guid.NewGuid().ToString() };

            OrderInputModel model = new()
            {
                Username = "Pesho",
                Phone = "0987654321",
                AddInfoOrder = "",
                CutleryCount = 0,
                Address = new AddressInputModel()
                {
                    addInfo = "",
                    city = "Ruse",
                    borough = "Zdravec",
                    street = "Momina Sylza",
                    streetNumber = "4",
                    block = "34",
                    floor = "3",
                    displayName = "The address",
                    entry = "A",
                    latitude = "26.4549485",
                    longitude = "43.642635",
                },
                Cart = new List<ShoppingCartItemInputModel>()
                {
                    new ShoppingCartItemInputModel()
                    {
                        PId = Guid.NewGuid().ToString(),
                        Qty = 1,
                        Description = String.Empty,
                        Extras = new List<ExtraItemInputModel>()
                        {
                            new ExtraItemInputModel{ id = 1, qty = 2, name = "extra1" },
                            new ExtraItemInputModel{ id = 2, qty = 1, name = "extra2" },
                        },
                    }

                }
            };

            var userRepo = new Mock<IRepository<DeliveryUser>>();

            var service = new OrdersService(null, null, null, userRepo.Object, null);

            var result = await service.CreateOrderAsync(model, user);

            var expectedOrdersCount = 1;
            Assert.Equal(user.Orders.Count(), expectedOrdersCount);



            userRepo.Verify(x => x.SaveChangesAsync(), Times.Once());
        }
    }
}
