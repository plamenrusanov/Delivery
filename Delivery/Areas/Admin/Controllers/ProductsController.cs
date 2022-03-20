using Delivery.Core.Constants;
using Delivery.Core.Contracts;
using Delivery.Core.ViewModels.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        // [Authorize(Roles = GlobalConstants.AdministratorName)]
        public IActionResult Index()
        {
            return View();
        }

        // [Authorize(Roles = GlobalConstants.AdministratorName)]
        public async Task<IActionResult> Create()
        {
            var model = new ProductInputModel();
            model = await productService.AddDropdownsCollections(model);
            return View(model);
        }

        [HttpPost]
        // [Authorize(Roles = GlobalConstants.AdministratorName)]
        public async Task<IActionResult> Create(ProductInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await productService.CreateProductAsync(model);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
