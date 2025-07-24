using Microsoft.AspNetCore.Mvc;
using NotificationLibrary.NotificationControl;
using WebAppService.Models.Updates;

namespace WebAppService.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserStatusController : Controller
    {
        public async Task<IActionResult> CheckInactiveUsers()
        {
            DateTime timeThreshold = DateTime.Now.AddMinutes(-5);

            using (var db = new AppDbContext())
            {
                var inactiveUsers = db.WebUserOnlines
                    .Where(u => u.IsOnline == true && u.LastActive < timeThreshold)
                    .ToList();

                foreach (var user in inactiveUsers)
                {
                    user.IsOnline = false;
                    db.Update(user);
                }

                await db.SaveChangesAsync();
            }
            return Ok("Cập nhật trạng thái Offline cho user không hoạt động.");
        }

       [HttpGet]
        public IActionResult LoadData(string userId)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var lstData = db.WebUserOnlines.AsEnumerable().Select(c => new
                    {
                        c.Id,
                        HoTen = "",
                        c.IsOnline,
                        c.UserName,
                        c.ComputerName,
                        c.IpAddress,
                        LastActive = c.LastActive != null? string.Format("{0:HH:mm:ss / dd-MM-yyyy}",c.LastActive) :"",
                    }).ToList();

                    return new JsonResult(new
                    {
                        lstData,
                        status = true
                    });
                }
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
