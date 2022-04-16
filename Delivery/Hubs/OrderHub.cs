using Delivery.Core.Contracts;
using Delivery.Infrastructure.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Delivery.Hubs
{
    [Authorize(Roles = GlobalConstants.AdministratorName)]
    public class OrderHub : Hub
    {
        private readonly IOrdersService ordersService;
        private readonly ILogger<OrderHub> logger;
        private readonly IHubContext<UserOrdersHub> hubUser;

        public OrderHub(IOrdersService ordersService, ILogger<OrderHub> logger, IHubContext<UserOrdersHub> hubUser)
        {
            this.ordersService = ordersService;
            this.logger = logger;
            this.hubUser = hubUser;
        }

        public async Task OperatorChangeStatus(string status, string order, string setTime)
        {
            if (string.IsNullOrEmpty(status) || string.IsNullOrEmpty(order))
            {
                await this.Clients.Caller.SendAsync("OperatorAlertMessage", "Липсват данни за статуса или номера на поръчката!");
                return;
            }

            try
            {
                var userId = await ordersService.ChangeStatusAsync(status, order, setTime);
                await Clients.All.SendAsync("OperatorStatusChanged", order, status, userId);
                await hubUser.Clients.User(userId).SendAsync("UserStatusChanged", order, status);
            }
            catch (Exception e)
            {
                this.logger.LogInformation(e.Message);
                await this.Clients.Caller.SendAsync("OperatorAlertMessage", e.Message);
            }
        }
    }
}
