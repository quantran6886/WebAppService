using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppService.Areas.Admin._Helper;

namespace WebAppService.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var user = SessionHelper.GetUser(HttpContext.Session);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

       
    }
}
