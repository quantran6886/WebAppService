using Microsoft.AspNetCore.Mvc;

namespace clinic_website.Controllers
{
	public class FAQController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
