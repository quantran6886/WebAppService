using System;
using System.Collections.Generic;

namespace WebAppService.Models;

public partial class BrowerHomePage
{
    public long Id { get; set; }

    public int? SoThuTu { get; set; }

    public string? MoTaHienThi { get; set; }

    public string? TenTieuDe { get; set; }

    public string? Link { get; set; }

    public string? Tag { get; set; }

    public string? PhanLoai { get; set; }

    public bool? IsBanerHome { get; set; }
    public string? SeoUrl { get; set; }

    public string? SeoTittile { get; set; }
}
