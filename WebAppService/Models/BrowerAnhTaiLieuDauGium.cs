using System;
using System.Collections.Generic;

namespace WebAppService.Models.Updates;

public partial class BrowerAnhTaiLieuDauGium
{
    public long IdFile { get; set; }

    public long? IdTaiSan { get; set; }

    public int? SoThuTu { get; set; }

    public string? MoTa { get; set; }

    public string? UrlFile { get; set; }

    public string? NameFile { get; set; }

    public bool? IsTaiLieu { get; set; }

    public bool? IsAnh { get; set; }

    public bool? IsHienThiDau { get; set; }

    public bool? IsSuDung { get; set; }

    public virtual BrowerTaiSanDauGium? IdTaiSanNavigation { get; set; }
}
