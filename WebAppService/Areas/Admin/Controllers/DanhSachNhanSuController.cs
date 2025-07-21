using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppService.Areas.Admin._Helper;

namespace WebAppService.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class DanhSachNhanSuController : AdminBaseController
    {
        public IActionResult DanhSachNhanSu()
        {
            return View();
        }
    }
}
