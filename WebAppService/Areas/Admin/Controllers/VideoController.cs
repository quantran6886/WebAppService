using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppService.Models;

namespace WebAppService.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class VideoController : Controller
    {
        public IActionResult Video()
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
                var lstData = db.WebVideos.OrderByDescending(x => x.ThoiGianTao).Select(x => new
                {
                    x.IdVideo,
                    x.UrlImage,
                    x.NameImage,
                    x.UrlVideo,
                    x.NameVideo,
                    x.TieuDeBaiViet,
                    x.MoTaNgan,
                    x.NguoiTao,
                    x.IsCongKhai,
                    x.IsVideoNoiBat,
                    x.TieuDeNgan,
                    x.ThoiGianTao,
                }).ToList().Select(x => new
                {
                    x.IdVideo,
                    x.UrlImage,
                    x.NameImage,
                    x.UrlVideo,
                    x.NameVideo,
                    x.TieuDeBaiViet,
                    x.MoTaNgan,
                    x.NguoiTao,
                    x.IsCongKhai,
                    x.IsVideoNoiBat,
                    x.TieuDeNgan,
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
        public IActionResult DeleteData(Guid? IdVideo)
        {
            try
            {
                if (IdVideo != Guid.Empty)
                {
                    var find_data = db.WebVideos.Find(IdVideo);
                    if (find_data != null)
                    {
                        db.WebVideos.Remove(find_data);
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
        public IActionResult LoadDetail(Guid? IdVideo)
        {
            try
            {
                var lstData = db.WebVideos.Where(c => c.IdVideo == IdVideo).Select(x => new
                {
                    x.IdVideo,
                    x.SapXep,
                    x.TieuDeBaiViet,
                    x.NguoiTao,
                    x.IsCongKhai,
                    x.IsVideoNoiBat,
                    x.UrlImage,
                    x.UrlVideo,
                    x.NameVideo,
                    x.MoTaNgan,
                    x.TieuDeNgan,
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
        public async Task<IActionResult> SaveData([FromForm] string strData, [FromForm] bool isThayDoi, [FromForm] bool isThayDoi2, IFormFileCollection files, IFormFileCollection videos)
        {
            string duong_dan_tai_lieu = "";
            string ten_file = "";
            string duong_dan_video = "";
            string ten_video = "";
            var ClientData = System.Text.Json.JsonSerializer.Deserialize<WebVideo>(strData);
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
                            var pathName = Path.Combine("FilesUploads", "Video_" + DateTime.Now.ToString("yyyyMM"));
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
                if (ClientData.IdVideo == Guid.Empty)
                {
                    ClientData.IdVideo = Guid.NewGuid();
                    if (isThayDoi)
                    {
                        ClientData.UrlImage = "/" + duong_dan_tai_lieu.Replace("\\", "/");
                        ClientData.NameImage = ten_file;
                    }
                    ClientData.UrlVideo = ClientData.UrlVideo;
                    ClientData.TieuDeNgan = ClientData.TieuDeNgan;
                    ClientData.TieuDeBaiViet = ClientData.TieuDeBaiViet;
                    ClientData.MoTaNgan = ClientData.MoTaNgan;
                    ClientData.SapXep = ClientData.SapXep;
                    ClientData.IsVideoNoiBat = ClientData.IsVideoNoiBat;
                    ClientData.IsCongKhai = ClientData.IsCongKhai;
                    ClientData.NguoiTao = User.Identity.Name;
                    ClientData.ThoiGianTao = DateTime.Now;
                    db.WebVideos.Add(ClientData);
                }
                else
                {
                    var existing = db.WebVideos.FirstOrDefault(x => x.IdVideo == ClientData.IdVideo);
                    if (existing != null)
                    {
                        if (isThayDoi)
                        {
                            existing.UrlImage = "/" + duong_dan_tai_lieu.Replace("\\", "/");
                            existing.NameImage = ten_file;
                        }
                        existing.UrlVideo = ClientData.UrlVideo ;
                        existing.TieuDeNgan = ClientData.TieuDeNgan;
                        existing.TieuDeBaiViet = ClientData.TieuDeBaiViet;
                        existing.MoTaNgan = ClientData.MoTaNgan;
                        existing.SapXep = ClientData.SapXep;
                        existing.IsVideoNoiBat = ClientData.IsVideoNoiBat;
                        existing.IsCongKhai = ClientData.IsCongKhai;
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
