using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web;
using WebAppService.Models;

namespace WebAppService.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class SeoInfomationController : Controller
    {
        public IActionResult SeoInfomation()
        {
            return View();
        }

        AppDbContext db = new AppDbContext();


        [HttpPost]
        public async Task<IActionResult> SaveData([FromForm] string strData, [FromForm] bool isThayDoi, IFormFileCollection files)
        {
            string duong_dan_tai_lieu = "";
            string ten_file = "";
            string decodedContent = "";
            var ClientData = System.Text.Json.JsonSerializer.Deserialize<BrowerTaiKhoanDangKy>(strData);
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
                            var pathName = Path.Combine("FilesUploads", "Config_" + DateTime.Now.ToString("yyyyMM"));
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

                var findData = db.BrowerTaiKhoanDangKies.FirstOrDefault();
                if (findData != null)
                {

                    if (isThayDoi)
                    {
                        findData.UrlTaiLieu = "/" + duong_dan_tai_lieu.Replace("\\", "/");
                        findData.NameTaiLieu = ten_file;
                    }
                    findData.Email = ClientData.Email;
                    findData.TenToChuc = ClientData.TenToChuc;
                    findData.SoDienThoai = ClientData.SoDienThoai;
                    findData.ChucVu = ClientData.ChucVu;
                    findData.MaSoThue = ClientData.MaSoThue;
                    findData.NoiCapMst = ClientData.NoiCapMst;
                    findData.DiaChiDoanhNghiep = ClientData.DiaChiDoanhNghiep;
                    findData.Ho = ClientData.Ho;
                    findData.TenDem = ClientData.TenDem;
                    findData.Ten = ClientData.Ten;
                    findData.PasswordHash = ClientData.PasswordHash;
                    db.Entry(findData).State = EntityState.Modified;
                }
                else
                {
                    ClientData.IdTaiKhoan = 0;
                    if (isThayDoi)
                    {
                        ClientData.UrlTaiLieu = "/" + duong_dan_tai_lieu.Replace("\\", "/");
                        ClientData.NameTaiLieu = ten_file;
                    }
                    db.BrowerTaiKhoanDangKies.Add(ClientData);

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
        public IActionResult LoadDetail()
        {
            try
            {
                var lstData = db.BrowerTaiKhoanDangKies.AsEnumerable().Select(c => new
                {
                    c.IdTaiKhoan,
                    c.Email,
                    c.TenToChuc,
                    c.SoDienThoai,
                    c.ChucVu,
                    c.MaSoThue,
                    c.NoiCapMst,
                    c.DiaChiDoanhNghiep,
                    c.Ho,
                    c.TenDem,
                    c.Ten,
                    c.PasswordHash,
                    c.UrlTaiLieu,
                    c.NameTaiLieu,
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
