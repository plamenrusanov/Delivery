using Delivery.Core.Contracts;
using Delivery.Core.ViewModels.Products;
using Delivery.Infrastructure.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Areas.Admin.Controllers
{
    public class ProductsController : AdministratorController
    {
        private const string Products = "Продукти";
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData[GlobalConstants.viewDataTitle] = Products;
            List<ProductAdminListViewModel> listWithProducts = await productService.GetListWithProductsAsync();
            return View(listWithProducts);
        }

        public async Task<IActionResult> Create()
        {
            ViewData[GlobalConstants.viewDataTitle] = Products;
            var model = new ProductInputModel();
            model = await productService.AddDropdownsCollectionsAsync(model);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductInputModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData[GlobalConstants.viewDataTitle] = Products;
                model = await productService.AddDropdownsCollectionsAsync(model);
                return View(model);
            }

            try
            {
                await productService.CreateProductAsync(model);

                return RedirectToAction(nameof(Index));
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
                ViewData[GlobalConstants.viewDataTitle] = Products;
                ProductEditModel model = await productService.CreateEditModelAsync(id);
                return View(model);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductEditModel model)
        {
           
            if (!ModelState.IsValid)
            {
                ViewData[GlobalConstants.viewDataTitle] = Products;
                await productService.AddDropdownsCollectionsAsync(model);
                return View(model);
            }

            try
            {
                await productService.EditProductAsync(model);

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
                await productService.DeleteProductAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        }
    }
}
