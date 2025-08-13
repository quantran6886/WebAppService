using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppService.Models;

namespace WebAppService.Controllers
{
    public class DanhSachVideoController : Controller
    {
        public IActionResult DanhSachVideo()
        {
            return View();
        }
        AppDbContext db = new AppDbContext();
        [HttpGet]
        public IActionResult LoadData(int page, int pageSize, string txtSearh, string? cb, string? dm)
        {
            try
            {
                var keyword = (txtSearh ?? "").ToLower().Trim();

                var allData = db.WebVideos.AsNoTracking()
                    .Where(c =>
                        c.IsCongKhai == true &&
                        (string.IsNullOrEmpty(keyword) || c.TieuDeBaiViet.ToLower().Trim().Contains(keyword))
                    )
                    .OrderByDescending(x => x.ThoiGianTao)
                    .ToList();

                var lstData = allData.Select(x => new
                {
                    x.IdVideo,
                    x.UrlImage,
                    x.NameImage,
                    x.TieuDeBaiViet,
                    x.MoTaNgan,
                    x.NguoiTao,
                    x.IsCongKhai,
                    x.TieuDeNgan,
                    x.ThoiGianTao,
                    x.SeoUrl,
                    x.UrlVideo
                }).Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();


                return new JsonResult(new
                {
                    totalRow = allData.Count,
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
