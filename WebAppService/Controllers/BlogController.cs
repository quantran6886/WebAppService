using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppService.Areas.Admin._Helper;
using WebAppService.Models;

namespace clinic_website.Controllers
{
    public class BaiVietItemViewModel
    {
        public Guid IdBaiViet { get; set; }
        public string UrlImage { get; set; }
        public bool? IsBaiVietNoiBat { get; set; }
        public string TieuDeBaiViet { get; set; }
        public string MoTaNgan { get; set; }
        public string NguoiTao { get; set; }
        public string CbNhomBaiViet { get; set; }
        public string CbLoaiBaiDang { get; set; }
        public string TieuDeNgan { get; set; }
        public string NgayDang { get; set; }    
        public string ThoiGianCapNhap { get; set; }
    }
    public class ListDetailViewModel
    {
        public WebTinTucBaiViet? Record { get; set; }
        public List<BaiVietItemViewModel>? ListData { get; set; }
    }
    public class BlogController : Controller
    {
        public IActionResult Blog()
        {
            return View();
        }
        public IActionResult BlogList()
        {
            return View();
        }
        [HttpGet]
        [Route("chi-tiet-bai-viet/{id?}")]
        public IActionResult BlogDetail(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Không tìm thấy thông tin.");
            }

            var record = db.WebTinTucBaiViets.AsNoTracking().FirstOrDefault(c => c.SeoUrl.ToString() == id);
            if (record == null)
            {
                return NotFound("Không tìm thấy dịch vụ.");
            }

            var listData = db.WebTinTucBaiViets.AsNoTracking()
                           .Where(c => c.IdBaiViet != record.IdBaiViet && c.IsCongKhai == true)
                           .ToList()
                           .Select(c => new BaiVietItemViewModel
                           {
                               IdBaiViet = c.IdBaiViet,
                               UrlImage = c.UrlImage,
                               IsBaiVietNoiBat = c.IsBaiVietNoiBat,
                               TieuDeBaiViet = c.TieuDeBaiViet,
                               MoTaNgan = c.MoTaNgan,
                               NguoiTao = c.NguoiTao,
                               CbNhomBaiViet = string.IsNullOrEmpty(c.CbNhomBaiViet) ? "" : c.CbNhomBaiViet,
                               CbLoaiBaiDang = string.IsNullOrEmpty(c.CbLoaiBaiDang) ? "" : c.CbLoaiBaiDang,
                               TieuDeNgan = c.TieuDeNgan,
                               NgayDang = c.ThoiGianCapNhap?.ToString("dd/MM/yyyy"),
                               ThoiGianCapNhap = c.ThoiGianCapNhap.HasValue ? TimeAgo.GetTime(c.ThoiGianCapNhap.Value) : "Chưa xác định"
                           }).ToList();


            var viewModel = new ListDetailViewModel
            {
                Record = record,
                ListData = listData
            };

            return View(viewModel);
        }
        AppDbContext db = new AppDbContext();

        [HttpGet]
        public IActionResult LoadData(int page, int pageSize)
        {
            try
            {
                var ListAllData = db.WebTinTucBaiViets.AsNoTracking().Where(c => c.IsCongKhai == true).Select(c => new
                {
                    c.IdBaiViet,
                    c.UrlImage,
                    c.IsBaiVietNoiBat,
                    c.TieuDeBaiViet,
                    c.MoTaNgan,
                    c.NguoiTao,
                    CbNhomBaiViet = string.IsNullOrEmpty(c.CbNhomBaiViet) ? "" : c.CbNhomBaiViet,
                    CbLoaiBaiDang = string.IsNullOrEmpty(c.CbLoaiBaiDang) ? "" : c.CbLoaiBaiDang,
                    c.TieuDeNgan,
                    c.SeoUrl,
                    ThoiGianCapNhap = c.ThoiGianCapNhap != null ? c.ThoiGianCapNhap : DateTime.Now,
                }).ToList();

                var bvnbTrongThangRaw = ListAllData
                    .Where(c => c.ThoiGianCapNhap?.Month == DateTime.Now.Month &&
                                c.ThoiGianCapNhap?.Year == DateTime.Now.Year &&
                                c.IsBaiVietNoiBat == true)
                    .ToList();

                if (!bvnbTrongThangRaw.Any())
                {
                    bvnbTrongThangRaw = ListAllData
                        .Where(c => c.IsBaiVietNoiBat == true)
                        .OrderByDescending(c => c.ThoiGianCapNhap)
                        .Take(5)
                        .ToList();
                }

                var bvnbTrongThang = bvnbTrongThangRaw.Select(c => new
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
                    c.SeoUrl,
                    NgayDang = c.ThoiGianCapNhap?.ToString("dd/MM/yyyy"),
                    ThoiGianCapNhap = TimeAgo.GetTime(c.ThoiGianCapNhap ?? DateTime.Now)
                }).ToList();

                var bvPhoBien = ListAllData.Where(C => C.CbLoaiBaiDang == "Bài  viết" || C.CbLoaiBaiDang == "Bài viết").Select(c => new
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
                    c.SeoUrl,
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
                    c.SeoUrl,
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

        [HttpGet]
        public IActionResult LoadListData(int page, int pageSize, string txtSearh, string? cb)
        {
            try
            {
                var keyword = (txtSearh ?? "").ToLower().Trim();

                var allData = db.WebTinTucBaiViets.AsNoTracking()
                    .Where(c =>
                        c.IsCongKhai == true &&
                        (string.IsNullOrEmpty(keyword) || c.TieuDeBaiViet.ToLower().Trim().Contains(keyword)) &&
                        (!string.IsNullOrEmpty(cb)  ? c.CbNhomBaiViet == cb : true)
                    )
                    .OrderByDescending(x => x.ThoiGianTao)
                    .ToList();

                var lstData = allData.Select(x => new
                {
                    x.IdBaiViet,
                    x.UrlImage,
                    x.NameImage,
                    x.TieuDeBaiViet,
                    x.MoTaNgan,
                    x.NguoiTao,
                    x.IsCongKhai,
                    x.IsBaiVietNoiBat,
                    x.TieuDeNgan,
                    x.ThoiGianTao,
                    x.SeoUrl,
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
