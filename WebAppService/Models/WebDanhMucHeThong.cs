using System;
using System.Collections.Generic;

namespace WebAppService.Models;

public partial class WebDanhMucHeThong
{
    public long IdHeThong { get; set; }

    public int? ThuTuLdm { get; set; }

    public int? ThuTuTg { get; set; }

    public string? LoaiDanhMuc { get; set; }

    public string? TenGoi { get; set; }

    public string? GhiChu { get; set; }

    public bool? IsPhanLoai { get; set; }

    public string? MauSac { get; set; }
}
