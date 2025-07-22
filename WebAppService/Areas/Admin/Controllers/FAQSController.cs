using Microsoft.AspNetCore.Mvc;
using WebAppService.Models.Updates;

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
        public IActionResult LoadTable(string LoaiDanhMuc)
        {
            try
            {
                var lstData = db.WebDanhMucHeThongs.Where(c => c.LoaiDanhMuc == LoaiDanhMuc).Select(c => new
                {
                    c.IdHeThong,
                    c.ThuTuTg,
                    c.TenGoi,
                    c.GhiChu,
                }).ToList();

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
        public IActionResult LoadDetail(Int64? IdHeThong)
        {
            try
            {
                var lstData = db.WebDanhMucHeThongs.Where(c => c.IdHeThong == IdHeThong).Select(c => new
                {
                    c.IdHeThong,
                    c.LoaiDanhMuc,
                    c.ThuTuTg,
                    c.TenGoi,
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
        public IActionResult DeleteData(Int64? IdHeThong)
        {
            try
            {
                if (IdHeThong > 0)
                {
                    var find_data = db.WebDanhMucHeThongs.Find(IdHeThong);
                    if (find_data != null)
                    {
                        db.WebDanhMucHeThongs.Remove(find_data);
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
        public IActionResult SaveData(Int64? IdHeThong, Int32 ThuTuTG, string TenGoi, string GhiChu, string LoaiDanhMuc)
        {
            try
            {
                var check_so_thu_tu = db.WebDanhMucHeThongs.Where(c => IdHeThong == 0 ? (c.ThuTuTg == ThuTuTG && c.LoaiDanhMuc == LoaiDanhMuc) : (c.IdHeThong != IdHeThong && c.ThuTuTg == ThuTuTG && c.LoaiDanhMuc == LoaiDanhMuc)).Count();
                if (check_so_thu_tu > 0)
                {
                    return Json(new
                    {
                        code = false,
                        message = "Số thứ tự bạn nhập đã bị trùng với dữ liệu khác.",
                    });
                }
                if (IdHeThong == 0)
                {
                    db.WebDanhMucHeThongs.Add(new WebDanhMucHeThong
                    {
                        ThuTuTg = ThuTuTG,
                        TenGoi = TenGoi,
                        LoaiDanhMuc = LoaiDanhMuc,
                        GhiChu = GhiChu,
                    });
                    db.SaveChanges();
                }
                else
                {
                    var find_data = db.WebDanhMucHeThongs.Find(IdHeThong);
                    if (find_data != null)
                    {
                        find_data.ThuTuTg = ThuTuTG;
                        find_data.TenGoi = TenGoi;
                        find_data.LoaiDanhMuc = LoaiDanhMuc;
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
