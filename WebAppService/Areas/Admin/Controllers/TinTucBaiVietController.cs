using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppService.Models.Updates;

namespace WebAppService.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class TinTucBaiVietController : Controller
    {
        public IActionResult TinTucBaiViet()
        {
            return View();
        }
        AppDbContext db = new AppDbContext();

        [HttpGet]
        public IActionResult LoadTable()
        {
            try
            {
                var lstData = db.WebTinTucBaiViets
                .OrderByDescending(x => x.ThoiGianTao)
                .Select(x => new
                {
                    x.IdBaiViet,
                    x.TieuDeBaiViet,
                    x.NguoiTao,
                    x.IsCongKhai,
                    x.IsBaiVietNoiBat,
                    x.ThoiGianTao,
                    x.ThoiGianCapNhap
                })
                .ToList();
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
        public async Task<IActionResult> SaveData([FromForm] string strData, [FromForm] bool isThayDoi, IFormFileCollection files)
        {
            string duong_dan_tai_lieu = "";
            string ten_file = "";
            var ClientData = System.Text.Json.JsonSerializer.Deserialize<WebTinTucBaiViet>(strData);

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
                if (ClientData.IdBaiViet == Guid.Empty)
                {
                    ClientData.IdBaiViet = Guid.NewGuid();
                    if (isThayDoi)
                    {
                        ClientData.UrlImage = "/" + duong_dan_tai_lieu.Replace("\\", "/");
                        ClientData.NameImage = ten_file;
                    }
                    ClientData.TieuDeNgan = ClientData.TieuDeNgan;
                    ClientData.TieuDeBaiViet= ClientData.TieuDeBaiViet;
                    ClientData.NoiDung = ClientData.NoiDung;
                    ClientData.IsBaiVietNoiBat = ClientData.IsBaiVietNoiBat;
                    ClientData.IsCongKhai = ClientData.IsCongKhai;
                    ClientData.CbLoaiBaiDang = ClientData.CbLoaiBaiDang;
                    ClientData.CbNhomBaiViet = ClientData.CbNhomBaiViet;
                    db.WebTinTucBaiViets.Add(ClientData);
                }
                else
                {
                    var existing = db.WebTinTucBaiViets.FirstOrDefault(x => x.IdBaiViet == ClientData.IdBaiViet);
                    if (existing != null)
                    {
                        if (isThayDoi)
                        {
                            ClientData.UrlImage = "/" + duong_dan_tai_lieu.Replace("\\", "/");
                            ClientData.NameImage = ten_file;
                            existing.UrlImage = ClientData.UrlImage;
                            existing.NameImage = ClientData.NameImage;
                        }
                        existing.TieuDeNgan = ClientData.TieuDeNgan;
                        existing.TieuDeBaiViet = ClientData.TieuDeBaiViet;
                        existing.NoiDung = ClientData.NoiDung;
                        existing.IsBaiVietNoiBat = ClientData.IsBaiVietNoiBat;
                        existing.IsCongKhai = ClientData.IsCongKhai;
                        existing.CbLoaiBaiDang = ClientData.CbLoaiBaiDang;
                        existing.CbNhomBaiViet = ClientData.CbNhomBaiViet;
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
    }
}
