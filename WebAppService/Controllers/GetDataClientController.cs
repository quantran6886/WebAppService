using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.WebSockets;
using WebAppService.Middlewares;
using WebAppService.Models;

namespace clinic_website.Controllers
{
    [Route("api")]
    [ApiController]
    public class GetDataClientController : Controller
    {
        AppDbContext db = new AppDbContext();

        [HttpGet("get-data-home")]
        public IActionResult GetHome()
        {
            try
            {
                var ListHome = db.WebCauHinhTrangs.AsNoTracking().Where(x => x.IsCongKhai == true).OrderBy(c=> c.SapXep).ToList();
                var ListVideo = db.WebVideos.AsNoTracking().Where(x => x.IsCongKhai == true).ToList();
                var ListDichVu = db.WebDichVus.AsNoTracking().Where(x => x.IsCongKhai == true).ToList();
                var ListBaoChi = db.WebTinTucBaiViets.AsNoTracking().Where(x => x.IsCongKhai == true && x.CbLoaiBaiDang == "Báo chí").ToList();

                return new JsonResult(new
                {
                    ListVideo,
                    ListHome,
                    ListDichVu,
                    ListBaoChi,
                    status = true
                });
            }
            catch (System.Exception ex)
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