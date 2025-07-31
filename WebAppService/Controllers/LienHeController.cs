using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NotificationLibrary.NotificationControl;
using WebAppService.Models;

namespace WebAppService.Controllers
{
    public class LienHeController : Controller
    {
        public IActionResult LienHe()
        {
            var recapchakey = db.SYSVARs.FirstOrDefault(x => x.VARNAME == "RECAPCHAKEY");
            if (recapchakey != null)
            {
                ViewBag.ReCaptchaKey = recapchakey.VARVALUE;
            }
            else
            {
                ViewBag.ReCaptchaKey = "";
            }
            return View();
        }

        private readonly IHubContext<NotificationHub> _hubContext;
        AppDbContext db = new AppDbContext();
        public LienHeController(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> GuiThongBao(string ho_ten, string so_dien_thoai, string email,string loi_nhan)
        {
            WebThongBao _tb = new WebThongBao();
            _tb.HoTenNguoiGui = ho_ten;
            _tb.NoiDung = loi_nhan;
            _tb.Email = email;
            _tb.SoDienThoai = so_dien_thoai;
            _tb.ThoiGianTao = DateTime.Now;
            this.db.WebThongBaos.Add(_tb);
            db.SaveChanges();
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", ho_ten);
            return Ok(new { success = true });
        }

    }
}
