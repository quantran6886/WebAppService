using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using WebAppService.Models;

namespace NotificationLibrary.NotificationControl
{
    public class UserStatusHub : Hub
    {
        private static ConcurrentDictionary<string, string> OnlineUsers = new ConcurrentDictionary<string, string>();

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            string Id = httpContext.Session.GetString("SessUserId");

            if (string.IsNullOrEmpty(Id))
            {
                Console.WriteLine("⚠ Không tìm thấy UserId trong session.");
                return;
            }

            string ipAddress = httpContext.Connection.RemoteIpAddress?.ToString();
            string computerName = "Unknown";

            try
            {
                if (!string.IsNullOrEmpty(ipAddress))
                {
                    computerName = Dns.GetHostEntry(ipAddress).HostName;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠ Lỗi lấy tên máy tính: {ex.Message}");
            }

            Console.WriteLine($"👤 {Id} đang online từ {ipAddress} - {computerName}");

            OnlineUsers[Id] = Context.ConnectionId;

            // Cập nhật trạng thái vào database
            using (var db = new AppDbContext())
            {
                var userStatus = db.WebUserOnlines.Find(Id);
                if (userStatus == null)
                {
                    db.WebUserOnlines.Add(new WebUserOnline
                    {
                        Id = Id,
                        IsOnline = true,
                        LastActive = DateTime.Now,
                        IpAddress = ipAddress,
                        ComputerName = computerName,
                        UserName = ""
                    });
                }
                else
                {
                    userStatus.IsOnline = true;
                    userStatus.LastActive = DateTime.Now;
                    userStatus.IpAddress = ipAddress;
                    userStatus.ComputerName = computerName;
                    db.Update(userStatus);
                }
                await db.SaveChangesAsync();
            }

            await Clients.All.SendAsync("UserOnline", Id, ipAddress, computerName);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var httpContext = Context.GetHttpContext();
            string Id = httpContext.Session.GetString("SessUserId");

            if (!string.IsNullOrEmpty(Id))
            {
                OnlineUsers.TryRemove(Id, out _);

                // Cập nhật trạng thái offline vào database
                using (var db = new AppDbContext())
                {
                    var userStatus = db.WebUserOnlines.Find(Id);
                    if (userStatus != null)
                    {
                        userStatus.IsOnline = false;
                        userStatus.LastActive = DateTime.Now;
                        db.Update(userStatus);
                        await db.SaveChangesAsync();
                    }
                }

                await Clients.All.SendAsync("UserOffline", Id);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
