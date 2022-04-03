using Delivery.Core.ViewModels.Orders;
using Delivery.Infrastructure.Models;

namespace Delivery.Core.Contracts
{
    public interface IOrdersService
    {
        Task<bool> ValidateWithMirrorObjectAsync(OrderInputModel model);
        Task CreateOrderAsync(OrderInputModel model, DeliveryUser user);
    }
}
