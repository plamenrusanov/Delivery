using Delivery.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = GlobalConstants.AdministratorName)]
    public class AdminController : Controller
    {
    }
}
