using Microsoft.AspNetCore.Mvc;
using WebAppService.Models;

namespace WebAppService.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FAQSController : Controller
    {
        public IActionResult FAQS()
        {
            return View();
        }

        AppDbContext db = new AppDbContext();

        [HttpGet]
        public IActionResult LoadTable()
        {
            try
            {
                var lstData = db.WebFaqs.Select(c => new
                {
                    c.IdFaqs,
                    c.SoThuTu,
                    c.CauHoi,
                    c.CauTraLoi,
                    c.GhiChu,
                }).OrderBy(c => c.SoThuTu).ToList();

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
        public IActionResult LoadDetail(Guid? IdFaqs)
        {
            try
            {
                var lstData = db.WebFaqs.Where(c => c.IdFaqs == IdFaqs).Select(c => new
                {
                    c.IdFaqs,
                    c.CauHoi,
                    c.CauTraLoi,
                    c.SoThuTu,
                    c.GhiChu,
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

        [HttpPost]
        public IActionResult DeleteData(Guid? IdFaqs)
        {
            try
            {
                if (IdFaqs != Guid.Empty)
                {
                    var find_data = db.WebFaqs.Find(IdFaqs);
                    if (find_data != null)
                    {
                        db.WebFaqs.Remove(find_data);
                    }
                    db.SaveChanges();
                }
                return Json(new
                {
                    status = true,
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false,
                    message = ex.Message
                });
            }
        }

        [HttpPost]
        public IActionResult SaveData(Guid? IdFaqs, Int32 SoThuTu, string CauHoi, string CauTraLoi, string GhiChu)
        {
            try
            {
                var check_so_thu_tu = db.WebFaqs.Where(c => IdFaqs == Guid.Empty ?
                (c.SoThuTu == SoThuTu ) :
                (c.IdFaqs != IdFaqs && c.SoThuTu == SoThuTu)).Count();
                if (check_so_thu_tu > 0)
                {
                    return Json(new
                    {
                        code = false,
                        message = "Số thứ tự bạn nhập đã bị trùng với dữ liệu khác.",
                    });
                }
                if (IdFaqs == Guid.Empty )
                {
                    db.WebFaqs.Add(new WebFaq
                    {
                        IdFaqs = Guid.NewGuid(),
                        SoThuTu = SoThuTu,
                        CauHoi = CauHoi,
                        CauTraLoi = CauTraLoi,
                        GhiChu = GhiChu,
                    });
                    db.SaveChanges();
                }
                else
                {
                    var find_data = db.WebFaqs.Find(IdFaqs);
                    if (find_data != null)
                    {
                        find_data.SoThuTu = SoThuTu;
                        find_data.CauHoi = CauHoi;
                        find_data.CauTraLoi = CauTraLoi;
                        find_data.GhiChu = GhiChu;
                    }
                    db.SaveChanges();
                }
                return Json(new
                {
                    status = true,
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false,
                    message = ex.Message
                });
            }
        }

    }
}
