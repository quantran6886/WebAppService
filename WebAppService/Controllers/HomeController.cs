using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
