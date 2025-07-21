using Microsoft.AspNetCore.Mvc;

namespace clinic_website.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult News()
        {
            return View();
        }
    }
}
