using System;
using System.Collections.Generic;

namespace WebAppService.Models;

public partial class WebVideo
{
    public Guid IdVideo { get; set; }

    public string TieuDeBaiViet { get; set; } = null!;

    public string? TieuDeNgan { get; set; }

    public string? MoTaNgan { get; set; }

    public string? NguoiTao { get; set; }

    public string? UrlImage { get; set; }

    public string? NameImage { get; set; }

    public string? NameVideo { get; set; }

    public string? UrlVideo { get; set; }

    public int? SapXep { get; set; }

    public bool? IsVideoNoiBat { get; set; }

    public bool? IsCongKhai { get; set; }

    public DateTime? ThoiGianTao { get; set; }

    public DateTime? ThoiGianCapNhap { get; set; }
    public string? SeoUrl { get; set; }

    public string? SeoTittile { get; set; }
}
