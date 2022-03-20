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
        public async Task<IActionResult> Index()
        {
            List<CategoryViewModel> categories = await categoriesService.GetCategoriesWhitoutDeleted();
            return View(categories);
        }

        // [Authorize(Roles = GlobalConstants.AdministratorName)]
        public IActionResult Create()
        {
            CategoryInputModel model = new();

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

            try
            {
                await categoriesService.CreateCategoryAsync(model);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }
        }



        // [Authorize(Roles = GlobalConstants.AdministratorName)]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                CategoryEditModel model = await categoriesService.GetCategoryEditModelAsync(id);

                return View(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        // [Authorize(Roles = GlobalConstants.AdministratorName)]
        public async Task<IActionResult> Edit(CategoryEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await categoriesService.UpdateCategoryAsync(model);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }

        // [Authorize(Roles = GlobalConstants.AdministratorName)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await categoriesService.DeleteCategoryAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
