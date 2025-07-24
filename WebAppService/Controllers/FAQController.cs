using Microsoft.AspNetCore.Mvc;
using WebAppService.Models.Updates;

namespace clinic_website.Controllers
{
	public class FAQController : Controller
	{
		public IActionResult Index()
		{
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
                    c.CauHoi,
                    c.CauTraLoi,

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
