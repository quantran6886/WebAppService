using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAppService.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class DichVuController : Controller
    {
        public IActionResult DichVu()
        {
            return View();
        }
    }
}
