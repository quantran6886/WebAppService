using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NotificationLibrary.NotificationControl;

namespace WebAppService.Controllers
{
    public class LienHeController : Controller
    {
        public IActionResult LienHe()
        {
            return View();
        }

        private readonly IHubContext<NotificationHub> _hubContext;

        public LienHeController(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> GuiThongBao(string ho_ten, string lien_he)
        {
            var message = new
            {
                HoTen = ho_ten,
                LienHe = lien_he,
                NgayGui = DateTime.Now
            };
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", message);
            return Ok(new { success = true });
        }

    }
}
