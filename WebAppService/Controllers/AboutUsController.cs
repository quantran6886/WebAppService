using Microsoft.AspNetCore.Mvc;

namespace WebAppService.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult AboutUs()
        {
            return View();
        }
    }
}
