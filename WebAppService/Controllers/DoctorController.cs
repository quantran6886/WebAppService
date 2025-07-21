using Microsoft.AspNetCore.Mvc;

namespace clinic_website.Controllers
{
	public class DoctorController : Controller
	{
		public IActionResult ListDoctor()
		{
			return View();
		}
		public IActionResult DoctorDetail()
		{
			return View();
		}
	}
}
