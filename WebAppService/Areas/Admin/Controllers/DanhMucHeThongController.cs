using Microsoft.AspNetCore.Mvc;
using WebAppService.Models.Updates;

namespace WebAppService.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DanhMucHeThongController : Controller
    {
        public IActionResult DanhMucHeThong()
        {
            return View();
        }
        AppDbContext db = new AppDbContext();

        [HttpGet]
        public IActionResult LoadTable(string LoaiDanhMuc)
        {
            try
            {
                var lstData = db.WebDanhMucHeThongs.Where(c => c.IsPhanLoai != true).Select(c => new
                {
                    c.ThuTuTg,
                    c.TenGoi,
                    c.GhiChu,
                });

                return new JsonResult(new
                {
                    lstData,
                    status = true
                });
            }
            catch (Exception ex)
            {

                return new JsonResult(new
                {
                    message = ex.Message,
                    status = false
                });
            }
        }

    }
}
