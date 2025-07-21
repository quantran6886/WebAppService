using Microsoft.AspNetCore.Mvc;

namespace clinic_website.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Blog()
        {
            return View();
        }
        public IActionResult BlogDetail()
        {
            return View();
        }
    }
}
