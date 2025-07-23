using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppService.Models.Updates;

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
        AppDbContext db = new AppDbContext();

        
    }
}
