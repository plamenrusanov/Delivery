using Delivery.Core.Constants;
using Delivery.Core.Contracts;
using Delivery.Core.ViewModels.Orders;
using Delivery.Core.ViewModels.ShoppingCart;
using Delivery.Hubs;
using Delivery.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Delivery.Areas.Guest.Controllers
{
    [Area("Guest")]
    public class OrdersController : Controller
    {
        private readonly IAddresesService addresesService;
        private readonly IOrdersService ordersService;
        private readonly UserManager<DeliveryUser> userManager;
        private readonly IHubContext<OrderHub> hubAdmin;

        public OrdersController(IAddresesService addresesService,
            IOrdersService ordersService,
            UserManager<DeliveryUser> userManager,
            IHubContext<OrderHub> hubAdmin)
        {
            this.addresesService = addresesService;
            this.ordersService = ordersService;
            this.userManager = userManager;
            this.hubAdmin = hubAdmin;
        }

        //[Authorize(GlobalConstants.AdministratorName)]
        public async Task<IActionResult> Index()
        {
            List<OrderViewModel> orders = await ordersService.GetDailyOrdersAsync();
            return this.View(orders);
        }

        public IActionResult Create()
        {
            var model = new OrderInputModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderInputModel model)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 418;
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(s => s.ErrorMessage);
                return Json(errors);
            }

            if (!await ordersService.ValidateWithMirrorObjectAsync(model))
            {
                return BadRequest();
            }

            try
            {
                DeliveryUser user = await GetUser(model);

                int orderId = await ordersService.CreateOrderAsync(model, user);
                await hubAdmin.Clients.All.SendAsync("OperatorNewOrder", orderId);
                this.TempData["NewOrder"] = true;
                return this.Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[Authorize(Roles = GlobalConstants.AdministratorName)]
        public async Task<IActionResult> Details(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                return this.NotFound();
            }

            try
            {
                OrderDetailsViewModel model = await ordersService.GetDetailsByIdAsync(orderId);
                return this.View(model);
            }
            catch (Exception e)
            {
                return this.NotFound();
            }
        }

        public async Task<AddressViewModel> GetAddressFromLocation(string latitude, string longitude)
        {
            if (string.IsNullOrEmpty(latitude) || string.IsNullOrEmpty(longitude))
            {
                return null;
            }

            try
            {
                AddressViewModel model = await addresesService.GetAddressAsync(latitude, longitude);
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IActionResult> UserOrders()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                if (this.Request.Cookies.ContainsKey(GlobalConstants.UserIdCookieKey))
                {
                    user = await this.userManager.FindByIdAsync(this.Request.Cookies[GlobalConstants.UserIdCookieKey]);
                }
            }

            try
            {
                IEnumerable<OrderViewModel> model = await this.ordersService.GetMyOrdersAsync(user);
                return this.View(model);
            }
            catch (Exception e)
            {
                //await this.logger.WriteException(e);
                return this.NotFound();
            }
        }

        public async Task<IActionResult> UserOrderDetails(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                return BadRequest();
            }



            try
            {
                if (int.TryParse(orderId, out int id))
                {
                    UserOrderDetailsViewModel model = await ordersService.GetUserDetailsByIdAsync(id);
                    return this.View(model);
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        private async Task<DeliveryUser> GetUser(OrderInputModel model)
        {
            DeliveryUser user;

            if (!User.Identity?.IsAuthenticated ?? true)
            {
                if (Request.Cookies.TryGetValue(GlobalConstants.UserIdCookieKey, out string? userId))
                {
                    user = await userManager.FindByIdAsync(userId);
                }
                else
                {
                    user = new DeliveryUser
                    {

                        UserName = model.Username,
                        PhoneNumber = model.Phone,
                        Email = $"{Request.HttpContext.Connection.RemoteIpAddress}@auto.com",
                        CreatedOn = DateTime.Now,
                    };

                    _ = await userManager.CreateAsync(user);
                }
            }
            else
            {
                user = await userManager.FindByNameAsync(User.Identity!.Name);
            }

            UpdateCookie(user.Id);
            return user;
        }

        private void UpdateCookie(string id)
        {
            CookieOptions cookieOptions = new()
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddYears(2),
                Secure = true,
            };

            Response.Cookies.Append(GlobalConstants.UserIdCookieKey, id, cookieOptions);
        }
    }
}
