using Microsoft.AspNetCore.Mvc;

namespace WebAppService.Controllers
{
    public class DanhMucTaiSanController : Controller
    {
        public IActionResult DanhMucTaiSan()
        {
            return View();
        }
    }
}
