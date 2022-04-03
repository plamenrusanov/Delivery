using Delivery.Core.ViewModels.Orders;

namespace Delivery.Core.Contracts
{
    public interface IOrdersService
    {
        Task<bool> ValidateWithMirrorObjectAsync(OrderInputModel model);
    }
}
