using Microsoft.AspNetCore.Mvc;

namespace clinic_website.Controllers
{
    public class NewPaperController : Controller
    {
        public IActionResult NewPaper()
        {
            return View();
        }
    }
}
