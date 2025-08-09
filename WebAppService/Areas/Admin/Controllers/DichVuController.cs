using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web;
using WebAppService.Models;

namespace WebAppService.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class DichVuController : Controller
    {
        public IActionResult DichVu()
        {
            return View();
        }
     
        AppDbContext db = new AppDbContext();

        [HttpGet]
        public IActionResult LoadData()
        {
            try
            {
                var lstNhomBaiViet1 = db.WebDanhMucHeThongs.Where(c => c.LoaiDanhMuc == "Danh mục nhóm dịch vụ chuyên khoa").Select(c => new
                {
                    c.IdHeThong,
                    c.ThuTuTg,
                    c.TenGoi,
                    c.GhiChu,
                }).OrderBy(c => c.TenGoi).ToList();

                var lstNhomBaiViet2 = db.WebDichVus.Where(c => c.IsBaiVietNoiBat == true).Select(c => new
                {
                    c.IdDichVu,
                    TenGoi = !string.IsNullOrEmpty(c.TieuDeBaiViet) ? c.TieuDeBaiViet : "",
                }).OrderBy(c => c.TenGoi).ToList();

                return new JsonResult(new
                {
                    lstNhomBaiViet1,
                    lstNhomBaiViet2,
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
                var lstData = db.WebDichVus.Where(c => c.IsBaiVietNoiBat != true).OrderByDescending(x => x.ThoiGianTao).Select(x => new
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
                    x.CbNhomBaiViet,
                    x.TieuDeNgan,
                    x.ThoiGianTao,
                    x.SeoUrl,
                    x.SeoTittile,
                }).ToList().Select(x => new
                {
                    x.IdDichVu,
                    x.UrlImage,
                    x.NameImage,
                    x.TieuDeBaiViet,
                    x.MoTaNgan,
                    x.NguoiTao,
                    x.IsCongKhai,
                    x.IsBaiVietNoiBat,
                    CbLoaiBaiDang = x.CbLoaiBaiDang == "1" ? "<p class='text-danger'> DV chuyên khoa </p>" : x.CbLoaiBaiDang == "2" ? "<p class='text-success'>DV đặc biệt</p>" : "",
                    x.CbNhomBaiViet,
                    x.TieuDeNgan,
                    x.SeoUrl,
                    x.SeoTittile,
                    ThoiGianTao =  x.ThoiGianTao != null ? string.Format("{0:dd-MM-yyyy}",x.ThoiGianTao) : "",
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
                    x.SeoUrl,
                    x.SeoTittile,
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
                    x.SeoUrl,
                    x.SeoTittile,
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
                    //ClientData.TieuDeNgan = ClientData.TieuDeNgan;
                    //ClientData.TieuDeBaiViet= ClientData.TieuDeBaiViet;
                    //ClientData.MoTaNgan = ClientData.MoTaNgan;
                    //ClientData.SapXep = ClientData.SapXep;
                    //ClientData.NoiDung = decodedContent;
                    //ClientData.IsBaiVietNoiBat = ClientData.IsBaiVietNoiBat;
                    //ClientData.IsCongKhai = ClientData.IsCongKhai;
                    //ClientData.CbLoaiBaiDang = ClientData.CbLoaiBaiDang;
                    //ClientData.CbNhomBaiViet = ClientData.CbNhomBaiViet;
                    //ClientData.SeoTittile = ClientData.SeoUrl;
                    //ClientData.NguoiTao = User.Identity.Name;
                    ClientData.ThoiGianTao = DateTime.Now;  
                    ClientData.ThoiGianCapNhap = DateTime.Now;
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
                        existing.TieuDeNgan = ClientData.TieuDeNgan;
                        existing.TieuDeBaiViet = ClientData.TieuDeBaiViet;
                        existing.NoiDung = decodedContent;
                        existing.MoTaNgan = ClientData.MoTaNgan;
                        existing.SapXep = ClientData.SapXep;
                        existing.IsBaiVietNoiBat = ClientData.IsBaiVietNoiBat;
                        existing.IsCongKhai = ClientData.IsCongKhai;
                        existing.CbLoaiBaiDang = ClientData.CbLoaiBaiDang;
                        existing.CbNhomBaiViet = ClientData.CbNhomBaiViet;
                        existing.SeoUrl = ClientData.SeoUrl;
                        existing.SeoTittile = ClientData.SeoTittile;
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
    }
}
