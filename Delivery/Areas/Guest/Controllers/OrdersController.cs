using Delivery.Core.Contracts;
using Delivery.Core.ViewModels.Orders;
using Delivery.Core.ViewModels.ShoppingCart;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Areas.Guest.Controllers
{
    [Area("Guest")]
    public class OrdersController : Controller
    {
        private readonly IAddresesService addresesService;
        private readonly IOrdersService ordersService;

        public OrdersController(IAddresesService addresesService,
            IOrdersService ordersService)
        {
            this.addresesService = addresesService;
            this.ordersService = ordersService;
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
                var errors  = ModelState.Values.SelectMany(x => x.Errors).Select(s => s.ErrorMessage);
                return Json(errors);
            }

            if (!await ordersService.ValidateWithMirrorObjectAsync(model))
            {
                return BadRequest();
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
    }
}
