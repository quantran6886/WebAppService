using System;
using System.Collections.Generic;

namespace WebAppService.Models.Updates;

public partial class WebThongBao
{
    public Guid IdNoti { get; set; }

    public string? IdNguoiGui { get; set; }

    public string? IdNguoiNhan { get; set; }

    public string? TieuDe { get; set; }

    public string? NoiDung { get; set; }

    public string? FileDinhKem { get; set; }

    public bool? IsDaDoc { get; set; }

    public string? CbLoaiTin { get; set; }

    public DateTime? ThoiGianTao { get; set; }

    public DateTime? ThoiGianCapNhap { get; set; }

    public TimeOnly? TimeOnline { get; set; }
}
