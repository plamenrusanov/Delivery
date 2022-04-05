using Delivery.Core.ViewModels.Orders;
using Delivery.Core.ViewModels.Rating;
using Delivery.Infrastructure.Models;

namespace Delivery.Core.Contracts
{
    public interface IOrdersService
    {
        Task<bool> ValidateWithMirrorObjectAsync(OrderInputModel model);
        Task<int> CreateOrderAsync(OrderInputModel model, DeliveryUser user);
        Task<IEnumerable<UserOrderViewModel>> GetMyOrdersAsync(DeliveryUser? user);
        Task<UserOrderDetailsViewModel> GetUserDetailsByIdAsync(int id);
        Task SetRatingAsync(List<RatingItemDto> ratings, string message);
        Task<string> ChangeStatusAsync(string status, string order, string setTime, string taxId);
    }
}
