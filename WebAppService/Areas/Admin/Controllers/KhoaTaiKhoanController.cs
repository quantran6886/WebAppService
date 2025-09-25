using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebAppService.Models;

namespace WebAppService.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class KhoaTaiKhoanController : Controller
    {
        public IActionResult KhoaTaiKhoan()
        {
            return View();
        }
        AppDbContext db = new AppDbContext();

        [HttpGet]
        public IActionResult LoadTable()
        {
            try
            {
                var lstData = db.AspNetUsers.Select(x => new
                {
                    x.Id,
                    x.UserName,
                    x.PasswordHash,
                    Roles = x.Roles.AsEnumerable().FirstOrDefault().Name,
                    x.TwoFactorEnabled,
                }).Where(c => c.Roles != "SuperAdmin").ToList();

                return new JsonResult(new
                {
                    lstData,
                    status = true
                });
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

        [HttpPost]
        public IActionResult SaveData(Int64 Id, bool? IsLock)
        {
            try
            {
                var find_data = db.AspNetUsers.Where(c => c.Id == Id.ToString()).FirstOrDefault();

                if (find_data != null)
                {
                    find_data.TwoFactorEnabled = IsLock == true ? true : false;
                    find_data.PasswordHash = IsLock == true ? "" : "AQAAAAIAAYagAAAAELNCBKFKZtUtSW+32OyL/2iFO/osVLmQdUDjDt/9H0j+rxKEQb2qSEPwmpfwjQmJtw==\t";
                }
                db.SaveChanges();

                return new JsonResult(new
                {
                    status = true
                });
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
