using Microsoft.AspNetCore.Mvc;
using WebAppService.Areas.Admin._Helper;
using WebAppService.Models;

namespace clinic_website.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Blog()
        {
            return View();
        }
        public IActionResult BlogDetail()
        {
            return View();
        }
        AppDbContext db = new AppDbContext();

        [HttpGet]
        public IActionResult LoadData(int page, int pageSize)
        {
            try
            {
                var ListAllData = db.WebTinTucBaiViets.Where(c => c.IsCongKhai == true).Select(c => new
                {
                    c.IdBaiViet,
                    c.UrlImage,
                    c.IsBaiVietNoiBat,
                    c.TieuDeBaiViet,
                    c.MoTaNgan,
                    c.NguoiTao,
                    c.CbNhomBaiViet,
                    c.CbLoaiBaiDang,
                    c.TieuDeNgan,
                    ThoiGianCapNhap = c.ThoiGianCapNhap != null ? c.ThoiGianCapNhap : DateTime.Now,
                }).ToList();

                var bvnbTrongThang = ListAllData.Where(c => c.ThoiGianCapNhap?.Month == DateTime.Now.Month && c.ThoiGianCapNhap?.Year == DateTime.Now.Year && c.IsBaiVietNoiBat == true).Select(c => new
                {
                    c.IdBaiViet,
                    c.UrlImage,
                    c.IsBaiVietNoiBat,
                    c.TieuDeBaiViet,
                    c.MoTaNgan,
                    c.NguoiTao,
                    c.CbNhomBaiViet,
                    c.CbLoaiBaiDang,
                    c.TieuDeNgan,
                    c.ThoiGianCapNhap,
                }).ToList().Select(c => new
                {
                    c.IdBaiViet,
                    c.UrlImage,
                    c.IsBaiVietNoiBat,
                    c.TieuDeBaiViet,
                    c.MoTaNgan,
                    c.NguoiTao,
                    c.CbNhomBaiViet,
                    c.CbLoaiBaiDang,
                    c.TieuDeNgan,
                    NgayDang = c.ThoiGianCapNhap?.ToString("dd/MM/yyyy"),
                    ThoiGianCapNhap = TimeAgo.GetTime(c.ThoiGianCapNhap.Value)
                }).ToList();

                var bvPhoBien = ListAllData.Where(C => C.CbLoaiBaiDang == "Bài viết").Select(c => new
                {
                    c.IdBaiViet,
                    c.UrlImage,
                    c.IsBaiVietNoiBat,
                    c.TieuDeBaiViet,
                    c.MoTaNgan,
                    c.NguoiTao,
                    c.CbNhomBaiViet,
                    c.CbLoaiBaiDang,
                    c.TieuDeNgan,
                    NgayDang = c.ThoiGianCapNhap?.ToString("dd/MM/yyyy"),
                    ThoiGianCapNhap = TimeAgo.GetTime(c.ThoiGianCapNhap.Value)
                }).Take(20).ToList();

                var allData = ListAllData.Where(c => c.CbLoaiBaiDang == "Cẩm nang").ToList();

                var lstData = allData.Select(c => new
                {
                    c.IdBaiViet,
                    c.UrlImage,
                    c.IsBaiVietNoiBat,
                    c.TieuDeBaiViet,
                    c.MoTaNgan,
                    c.NguoiTao,
                    c.CbNhomBaiViet,
                    c.CbLoaiBaiDang,
                    c.TieuDeNgan,
                    NgayDang = c.ThoiGianCapNhap?.ToString("dd/MM/yyyy"),
                    ThoiGianCapNhap = TimeAgo.GetTime(c.ThoiGianCapNhap.Value)
                })
                     .Skip((page - 1) * pageSize)
                     .Take(pageSize)
                     .ToList();

                return new JsonResult(new
                {
                    totalRow = allData.Count,
                    bvnbTrongThang,
                    bvPhoBien,
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
