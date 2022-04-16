using Delivery.Core.Contracts;
using Delivery.Core.ViewModels.Menu;
using Delivery.Core.ViewModels.Products;
using Delivery.Infrastructure.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Areas.Guest.Controllers
{
    [Area(GlobalConstants.AreaGuest)]
    public class MenuController : Controller
    {
        private const string Menu = "Меню";
        private readonly IMenuService menuService;

        public MenuController(IMenuService menuService)
        {
            this.menuService = menuService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                ViewData[GlobalConstants.viewDataTitle] = Menu;

                MenuViewModel model = await menuService.GetCategoriesWithProdutsAsync();

                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IActionResult> GetProductsByCategory(string categoryId)
        {
            try
            {
                ViewData[GlobalConstants.viewDataTitle] = Menu;

                MenuViewModel model = await menuService.GetCategoriesWithProdutsAsync(categoryId);

                ViewData[GlobalConstants.viewDataTitle] = model.Categories.Where(x => x.Id == categoryId).First().Name;

                return View(viewName: nameof(Index), model: model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IActionResult> ShowProduct(string productId)
        {
            try
            {
                ProductDetailsViewModel model = await menuService.GetProductDetailsAsync(productId);

                ViewData[GlobalConstants.viewDataTitle] = Menu;

                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
