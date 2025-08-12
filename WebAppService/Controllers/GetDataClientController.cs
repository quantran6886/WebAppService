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
                var ListHome = db.WebCauHinhTrangs.AsNoTracking().Where(x => x.IsCongKhai == true && x.CbGiaoDien != "1").Select(c => new
                {
                    c.MaTrang,
                    c.SeoUrl,
                    c.TieuDe,
                    c.SapXep,
                    c.CbGiaoDien,
                    c.TxtCard1,
                }).OrderBy(c=> c.SapXep).ToList();

                var ListVideo = db.WebVideos.AsNoTracking().Where(x => x.IsCongKhai == true)
                    .Select(c => new
                    {
                        c.IdVideo,
                        c.TieuDeBaiViet,
                        c.MoTaNgan,
                        c.SeoUrl,
                        c.UrlImage,
                        c.IsVideoNoiBat
                    }).Take(3).OrderBy(c=> c.IsVideoNoiBat == true).Take(3).ToList();

                var ListDichVu = db.WebDichVus.AsNoTracking().Where(x => x.IsCongKhai == true && x.IsBaiVietNoiBat == true).Select(c => new
                {
                    c.TieuDeBaiViet,
                    c.IdDichVu,
                    c.SeoUrl,
                    c.CbNhomBaiViet,
                    c.CbLoaiBaiDang,
                    c.MoTaNgan
                }).ToList();

                var ListBaoChi = db.WebTinTucBaiViets.AsNoTracking().Where(x => x.IsCongKhai == true && x.CbLoaiBaiDang == "Báo chí")
                    .Select(c => new
                    {
                        c.IdBaiViet,
                        c.UrlImage,
                        c.TieuDeBaiViet,
                        c.MoTaNgan,
                        c.SeoUrl,
                        c.IsBaiVietNoiBat,
                    }).OrderBy(c => c.IsBaiVietNoiBat).Take(20).ToList();

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