using Microsoft.AspNetCore.Mvc;

namespace WebAppService.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TaiSanDauGiaController : Controller
    {
        public IActionResult TaiSanDauGia()
        {
            return View();
        }
    }
}
