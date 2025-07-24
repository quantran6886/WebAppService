using Microsoft.AspNetCore.Mvc;
using WebAppService.Models.Updates;

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
                return BadRequest("Không tìm thấy thông tin.");
            }
            var  record = db.WebNhanSus.FirstOrDefault(c => c.IdNhanSu.ToString() == id);
            return View(record);
        }

        AppDbContext db = new AppDbContext();

        [HttpGet]
        public IActionResult LoadData(int page, int pageSize)
        {
            try
            {
                var allData = db.WebNhanSus.ToList();

                var lstData = allData.Select(c => new
                {
                    c.IdNhanSu,
                    c.NameImage,
                    c.UrlImage,
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
