using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web;
using WebAppService.Models;

namespace WebAppService.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class DichVuDacBietController : Controller
    {
        public IActionResult DichVuDacBiet()
        {
            return View();
        }
        AppDbContext db = new AppDbContext();

        [HttpGet]
        public IActionResult LoadData()
        {
            try
            {
                var lstNhomBaiViet = db.WebDanhMucHeThongs.Where(c => c.LoaiDanhMuc == "Danh mục nhóm bài viết").Select(c => new
                {
                    c.IdHeThong,
                    c.ThuTuTg,
                    c.TenGoi,
                    c.GhiChu,
                }).OrderBy(c => c.TenGoi).ToList();

                var lstDanhMucBaiDang = db.WebDanhMucHeThongs.Where(c => c.LoaiDanhMuc == "Danh mục bài đăng").Select(c => new
                {
                    c.IdHeThong,
                    c.ThuTuTg,
                    c.TenGoi,
                    c.GhiChu,
                }).OrderBy(c => c.TenGoi).ToList();

                return new JsonResult(new
                {
                    lstNhomBaiViet,
                    lstDanhMucBaiDang,
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
        public IActionResult LoadTable()
        {
            try
            {
                var lstData = db.WebDichVus.Where(c => c.IsBaiVietNoiBat == true).OrderByDescending(x => x.ThoiGianTao).Select(x => new
                {
                    x.IdDichVu,
                    x.UrlImage,
                    x.NameImage,
                    x.TieuDeBaiViet,
                    x.MoTaNgan,
                    x.NguoiTao,
                    x.IsCongKhai,
                    x.IsBaiVietNoiBat,
                    x.CbLoaiBaiDang,
                    x.TieuDeNgan,
                    x.ThoiGianTao,
                    x.SeoUrl,
                    x.SeoTittile,
                    x.SapXep,
                }).ToList().Select(x => new
                {
                    x.IdDichVu,
                    x.SapXep,
                    x.UrlImage,
                    x.NameImage,
                    x.TieuDeBaiViet,
                    x.MoTaNgan,
                    x.NguoiTao,
                    x.IsCongKhai,
                    x.IsBaiVietNoiBat,
                    x.CbLoaiBaiDang,
                    x.TieuDeNgan,
                    x.SeoUrl,
                    x.SeoTittile,
                    IsTrangchu = x.SeoTittile == "1" ? true : false,
                    ThoiGianTao = x.ThoiGianTao != null ? string.Format("{0:dd-MM-yyyy}", x.ThoiGianTao) : "",
                });

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
        public IActionResult DeleteData(Guid? IdDichVu)
        {
            try
            {
                if (IdDichVu != Guid.Empty)
                {
                    var find_data = db.WebDichVus.Find(IdDichVu);
                    if (find_data != null)
                    {
                        db.WebDichVus.Remove(find_data);
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

        [HttpGet]
        public IActionResult LoadDetail(Guid? IdDichVu)
        {
            try
            {
                var lstData = db.WebDichVus.Where(c => c.IdDichVu == IdDichVu).Select(x => new
                {
                    x.IdDichVu,
                    x.TieuDeBaiViet,
                    x.NguoiTao,
                    x.NoiDung,
                    x.IsCongKhai,
                    x.IsBaiVietNoiBat,
                    x.CbNhomBaiViet,
                    x.UrlImage,
                    x.MoTaNgan,
                    x.CbLoaiBaiDang,
                    x.TieuDeNgan,
                    x.SeoTittile,
                    x.SeoUrl
                }).ToList().Select(x => new
                {
                    x.IdDichVu,
                    x.TieuDeBaiViet,
                    x.NguoiTao,
                    x.IsCongKhai,
                    x.IsBaiVietNoiBat,
                    x.NoiDung,
                    x.CbNhomBaiViet,
                    x.UrlImage,
                    x.MoTaNgan,
                    x.CbLoaiBaiDang,
                    x.TieuDeNgan,
                    x.SeoTittile,
                    x.SeoUrl
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
        public async Task<IActionResult> SaveData([FromForm] string strData, [FromForm] bool isThayDoi, IFormFileCollection files)
        {
            string decodedContent = "";
            string duong_dan_tai_lieu = "";
            string ten_file = "";
            var ClientData = System.Text.Json.JsonSerializer.Deserialize<WebDichVu>(strData);
            if (!string.IsNullOrEmpty(ClientData.NoiDung))
            {
                decodedContent = HttpUtility.UrlDecode(ClientData.NoiDung);
            }
            if (!string.IsNullOrEmpty(ClientData.SeoUrl))
            {
                var check = db.WebDichVus.FirstOrDefault(c => (ClientData.IdDichVu == Guid.Empty && c.SeoUrl.Trim() == ClientData.SeoUrl.Trim()) || (c.SeoUrl.Trim() == ClientData.SeoUrl.Trim() && c.IdDichVu != ClientData.IdDichVu));
                if (check != null)
                {
                    return Json(new
                    {
                        message = "Đường dẫn SEO đã tồn tại, vui lòng nhập đường dẫn khác.",
                        status = false
                    });
                }
            }
            try
            {
                if (files != null && files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            ten_file = file.FileName;
                            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                            var pathName = Path.Combine("FilesUploads", "BaiViet_" + DateTime.Now.ToString("yyyyMM"));
                            duong_dan_tai_lieu = Path.Combine(pathName, fileName);

                            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", pathName);
                            if (!Directory.Exists(uploadPath))
                            {
                                Directory.CreateDirectory(uploadPath);
                            }

                            var filePath = Path.Combine(uploadPath, fileName);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }
                        }
                    }
                }
                if (ClientData.IdDichVu == Guid.Empty)
                {
                    ClientData.IdDichVu = Guid.NewGuid();
                    if (isThayDoi)
                    {
                        ClientData.UrlImage = "/" + duong_dan_tai_lieu.Replace("\\", "/");
                        ClientData.NameImage = ten_file;
                    }
                    ClientData.NoiDung = decodedContent;
                    ClientData.IsBaiVietNoiBat = true;
                    ClientData.NguoiTao = User.Identity.Name;
                    ClientData.ThoiGianTao = DateTime.Now;
                    db.WebDichVus.Add(ClientData);
                }
                else
                {
                    var existing = db.WebDichVus.FirstOrDefault(x => x.IdDichVu == ClientData.IdDichVu);
                    if (existing != null)
                    {
                        if (isThayDoi)
                        {
                            ClientData.UrlImage = "/" + duong_dan_tai_lieu.Replace("\\", "/");
                            ClientData.NameImage = ten_file;
                            existing.UrlImage = ClientData.UrlImage;
                            existing.NameImage = ClientData.NameImage;
                        }
                        //existing.TieuDeNgan = ClientData.TieuDeNgan;
                        existing.TieuDeBaiViet = ClientData.TieuDeBaiViet;
                        existing.NoiDung = decodedContent;
                        existing.MoTaNgan = ClientData.MoTaNgan;
                        existing.SapXep = ClientData.SapXep;
                        existing.IsBaiVietNoiBat = true;
                        existing.IsCongKhai = ClientData.IsCongKhai;
                        existing.CbLoaiBaiDang = ClientData.CbLoaiBaiDang;
                        existing.CbNhomBaiViet = ClientData.CbNhomBaiViet;
                        existing.SeoTittile = ClientData.SeoTittile;
                        existing.SeoUrl = ClientData.SeoUrl;
                        existing.NguoiTao = User.Identity.Name;
                        existing.ThoiGianCapNhap = DateTime.Now;
                        db.Entry(existing).State = EntityState.Modified;
                    }
                }
                db.SaveChanges();
                return Json(new { status = true });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    message = ex.Message,
                    status = false
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(List<IFormFile> files, Guid IdDichVu)
        {
            try
            {
                if (files == null || files.Count == 0)
                    return Json(new { status = false, message = "Không có file nào được gửi lên." });

                var file = files[0]; // Lấy file đầu tiên

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "Icons");
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
                var fileUrl = "/Uploads/Icons/" + uniqueFileName;

                var record = db.WebDichVus.FirstOrDefault(x => x.IdDichVu == IdDichVu);
                if (record != null)
                {
                    record.TieuDeNgan = fileUrl;
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
        public async Task<IActionResult> DeleteFile(Guid IdDichVu)
        {
            try
            {
                var record = db.WebDichVus.FirstOrDefault(x => x.IdDichVu == IdDichVu);
                if (record != null && !string.IsNullOrEmpty(record.TieuDeNgan))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", record.TieuDeNgan.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    record.TieuDeNgan = null;
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
        public IActionResult SaveDataCongKhai(Guid IdDichVu, bool? IsTrangchu)
        {
            try
            {
                var find_data = db.WebDichVus.Find(IdDichVu);

                if (find_data != null)
                {
                    find_data.SeoTittile = IsTrangchu == true ? "1" : "0";
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
    }
}
