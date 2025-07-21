using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAppService.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class TinTucBaiVietController : Controller
    {
        public IActionResult TinTucBaiViet()
        {
            return View();
        }
    }
}
