using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.WebSockets;
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
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult LoadData()
        {
            try
            {
                var Datarecord = db.WebCauHinhTrangs.Where(c => c.IsCongKhai == true).ToList();
                var Datarecord2 = db.WebTinTucBaiViets.Where(c => c.IsCongKhai == true).ToList();
                var Datarecord3 = db.WebVideos.Where(c => c.IsCongKhai == true).ToList();

                var home1 = Datarecord.FirstOrDefault(c => c.CbGiaoDien == "1");
                var home2 = Datarecord.FirstOrDefault(c => c.CbGiaoDien == "2");
                var tintuc1 = Datarecord2.OrderByDescending(c => c.ThoiGianTao).Where(c => c.IsBaiVietNoiBat == true && c.CbLoaiBaiDang == "Báo chí").Take(1).ToList();
                var tintuc2 = Datarecord2.Where(c => c.IsBaiVietNoiBat != true && c.CbLoaiBaiDang == "Báo chí").ToList();
                var video1 = Datarecord3.OrderByDescending(c => c.ThoiGianTao).Where(c => c.IsVideoNoiBat == true).Take(1).ToList();
                var video2 = Datarecord3.OrderByDescending(c => c.ThoiGianTao).Where(c => c.IsVideoNoiBat != true).Take(2).ToList();
                var listData = db.WebDichVus
                                .Where(c => c.IsBaiVietNoiBat == true && c.IsCongKhai == true)
                                .OrderByDescending(x => x.ThoiGianTao)
                                .Take(9)
                                .ToList();

                var viewModel = new HomeViewModel
                {
                    Record = home1,
                    Record2 = home2,
                    Record3 = tintuc1,
                    Record4 = tintuc2,
                    Record5 = video1,
                    Record6 = video2,
                    ListData = listData
                };

                return new JsonResult(new
                {
                    viewModel,
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
        public IActionResult LoadMenu()
        {
            try
            {
                var dbLoad = db.WebDanhMucHeThongs.ToList();

                var lstChuyenKhoa = dbLoad.Where(c => c.LoaiDanhMuc == "Danh mục nhóm dịch vụ chuyên khoa").Select(c => new
                {
                    c.ThuTuTg,
                    c.TenGoi,
                    link = "/Service/ServiceList/?cb=" + c.TenGoi + "&dm=1",
                }).OrderBy(c => c.ThuTuTg).ToList();

                var lstDichVuDacBiet = db.WebDichVus.Where(c => c.IsBaiVietNoiBat == true).Select(c => new
                {
                    c.IdDichVu,
                    TenGoi = c.TieuDeBaiViet,
                    link = "/Service/ServiceList/?cb=" + c.TieuDeBaiViet + "&dm=2",
                }).ToList();

                var lstBaiViet = dbLoad.Where(c => c.LoaiDanhMuc == "Danh mục nhóm bài viết").Select(c => new
                {
                    c.ThuTuTg,
                    c.TenGoi,
                    link = "/Blog/BlogList/?cb=" + c.TenGoi,
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
