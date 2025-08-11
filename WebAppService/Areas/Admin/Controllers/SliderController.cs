using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppService.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAppService.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SliderController : Controller
    {
        public IActionResult Slider()
        {
            return View();
        }

        AppDbContext db = new AppDbContext();

        [HttpGet]
        public IActionResult LoadTable(string LoaiDanhMuc)
        {
            try
            {
                var lstData = db.WebCauHinhTrangs.Where(c => c.IsCard1 == true).Select(c => new
                {
                    c.MaTrang,
                    c.SapXep,
                    Image = !string.IsNullOrEmpty(c.TxtCard1) ? c.TxtCard1 : "/root/6388000.png",
                    c.TxtCard1,
                    c.CbGiaoDien,
                    c.IsCongKhai,
                    c.IsCard1,
                }).OrderBy(c => c.SapXep).ToList();

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
        public IActionResult SaveData(Guid MaTrang,int? SapXep , string? TxtCard1, string? CbGiaoDien)
        {
            try
            {
                var find_data = db.WebCauHinhTrangs.Find(MaTrang);  

                if (find_data != null)
                {
                    find_data.SapXep = SapXep;
                    find_data.TxtCard1 = TxtCard1;
                    find_data.CbGiaoDien = CbGiaoDien;
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
        public IActionResult SaveDataCongKhai(Guid MaTrang,  bool? IsCongKhai)
        {
            try
            {
                var find_data = db.WebCauHinhTrangs.Find(MaTrang);

                if (find_data != null)
                {
                    find_data.IsCongKhai = IsCongKhai;
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
        public async Task<IActionResult> UploadFile(List<IFormFile> files, Guid MaTrang)
        {
            try
            {
                if (files == null || files.Count == 0)
                    return Json(new { status = false, message = "Không có file nào được gửi lên." });

                var file = files[0]; // Lấy file đầu tiên

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "Slider");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Cập nhật DB
                var fileUrl = "/Uploads/Slider/" + uniqueFileName;

                var record = db.WebCauHinhTrangs.FirstOrDefault(x => x.MaTrang == MaTrang);
                if (record != null)
                {
                    record.TxtCard1 = fileUrl;
                    await db.SaveChangesAsync();
                }

                return Json(new { status = true, fileUrl });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> DeleteFile(Guid MaTrang)
        {
            try
            {
                var record = db.WebCauHinhTrangs.FirstOrDefault(x => x.MaTrang == MaTrang);
                if (record != null && !string.IsNullOrEmpty(record.TxtCard1))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", record.TxtCard1.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    record.TxtCard1 = null;
                    await db.SaveChangesAsync();

                    return Json(new { status = true });
                }

                return Json(new { status = false, message = "Không tìm thấy dữ liệu" });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteData(Guid MaTrang)
        {
            try
            {
                var record = db.WebCauHinhTrangs.FirstOrDefault(x => x.MaTrang == MaTrang);
                if (record != null && !string.IsNullOrEmpty(record.TxtCard1))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", record.TxtCard1.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
                db.WebCauHinhTrangs.Remove(record);
                await db.SaveChangesAsync();

                return Json(new { status = true });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult SaveNewForm()
        {
            try
            {
                db.WebCauHinhTrangs.Add(new WebCauHinhTrang
                {
                    MaTrang = new Guid(),
                    SapXep = null,
                    TxtCard1 = "",
                    CbGiaoDien = "",
                    IsCongKhai = false,
                    IsCard1 = true,
                });
                db.SaveChanges();

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
