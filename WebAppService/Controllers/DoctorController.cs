using Microsoft.AspNetCore.Mvc;
using WebAppService.Models;

namespace clinic_website.Controllers
{
	public class DoctorController : Controller
	{
		public IActionResult ListDoctor()
		{
			return View();
		}

        [HttpGet]
        [Route("bac-si/{id?}")]
        public IActionResult DoctorDetail(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return RedirectToAction("NotFound", "NotFound");
            }

            var record = db.WebNhanSus.FirstOrDefault(c => c.SeoUrl.ToString() == id);
            return View(record);
        }

        AppDbContext db = new AppDbContext();

        [HttpGet]
        public IActionResult LoadData(int page, int pageSize ,string? txtCoSo, string? txtChuyenKhoa)
        {
            try
            {
                var allData = db.WebNhanSus.Where(c => (!string.IsNullOrEmpty(txtCoSo) ? c.DonViKhoa == txtCoSo : true) 
                && (!string.IsNullOrEmpty(txtChuyenKhoa) ? c.ChucVu == txtChuyenKhoa : true)).ToList();

                var lstData = allData.Select(c => new
                {
                    c.IdNhanSu,
                    c.NameImage,
                    UrlImage = c.UrlImage != null ? c.UrlImage : "/root/6388000.png",
                    c.HoTen,
                    c.BangCapHocVi,
                    c.DonViKhoa,
                    c.ChucDanh,
                    c.SeoUrl,
                })
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

                return new JsonResult(new
                {
                    totalRow = allData.Count,
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
        public IActionResult LoadDanhMuc()
        {
            try
            {
                var listHethong = db.WebDanhMucHeThongs.OrderBy(c => c.ThuTuTg).ToList();

                var lstDonViKhoa = listHethong.Where(c => c.LoaiDanhMuc == "Danh mục cơ sở làm việc").Select(c => new
                {
                    c.IdHeThong,
                    c.ThuTuTg,
                    c.TenGoi,
                    c.GhiChu,
                }).ToList();

                var lstChuyenKhoa = listHethong.Where(c => c.LoaiDanhMuc == "Danh mục nhóm dịch vụ chuyên khoa").Select(c => new
                {
                    c.IdHeThong,
                    c.ThuTuTg,
                    c.TenGoi,
                    c.GhiChu,
                }).ToList();

                return new JsonResult(new
                {
                    lstDonViKhoa,
                    lstChuyenKhoa,
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
