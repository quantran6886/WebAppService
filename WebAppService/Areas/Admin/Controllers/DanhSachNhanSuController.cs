using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppService.Areas.Admin._Helper;
using WebAppService.Models.Updates;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
                });

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
        public async Task<IActionResult> SaveData([FromForm] string strData, IFormFileCollection files)
        {
            string duong_dan_tai_lieu = "";
            string ten_file = "";
            var ClientData = System.Text.Json.JsonSerializer.Deserialize<BrowerKhachHangDoiTac>(strData);

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
                            var pathName = Path.Combine("FilesUploads", ClientData.IdKhachHang.ToString().Trim() + "_" + DateTime.Now.ToString("yyyyMM"));
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

                //if (ClientData.IdCanBo == 0)
                //{
                //    ClientData.url_avatar = duong_dan_tai_lieu.Replace("\\", "/");
                //    ClientData.name_avatar = ten_file;
                //    _query.AspHoSoCanBo.Add(ClientData);
                //    _query.SaveChanges();
                //}
                //else
                //{
                //    var _data = _query.AspHoSoCanBo.Find(ClientData.IdCanBo);
                //    if (_data != null)
                //    {
                //        _data.cb_trang_thai_lam_viec = ClientData.cb_trang_thai_lam_viec;
                //        _data.ngay_bat_dau_cong_tac = ClientData.ngay_bat_dau_cong_tac;
                //        _data.chuc_vu = ClientData.chuc_vu;
                //        _data.so_hieu_giay_to = ClientData.so_hieu_giay_to;
                //        _data.ho_ten = ClientData.ho_ten;
                //        _data.gioi_tinh = ClientData.gioi_tinh;
                //        _data.ngay_sinh = ClientData.ngay_sinh;
                //        _data.so_dien_thoai = ClientData.so_dien_thoai;
                //        _data.email = ClientData.email;
                //        _data.cb_ngan_hang = ClientData.cb_ngan_hang;
                //        _data.so_tai_khoan = ClientData.so_tai_khoan;
                //        _data.cb_tinh = ClientData.cb_tinh;
                //        _data.cb_quan_huyen = ClientData.cb_quan_huyen;
                //        _data.cb_xa_phuong = ClientData.cb_xa_phuong;
                //        _data.dia_chi = ClientData.dia_chi;

                //        if (!string.IsNullOrEmpty(duong_dan_tai_lieu))
                //        {
                //            _data.url_avatar = duong_dan_tai_lieu.Replace("\\", "/");
                //            _data.name_avatar = ten_file;
                //        }
                //    }
                //    _query.SaveChanges();
                //}

                return Json(new { code = true });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    message = ex.Message,
                    code = false
                });
            }
        }

    }
}
