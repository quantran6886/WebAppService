using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using NotificationLibrary.NotificationControl;
using WebAppService.Areas.Admin._Helper;
using WebAppService.Areas.Admin.Modals;
using WebAppService.Models.Updates;

namespace WebAppService.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Login()
        {
            if (_httpContextAccessor.HttpContext.Session.GetString("SessUserId") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var userId = HttpContext.Session.GetString("SessUserId");

                if (!string.IsNullOrEmpty(userId))
                {
                    using (AppDbContext db = new AppDbContext())
                    {
                        var userStatus = db.WebUserOnlines.Find(userId);
                        if (userStatus != null)
                        {
                            // Đánh dấu user là offline
                            userStatus.IsOnline = false;
                            userStatus.LastActive = DateTime.Now;
                            db.Update(userStatus);
                            db.SaveChangesAsync();
                        }

                        // Gửi sự kiện SignalR báo user offline
                        var hubContext = HttpContext.RequestServices.GetService<IHubContext<UserStatusHub>>();
                        await hubContext.Clients.All.SendAsync("UserOffline", userId);
                    }
                }

                // Thực hiện logout bằng Identity
                await _signInManager.SignOutAsync();
                // Xóa Session
                HttpContext.Session.Remove("SessUserId");
                // Xóa Cookie
                Response.Cookies.Delete("SessUserId");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi logout: {ex.Message}");
            }

            return RedirectToAction("Login", "Login"); // Chuyển hướng về trang đăng nhập
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (AppDbContext _csdl = new AppDbContext())
                    {
                        var user = await _userManager.FindByNameAsync(model.UserName);
                        var userview = _csdl.ViewUserOnlines
                                            .Where(c => c.UserName.ToLower() == model.UserName.ToLower())
                                            .SingleOrDefault();

                        if (user != null)
                        {
                            var result = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);

                            if (result.Succeeded)
                            {
                                // Lưu vào Session
                                _httpContextAccessor.HttpContext.Session.SetString("SessUserId", user.Id);
                                SessionHelper.SetUser(_httpContextAccessor.HttpContext.Session, userview);

                                // Lưu vào Cookie (1 ngày)
                                CookieOptions option = new CookieOptions
                                {
                                    Expires = DateTime.Now.AddDays(1)
                                };
                                Response.Cookies.Append("SessUserId", user.Id, option);

                                return !string.IsNullOrEmpty(returnUrl) ? LocalRedirect(returnUrl) : RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, "❌ Mật khẩu không đúng.");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "❌ Tài khoản không tồn tại.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                ModelState.AddModelError(string.Empty, "⚠️ Lỗi trong quá trình đăng nhập.");
            }
            return View(model);
        }


        public ViewUserOnline GetUser()
        {
            string userJson = _httpContextAccessor.HttpContext.Session.GetString("CurrentUser");
            if (!string.IsNullOrEmpty(userJson))
            {
                return JsonConvert.DeserializeObject<ViewUserOnline>(userJson);
            }
            return null;
        }
    }
}
