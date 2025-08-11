using Microsoft.AspNetCore.Mvc;

namespace WebAppService.Controllers
{
    public class DanhSachVideoController : Controller
    {
        public IActionResult DanhSachVideo()
        {
            return View();
        }
    }
}
