using Delivery.Core.Contracts;
using Delivery.Core.ViewModels.Allergens;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Areas.Admin.Controllers
{
    public class AllergensController : AdminController
    {
        private readonly IAllergensService allergensService;

        public AllergensController(IAllergensService allergensService)
        {
            this.allergensService = allergensService;
        }

        public async Task<IActionResult> Index()
        {
            List<AllergenViewModel> categories = await allergensService.GetAllergensWhitoutDeletedAsync();
            return View(categories);
        }

        public IActionResult Create()
        {
            AllergenInputModel model = new();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AllergenInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await allergensService.CreateAllergenAsync(model);
                return RedirectToAction("Index");
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        }

        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                AllergenEditModel model = await allergensService.GetAllergenEditModelAsync(id);

                return View(model);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AllergenEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await allergensService.UpdateAllergenAsync(model);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await allergensService.DeleteAllergenAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch(ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        }
    }
}
