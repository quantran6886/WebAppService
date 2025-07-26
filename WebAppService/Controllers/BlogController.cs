using Microsoft.AspNetCore.Mvc;
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
                var allData = db.WebTinTucBaiViets.Where(c => c.IsCongKhai == true).OrderByDescending(x => x.ThoiGianTao).ToList();

                var lstData = allData.Select(c => new
                     {
                         c.TieuDeBaiViet,
                         c.MoTaNgan,
                         c.UrlImage,
                     })
                     .Skip((page - 1) * pageSize)
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
