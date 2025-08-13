using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.WebSockets;
using WebAppService.Middlewares;
using WebAppService.Models;

namespace clinic_website.Controllers
{
    public class HomeViewModel
    {
        public WebCauHinhTrang? Record { get; set; }
        public WebCauHinhTrang? Record2 { get; set; }
        public List<WebTinTucBaiViet>? Record3 { get; set; }
        public List<WebTinTucBaiViet>? Record4 { get; set; }
        public List<WebVideo>? Record5 { get; set; }
        public List<WebVideo>? Record6 { get; set; }
        public List<WebDichVu>? ListData { get; set; }
    }
    public class HomeController : Controller
    {
        AppDbContext db = new AppDbContext();
        private readonly DapperConnection _dapper;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, DapperConnection dapper)
        {
            _logger = logger;
            _dapper = dapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LoadDataAfter()
        {
            try
            {
                using (var connection = _dapper.CreateConnection())
                {
                    var sql = @"
                         SELECT * FROM WebTinTucBaiViet WHERE IsCongKhai = 1;
                         SELECT * FROM WebVideo WHERE IsCongKhai = 1;
                   
                     ";

                    using (var multi = await connection.QueryMultipleAsync(sql))
                    {
                        var datarecord2 = (await multi.ReadAsync<WebTinTucBaiViet>()).ToList();
                        var datarecord3 = (await multi.ReadAsync<WebVideo>()).ToList();

                        var tintuc1 = datarecord2
                            .Where(c => c.IsBaiVietNoiBat == true && c.CbLoaiBaiDang == "Báo chí")
                            .OrderByDescending(c => c.ThoiGianTao)
                            .Take(1)
                            .ToList();

                        var tintuc2 = datarecord2
                            .Where(c => c.IsBaiVietNoiBat != true && c.CbLoaiBaiDang == "Báo chí")
                            .ToList();

                        var video1 = datarecord3
                            .Where(c => c.IsVideoNoiBat == true)
                            .OrderByDescending(c => c.ThoiGianTao)
                            .Take(1)
                            .ToList();

                        var video2 = datarecord3
                            .Where(c => !c.IsVideoNoiBat == true)
                            .OrderByDescending(c => c.ThoiGianTao)
                            .Take(2)
                            .ToList();

                        var viewModel = new HomeViewModel
                        {
                            Record3 = tintuc1,
                            Record4 = tintuc2,
                            Record5 = video1,
                            Record6 = video2,
                        };

                        return new JsonResult(new
                        {
                            viewModel,
                            status = true
                        });
                    }
                }
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
        public async Task<IActionResult> LoadDataBefore()
        {
            try
            {
                using (var connection = _dapper.CreateConnection())
                {
                    var sql = @"SELECT * FROM WebCauHinhTrang WHERE IsCongKhai = 1;
                                SELECT TOP 9 * FROM WebDichVu WHERE IsBaiVietNoiBat = 1 AND IsCongKhai = 1 ORDER BY ThoiGianTao DESC;";

                    using (var multi = await connection.QueryMultipleAsync(sql))
                    {
                        var datarecord = (await multi.ReadAsync<WebCauHinhTrang>()).ToList();

                        var home1 = datarecord.FirstOrDefault(c => c.CbGiaoDien == "1");
                        var home2 = datarecord.FirstOrDefault(c => c.CbGiaoDien == "2");
                        var listData = (await multi.ReadAsync<WebDichVu>()).ToList();

                        var viewModel = new HomeViewModel
                        {
                            Record = home1,
                            Record2 = home2,
                            ListData = listData
                        };

                        return new JsonResult(new
                        {
                            viewModel,
                            status = true
                        });
                    }
                }
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
        public IActionResult LoadMenu()
        {
            try
            {
                var dbLoad = db.WebDanhMucHeThongs.ToList();

                var lstChuyenKhoa = dbLoad.Where(c => c.LoaiDanhMuc == "Danh mục nhóm dịch vụ chuyên khoa").Select(c => new
                {
                    c.ThuTuTg,
                    c.TenGoi,
                    link = "/chuyen-khoa/?cb=" + c.TenGoi + "&dm=1",
                }).OrderBy(c => c.ThuTuTg).ToList();

                var lstDichVuDacBiet = db.WebDichVus.Where(c => c.IsBaiVietNoiBat == true).Select(c => new
                {
                    c.IdDichVu,
                    TenGoi = c.TieuDeBaiViet,
                    link = "/dich-vu/" + c.SeoUrl,
                }).ToList();

                var lstBaiViet = dbLoad.Where(c => c.LoaiDanhMuc == "Danh mục nhóm bài viết").Select(c => new
                {
                    c.ThuTuTg,
                    c.TenGoi,
                    link = "/bai-viet/?cb=" + c.TenGoi,
                }).OrderBy(c => c.ThuTuTg).ToList();

                return new JsonResult(new
                {
                    lstChuyenKhoa,
                    lstDichVuDacBiet,
                    lstBaiViet,
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

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
