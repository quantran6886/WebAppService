using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAppService.Areas.Admin._Helper
{
    public class AdminBaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Kiểm tra nếu user chưa đăng nhập
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                string returnUrl = context.HttpContext.Request.Path;

                // Tránh redirect lặp vào chính trang Login
                if (!returnUrl.StartsWith("/Admin/Login/Login"))
                {
                    context.Result = new RedirectToActionResult("Login", "Admin/Login",
                        new { ReturnUrl = returnUrl });
                }
            }

            base.OnActionExecuting(context);
        }
    }
}
