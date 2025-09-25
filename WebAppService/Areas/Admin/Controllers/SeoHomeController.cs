using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppService.Models;

namespace WebAppService.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SeoHomeController : Controller
    {
        public IActionResult SeoHome()
        {
            return View();
        }

        AppDbContext db = new AppDbContext();

        [HttpGet]
        public IActionResult LoadTable()
        {
            try
            {
                var lstData = db.BrowerHomePages.Where(c => c.SoThuTu == null).Select(c => new
                {
                    c.Id,
                    c.SeoUrl,
                    c.SeoTittile,
                    Image = !string.IsNullOrEmpty(c.Link) ? c.Link : "/root/6388000.png",
                    c.Link,
                    c.PhanLoai,
                    c.IsBanerHome,
                }).OrderByDescending(c => c.IsBanerHome).AsNoTracking().ToList();

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
        public IActionResult SaveNewForm()
        {
            try
            {
                db.BrowerHomePages.Add(new BrowerHomePage
                {
                    Id = 0,
                    SeoTittile = null,
                    SeoUrl = null,
                    Link = null,
                    PhanLoai = null,
                    IsBanerHome = false,
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

        [HttpPost]
        public IActionResult SaveData(Int64 Id, string? SeoTittile, string? SeoUrl, string? PhanLoai)
        {
            try
            {
                var find_data = db.BrowerHomePages.Find(Id);

                if (find_data != null)
                {
                    find_data.SeoTittile = SeoTittile;
                    find_data.SeoUrl = SeoUrl;
                    find_data.PhanLoai = PhanLoai;
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
        public IActionResult SaveDataCongKhai(Int64 Id, bool? IsBanerHome)
        {
            try
            {
                var find_data = db.BrowerHomePages.Find(Id);

                if (find_data != null)
                {
                    if (IsBanerHome == true)
                    {
                        find_data.IsBanerHome = IsBanerHome;
                        var data = db.BrowerHomePages.Where(c => c.Id != Id).ToList();
                        foreach (var item in data)
                        {
                            item.IsBanerHome = false;
                        }
                    }
                    else
                    {
                        find_data.IsBanerHome = false;
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

        [HttpPost]
        public async Task<IActionResult> UploadFile(List<IFormFile> files, Int64 Id)
        {
            try
            {
                if (files == null || files.Count == 0)
                    return Json(new { status = false, message = "Không có file nào được gửi lên." });

                var file = files[0]; // Lấy file đầu tiên

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "SeoHome");
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
                var fileUrl = "/Uploads/SeoHome/" + uniqueFileName;

                var record = db.BrowerHomePages.FirstOrDefault(x => x.Id == Id);
                if (record != null)
                {
                    record.Link = fileUrl;
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
        public async Task<IActionResult> DeleteFile(Int64 Id)
        {
            try
            {
                var record = db.BrowerHomePages.FirstOrDefault(x => x.Id == Id);
                if (record != null && !string.IsNullOrEmpty(record.Link))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", record.Link.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    record.Link = null;
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
        public async Task<IActionResult> DeleteData(Int64 Id)
        {
            try
            {
                var record = db.BrowerHomePages.FirstOrDefault(x => x.Id == Id);
                if (record != null && !string.IsNullOrEmpty(record.Link))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", record.Link.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
                db.BrowerHomePages.Remove(record);
                await db.SaveChangesAsync();

                return Json(new { status = true });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });
            }
        }
    }
}
