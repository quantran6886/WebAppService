using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppService.Areas.Admin._Helper;
using WebAppService.Models;

namespace WebAppService.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class DanhSachNhanSuController : AdminBaseController
    {
        public IActionResult DanhSachNhanSu()
        {
            return View();
        }
        AppDbContext db = new AppDbContext();

        [HttpGet]
        public IActionResult LoadData()
        {
            try
            {
                var lstChucDanh = db.WebDanhMucHeThongs.Where(c => c.LoaiDanhMuc == "Danh mục chức danh").Select(c => new
                {
                    c.IdHeThong,
                    c.ThuTuTg,
                    c.TenGoi,
                    c.GhiChu,
                }).ToList();

                return new JsonResult(new
                {
                    lstChucDanh,
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
            string duong_dan_tai_lieu = "";
            string ten_file = "";
            var ClientData = System.Text.Json.JsonSerializer.Deserialize<WebNhanSu>(strData);

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
                            var pathName = Path.Combine("FilesUploads", "NhanSu_" + DateTime.Now.ToString("yyyyMM"));
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
                if (ClientData.IdNhanSu == Guid.Empty)
                {
                    ClientData.IdNhanSu = Guid.NewGuid();
                    if (isThayDoi)
                    {
                        ClientData.UrlImage = "/" + duong_dan_tai_lieu.Replace("\\", "/");
                        ClientData.NameImage = ten_file;
                    }
                    ClientData.HoTen = ClientData.HoTen;
                    ClientData.NgaySinh = ClientData.NgaySinh;
                    ClientData.ChucDanh = ClientData.ChucDanh;
                    ClientData.DonViKhoa = ClientData.DonViKhoa;
                    ClientData.BangCapHocVi = ClientData.BangCapHocVi;
                    ClientData.NgonNgu = ClientData.NgonNgu;
                    ClientData.KinhNghiemLamViec = ClientData.KinhNghiemLamViec;
                    db.WebNhanSus.Add(ClientData);
                }
                else
                {
                    var existing = db.WebNhanSus.FirstOrDefault(x => x.IdNhanSu == ClientData.IdNhanSu);
                    if (existing != null)
                    {
                        if (isThayDoi)
                        {
                            ClientData.UrlImage = "/" + duong_dan_tai_lieu.Replace("\\", "/");
                            ClientData.NameImage = ten_file;
                            existing.UrlImage = ClientData.UrlImage;
                            existing.NameImage = ClientData.NameImage;
                        }
                        existing.HoTen = ClientData.HoTen;
                        existing.NgaySinh = ClientData.NgaySinh;
                        existing.ChucDanh = ClientData.ChucDanh;
                        existing.DonViKhoa = ClientData.DonViKhoa;
                        existing.BangCapHocVi = ClientData.BangCapHocVi;
                        existing.NgonNgu = ClientData.NgonNgu;
                        existing.KinhNghiemLamViec = ClientData.KinhNghiemLamViec;
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


        [HttpGet]
        public IActionResult LoadTable()
        {
            try
            {
                var lstData = db.WebNhanSus.AsEnumerable().Select(c => new
                {
                    c.IdNhanSu,
                    c.HoTen,
                    c.ChucDanh,
                    c.DonViKhoa,
                    c.BangCapHocVi,
                    c.NgonNgu,
                    NgaySinh = c.NgaySinh != null ? string.Format("{0:dd-MM-yyyy}",c.NgaySinh) : "",
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
        public IActionResult LoadDetail(Guid? IdNhanSu)
        {
            try
            {
                var lstData = db.WebNhanSus.AsEnumerable().Where(c => c.IdNhanSu == IdNhanSu).Select(c => new
                {
                    c.IdNhanSu,
                    c.HoTen,
                    c.ChucDanh,
                    c.DonViKhoa,
                    c.BangCapHocVi,
                    c.NgonNgu,
                    NgaySinh = c.NgaySinh != null ? string.Format("{0:yyyy-MM-dd}", c.NgaySinh) : "",
                    c.UrlImage,
                    c.NameImage,
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
        public IActionResult DeleteData(Guid? IdNhanSu)
        {
            try
            {
                if (IdNhanSu != Guid.Empty)
                {
                    var find_data = db.WebNhanSus.Find(IdNhanSu);
                    if (find_data != null)
                    {
                        db.WebNhanSus.Remove(find_data);
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
