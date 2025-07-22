using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppService.Areas.Admin._Helper;
using WebAppService.Models.Updates;

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
        AppDbContext db = new AppDbContext();

        [HttpGet]
        public IActionResult LoadData()
        {
            try
            {
                var lstChucDanh = db.WebDanhMucHeThongs.Where(c => c.LoaiDanhMuc == "Danh mục chức danh").Select(c => new
                {
                    c.IdHeThong,
                    c.ThuTuTg,
                    c.TenGoi,
                    c.GhiChu,
                });

                return new JsonResult(new
                {
                    lstChucDanh,
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
