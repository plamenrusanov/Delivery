using Delivery.Core.Constants;
using Delivery.Core.Contracts;
using Delivery.Core.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Areas.Admin.Controllers
{
    public class ProductsController : AdminController
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
            ViewData[GlobalConstants.viewDataTitle] = "Продукти";
        }

        public async Task<IActionResult> Index()
        {        
            List<ProductAdminListViewModel> listWithProducts = await productService.GetListWithProductsAsync();
            return View(listWithProducts);
        }

        public IActionResult Create()
        {
            var model = new ProductInputModel();
            model = productService.AddDropdownsCollections(model);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductInputModel model)
        {
            if (!ModelState.IsValid)
            {
                model = productService.AddDropdownsCollections(model);
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
                model = (ProductEditModel)productService.AddDropdownsCollections(model);
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
