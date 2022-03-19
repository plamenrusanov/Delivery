using Delivery.Core.Contracts;
using Delivery.Core.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        // [Authorize(Roles = GlobalConstants.AdministratorName)]
        public IActionResult Index()
        {
            return View();
        }

        // [Authorize(Roles = GlobalConstants.AdministratorName)]
        public async Task<IActionResult> Create()
        {
            var model = new CategoryInputModel();
            return View(model);
        }

        [HttpPost]
        // [Authorize(Roles = GlobalConstants.AdministratorName)]
        public async Task<IActionResult> Create(CategoryInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }



            return RedirectToAction("Index");
        }
    }
}
