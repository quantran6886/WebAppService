using Microsoft.AspNetCore.Mvc;

namespace WebAppService.Controllers
{
    public class ChiTietSanPhamController : Controller
    {
        public IActionResult ChiTietSanPham()
        {
            return View();
        }
    }
}
