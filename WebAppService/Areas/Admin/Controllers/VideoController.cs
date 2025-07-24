using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAppService.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class VideoController : Controller
    {
        public IActionResult Video()
        {
            return View();
        }
    }
}
