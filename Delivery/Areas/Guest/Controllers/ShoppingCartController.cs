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
        public IActionResult Index(OrderInputModel model = null)
        {
            if (model == null)
            {
                model = new OrderInputModel();
            }
            return View(model);
        }

       
    }

}
