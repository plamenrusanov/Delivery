using Delivery.Core.Contracts;
using Delivery.Core.ViewModels.Packagies;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PackageController : Controller
    {
        private readonly IPackagesService packageService;

        public PackageController(IPackagesService packageService)
        {
            this.packageService = packageService;
        }

        // [Authorize(Roles = GlobalConstants.AdministratorName)]
        public async Task<IActionResult> Index()
        {
            List<PackageViewModel> categories = await packageService.GetPackagesWhitoutDeletedAsync();
            return View(categories);
        }

        // [Authorize(Roles = GlobalConstants.AdministratorName)]
        public IActionResult Create()
        {
            PackageInputModel model = new();

            return View(model);
        }

        [HttpPost]
        // [Authorize(Roles = GlobalConstants.AdministratorName)]
        public async Task<IActionResult> Create(PackageInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await packageService.CreatePackageAsync(model);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        }



        // [Authorize(Roles = GlobalConstants.AdministratorName)]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                PackageEditModel model = await packageService.GetPackageEditModelAsync(id);

                return View(model);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        }

        [HttpPost]
        // [Authorize(Roles = GlobalConstants.AdministratorName)]
        public async Task<IActionResult> Edit(PackageEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await packageService.UpdatePackageAsync(model);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        }

        // [Authorize(Roles = GlobalConstants.AdministratorName)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await packageService.DeletePackageAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        }
    }
}
