using Microsoft.AspNetCore.Mvc;

namespace WebAppService.Controllers
{
    public class NotFoundController : Controller
    {
        public IActionResult NotFound()
        {
            return View();
        }
    }
}
