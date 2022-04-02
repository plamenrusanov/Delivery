using Delivery.Core.Contracts;
using Delivery.Core.ViewModels.Orders;
using Delivery.Core.ViewModels.ShoppingCart;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Areas.Guest.Controllers
{
    [Area("Guest")]
    public class ShoppingCartController : Controller
    {
        private readonly IAddresesService addresesService;

        public ShoppingCartController(IAddresesService addresesService)
        {
            this.addresesService = addresesService;
        }
        public IActionResult Index()
        {
            var model = new OrderInputModel();
            return View(model);
        }

        public async Task<AddressInputModel> GetAddressFromLocation(string latitude, string longitude)
        {
            if (string.IsNullOrEmpty(latitude) || string.IsNullOrEmpty(longitude))
            {
                return null;
            }


            try
            {
                AddressInputModel model = await addresesService.GetAddressAsync(latitude, longitude);
                return model;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }

}
