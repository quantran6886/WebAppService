using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using WebAppService.Models;

namespace WebAppService.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AboutUsController : Controller
    {
        public IActionResult AboutUs()
        {
            return View();
        }
        AppDbContext db = new AppDbContext();

        [HttpPost]
        public IActionResult SaveData(string NoiDung, bool IsCongKhai)
        {
            try
            {
                string decodedContent = "";
                if (!string.IsNullOrEmpty(NoiDung))
                {
                    decodedContent = HttpUtility.UrlDecode(NoiDung);
                }
                var data_find = db.WebAbousUs.FirstOrDefault();

                if (data_find != null)
                {
                    data_find.NoiDung = decodedContent;
                    data_find.IsCongKhai = IsCongKhai;
                }
                else
                {
                    WebAbousU _ch = new WebAbousU();
                    _ch.NoiDung = decodedContent;
                    _ch.IsCongKhai = IsCongKhai;
                    db.WebAbousUs.Add(_ch);
                }
                db.SaveChanges();
                return new JsonResult(new
                {
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

        [HttpPost]
        public IActionResult GetSeo()
        {
            try
            {
                var data_find = db.WebAbousUs.FirstOrDefault();
                if (data_find != null)
                {
                    var vbtl = db.BrowerVanBanTaiLieus.Where(c => c.TenVanBan == "3").FirstOrDefault();
                    data_find.NoiDung = vbtl.UrlFile;
                }
                db.SaveChanges();
                return new JsonResult(new
                {
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
        public IActionResult LoadDetail()
        {
            try
            {
                var lstData = db.WebAbousUs.Select(x => new
                {
                    x.IdAbousUs,
                    x.NoiDung,
                    x.IsCongKhai,
                }).ToList().Select(x => new
                {
                    x.IdAbousUs,
                    x.NoiDung,
                    x.IsCongKhai,
                }).FirstOrDefault();

                return new JsonResult(new
                {
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
