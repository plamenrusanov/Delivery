using Delivery.Core.Constants;
using Delivery.Core.Contracts;
using Delivery.Core.ViewModels.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Areas.Administration.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        [Authorize(Roles = GlobalConstants.AdministratorName)]
        public IActionResult Create()
        {
            var model = new ProductInputModel();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorName)]
        public async Task<IActionResult> Create(ProductInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }



            return RedirectToAction("Index");
        }
    }
}
