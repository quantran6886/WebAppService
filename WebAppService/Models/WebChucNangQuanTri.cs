using System;
using System.Collections.Generic;

namespace WebAppService.Models;

public partial class WebChucNangQuanTri
{
    public long IdChucNang { get; set; }

    public int? SoThuTu { get; set; }

    public string? TenMenu { get; set; }

    public string? UrlDuongDan { get; set; }

    public string? Controller { get; set; }

    public string? Action { get; set; }

    public bool? IsSuDung { get; set; }

    public virtual ICollection<WebPhanQuyenQuanTri> WebPhanQuyenQuanTris { get; set; } = new List<WebPhanQuyenQuanTri>();
}
