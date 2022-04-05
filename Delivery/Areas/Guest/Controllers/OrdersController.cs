using Delivery.Core.Constants;
using Delivery.Core.Contracts;
using Delivery.Core.ViewModels.Orders;
using Delivery.Core.ViewModels.ShoppingCart;
using Delivery.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Areas.Guest.Controllers
{
    [Area("Guest")]
    public class OrdersController : Controller
    {
        private readonly IAddresesService addresesService;
        private readonly IOrdersService ordersService;
        private readonly UserManager<DeliveryUser> userManager;

        public OrdersController(IAddresesService addresesService,
            IOrdersService ordersService,
            UserManager<DeliveryUser> userManager)
        {
            this.addresesService = addresesService;
            this.ordersService = ordersService;
            this.userManager = userManager;
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

                await ordersService.CreateOrderAsync(model, user);
                //await this.hubAdmin.Clients.All.SendAsync("OperatorNewOrder", id);
                this.TempData["NewOrder"] = true;
                return this.Ok();
            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction("Index");
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
                IEnumerable<UserOrderViewModel> model = await this.ordersService.GetMyOrdersAsync(user);
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
