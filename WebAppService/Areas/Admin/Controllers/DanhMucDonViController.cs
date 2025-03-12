using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAppService.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DanhMucDonViController : Controller
    {
        public IActionResult DanhMucDonVi()
        {
            return View();
        }
    }
}
