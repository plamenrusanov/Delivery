using Delivery.Core.Constants;
using Delivery.Core.Contracts;
using Delivery.Core.ViewModels.Menu;
using Delivery.Core.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Areas.Guest.Controllers
{
    [Area("Guest")]
    public class MenuController : Controller
    {
        private readonly IMenuService menuService;

        public MenuController(IMenuService menuService)
        {
            this.menuService = menuService;
        }
        public async Task<IActionResult> Index()
        {
            ViewData[GlobalConstants.viewDataTitle] = "Menu";
            MenuViewModel model = await menuService.GetCategoriesWithProdutsAsync();
            return View(model);
        }

        public async Task<IActionResult> GetProductsByCategory(string categoryId)
        {
            MenuViewModel model = await menuService.GetCategoriesWithProdutsAsync(categoryId);
            ViewData[GlobalConstants.viewDataTitle] = model.Categories.Where(x => x.Id == categoryId).First().Name;
            return View(viewName: nameof(Index), model: model);
        }

        public async Task<IActionResult> ShowProduct(string productId)
        {
            try
            {
                ProductDetailsViewModel model = await menuService.GetProductDetailsAsync(productId);
                ViewData[GlobalConstants.viewDataTitle] = "";
                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
