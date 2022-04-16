using Delivery.Core.Contracts;
using Delivery.Core.ViewModels.Rating;
using Microsoft.AspNetCore.SignalR;

namespace Delivery.Hubs
{
    public class UserOrdersHub : Hub
    {
        private readonly IOrdersService ordersService;
        private readonly ILogger<UserOrdersHub> logger;

        public UserOrdersHub(
            IOrdersService ordersService,
            ILogger<UserOrdersHub> logger)
        {
            this.ordersService = ordersService;
            this.logger = logger;
            
        }

        public async Task UserSetRating(List<RatingItemDto> ratings, string message)
        {
            try
            {
                await this.ordersService.SetRatingAsync(ratings, message);
                await this.Clients.Caller.SendAsync("SuccessfullySetRating");
            }
            catch (Exception e)
            {
                this.logger.LogInformation(e.Message);
            }
        }
    }
}
