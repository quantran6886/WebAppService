using Microsoft.AspNetCore.Mvc;
using WebAppService.Models;

namespace WebAppService.Controllers
{
    public class AboutUsController : Controller
    {
        AppDbContext db = new AppDbContext();
        public IActionResult AboutUs()
        {
            var record = db.WebAbousUs.FirstOrDefault();

            if (record == null)
            {
                return NotFound("Không tìm thấy thông tin.");
            }

            return View(record);
        }
    }
}
