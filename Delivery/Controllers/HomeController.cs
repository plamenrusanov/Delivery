using Delivery.Infrastructure.Constants;
using Delivery.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Delivery.Controllers
{
    public class HomeController : Controller
    {
        private const string Home = "Delivery";
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;        
        }

        public IActionResult Index()
        {
            ViewData[GlobalConstants.viewDataTitle] = Home;
            return View();
        }

        public IActionResult Privacy()
        {
            ViewData[GlobalConstants.viewDataTitle] = Home;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewData[GlobalConstants.viewDataTitle] = Home;
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}