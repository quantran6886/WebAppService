using Microsoft.AspNetCore.Mvc;

namespace clinic_website.Controllers
{
	public class ServiceController : Controller
	{
		public IActionResult ServiceList()
		{
			return View();
		}
		public IActionResult ServiceDetail()
		{
			return View();
		}
	}
}
