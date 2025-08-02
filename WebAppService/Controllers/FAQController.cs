using Microsoft.AspNetCore.Mvc;
using WebAppService.Models;

namespace clinic_website.Controllers
{
	public class FAQController : Controller
	{
		public IActionResult Index()
		{
            var recapchakey = db.WebDanhMucHeThongs.Where(c => c.LoaiDanhMuc == "capcha").FirstOrDefault();
            if (recapchakey != null)
            {
                ViewBag.ReCaptchaKey = recapchakey.TenGoi;
            }
            else
            {
                ViewBag.ReCaptchaKey = "";
            }
            return View();
		}

        AppDbContext db = new AppDbContext();

        [HttpGet]
        public IActionResult LoadData(int page, int pageSize)
        {
            try
            {
                var allData = db.WebFaqs.ToList();

                var lstData = allData.Select(c => new
                {
                    c.IdFaqs,
                    c.SoThuTu,
                    CauHoi = string.IsNullOrEmpty(c.CauHoi) ? "" : c.CauHoi ,
                    CauTraLoi = string.IsNullOrEmpty(c.CauTraLoi) ? "" : c.CauTraLoi,
                }).OrderBy(c => c.SoThuTu)
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
