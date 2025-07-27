using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppService.Models;

namespace clinic_website.Controllers
{
    public class HomeViewModel
    {
        public WebCauHinhTrang? Record { get; set; }
        public WebCauHinhTrang? Record2 { get; set; }
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

                var home1 = Datarecord.FirstOrDefault(c => c.CbGiaoDien == "1");
                var home2 = Datarecord.FirstOrDefault(c => c.CbGiaoDien == "2");

                var listData = db.WebDichVus
                                .Where(c => c.IsBaiVietNoiBat == true && c.IsCongKhai == true)
                                .OrderByDescending(x => x.ThoiGianTao)
                                .Take(9)
                                .ToList();

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
