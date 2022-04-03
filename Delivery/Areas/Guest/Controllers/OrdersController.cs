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
                DeliveryUser user;

                if (!User.Identity?.IsAuthenticated ?? true)
                {
                    if (Request.Cookies.TryGetValue("uid", out string? userId))
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

                await ordersService.CreateOrderAsync(model, user);
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

        private void UpdateCookie(string id)
        {
            CookieOptions cookieOptions = new()
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddYears(2),
                Secure = true,
            };

            Response.Cookies.Append("uid", id, cookieOptions);
        }
    }
}
