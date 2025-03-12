using Microsoft.AspNetCore.Mvc;
using NotificationLibrary;
using NotificationLibrary.NotificationControl;
using System.Threading.Tasks;

namespace WebAppService.Controllers
{
    [Area("Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendNotification([FromBody] string message)
        {
            await _notificationService.SendToAll(message);
            return Ok(new { Message = "Notification sent successfully!" });
        }
    }
}