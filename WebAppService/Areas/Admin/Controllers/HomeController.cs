using System.Diagnostics;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppService.Areas.Admin._Helper;
using WebAppService.Models.Updates;

namespace WebAppService.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var user = SessionHelper.GetUser(HttpContext.Session);

            return View();
        }

        AppDbContext db = new AppDbContext();

        public IActionResult HomePage1()
        {
            return View();
        }

        public IActionResult HomePage2()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveData(string CbGiaoDien, string NoiDung, bool IsCongKhai)
        {
            try
            {
                string decodedContent = "";
                if (!string.IsNullOrEmpty(NoiDung))
                {
                    decodedContent = HttpUtility.UrlDecode(NoiDung);
                }
                var data_find = db.WebCauHinhTrangs.Where(c => c.CbGiaoDien == CbGiaoDien).FirstOrDefault();
                if (CbGiaoDien == "1")
                {
                    if (data_find != null)
                    {
                        data_find.NoiDung = decodedContent;
                        data_find.IsCongKhai = IsCongKhai;
                    }
                    else
                    {
                        WebCauHinhTrang _ch = new WebCauHinhTrang();
                        _ch.CbGiaoDien = "1";
                        _ch.NoiDung = decodedContent;
                        _ch.IsCongKhai = IsCongKhai;
                        db.WebCauHinhTrangs.Add(_ch);
                    }
                }
                else if (CbGiaoDien == "2")
                {
                    if (data_find != null)
                    {
                        data_find.NoiDung = decodedContent;
                        data_find.IsCongKhai = IsCongKhai;
                    }
                    else
                    {
                        WebCauHinhTrang _ch = new WebCauHinhTrang();
                        _ch.CbGiaoDien = "2";
                        _ch.NoiDung = decodedContent;
                        _ch.IsCongKhai = IsCongKhai;
                        db.WebCauHinhTrangs.Add(_ch);
                    }
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
        public IActionResult LoadDetail(string CbGiaoDien)
        {
            try
            {
                var lstData = db.WebCauHinhTrangs.Where(c => c.CbGiaoDien == CbGiaoDien).Select(x => new
                {
                    x.MaTrang,
                    x.NoiDung,
                    x.IsCongKhai,
                }).ToList().Select(x => new
                {
                    x.MaTrang,
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

        [HttpGet]
        public IActionResult GetListNotification()
        {
            try
            {
                var lstData = db.WebThongBaos.Select(x => new
                {
                    x.IdNoti,
                    x.IsDaDoc,
                    x.TieuDe,
                    x.NoiDung,
                    x.ThoiGianTao,
                }).ToList().Select(x => new
                {
                    x.IdNoti,
                    x.IsDaDoc,
                    x.TieuDe,
                    x.NoiDung,
                    ThoiGianTao = x.ThoiGianTao != null ? string.Format("{0:{dd/MM/yyy HH:ss}}",x.ThoiGianTao) : "",
                }).OrderBy(c => c.ThoiGianTao).Take(50).ToList();

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
