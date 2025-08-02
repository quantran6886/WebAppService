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
        public IActionResult DoctorDetail(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return RedirectToAction("NotFound", "NotFound");
            }
            var  record = db.WebNhanSus.FirstOrDefault(c => c.IdNhanSu.ToString() == id);
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

    }
}
