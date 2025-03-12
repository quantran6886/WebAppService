using Microsoft.AspNetCore.Mvc;

namespace WebAppService.Controllers
{
    public class DangKyTaiKhoanController : Controller
    {
        public IActionResult DangKyTaiKhoan()
        {
            return View();
        }
    }
}
