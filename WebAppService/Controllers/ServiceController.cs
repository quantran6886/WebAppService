using Microsoft.AspNetCore.Mvc;
using WebAppService.Models;

namespace clinic_website.Controllers
{
    public class DichVuDetailViewModel
    {
        public WebDichVu? Record { get; set; }
        public List<WebDichVu>? ListData { get; set; }
    }
    public class ServiceController : Controller
    {
        AppDbContext db = new AppDbContext();
        public IActionResult ServiceList()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ServiceDetail(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return RedirectToAction("NotFound", "NotFound");
            }

            var record = db.WebDichVus.FirstOrDefault(c => c.IdDichVu.ToString() == id);
            if (record == null)
            {
                return RedirectToAction("NotFound", "NotFound");
            }

            var listData = db.WebDichVus
                            .Where(c => c.IdDichVu != record.IdDichVu && c.IsBaiVietNoiBat != true && c.IsCongKhai == true)
                            .OrderByDescending(x => x.ThoiGianTao)
                            .Take(5)
                            .ToList();

            var viewModel = new DichVuDetailViewModel
            {
                Record = record,
                ListData = listData
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult LoadData(int page, int pageSize, string txtSearh, string? cb, string? dm)
        {
            try
            {
                var keyword = (txtSearh ?? "").ToLower().Trim();

                var allData = db.WebDichVus
                    .Where(c =>
                        c.IsCongKhai == true &&
                        c.IsBaiVietNoiBat != true &&
                        (string.IsNullOrEmpty(keyword) || c.TieuDeBaiViet.ToLower().Trim().Contains(keyword)) &&
                        (!string.IsNullOrEmpty(cb) && !string.IsNullOrEmpty(dm) ? c.CbNhomBaiViet == cb && c.CbLoaiBaiDang == dm : true)
                    )
                    .OrderByDescending(x => x.ThoiGianTao)
                    .ToList();

                var lstData = allData.Select(x => new
                {
                    x.IdDichVu,
                    x.UrlImage,
                    x.NameImage,
                    x.TieuDeBaiViet,
                    x.MoTaNgan,
                    x.NguoiTao,
                    x.IsCongKhai,
                    x.IsBaiVietNoiBat,
                    x.TieuDeNgan,
                    x.ThoiGianTao,
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
