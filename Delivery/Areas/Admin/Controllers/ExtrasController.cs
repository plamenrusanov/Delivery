using Delivery.Core.Constants;
using Delivery.Core.Contracts;
using Delivery.Core.ViewModels.Extras;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Areas.Admin.Controllers
{
    public class ExtrasController : AdminController
    {
        private readonly IExtrasService extrasService;
        private readonly ILogger<ExtrasController> logger;

        public ExtrasController(
            IExtrasService extrasService,
            ILogger<ExtrasController> logger)
        {
            ViewData[GlobalConstants.viewDataTitle] = "Добавки";
            this.extrasService = extrasService;
            this.logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var model = await extrasService.All();
                return View(model);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        }

        public IActionResult Create()
        {
            var model = new ExtraInpitModel();
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Create(ExtraInpitModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await extrasService.AddExtraAsync(model);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        }


        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                ExtraEditModel model = await extrasService.GetEditModelAsync(id);
                return View(model);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ExtraEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await extrasService.UpdateExtraAsync(model);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        }


        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await extrasService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        }
    }
}
