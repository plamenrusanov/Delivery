using Delivery.Infrastructure.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Areas.Admin.Controllers
{
    [Area(GlobalConstants.AreaAdmin)]
    [Authorize(Roles = GlobalConstants.AdministratorName)]
    public class AdministratorController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }

}
