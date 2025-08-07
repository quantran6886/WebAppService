using System;
using System.Collections.Generic;

namespace WebAppService.Models;

public partial class WebNhanSu
{
    public Guid IdNhanSu { get; set; }

    public string HoTen { get; set; } = null!;

    public DateTime? NgaySinh { get; set; }

    public string? CbGioiTinh { get; set; }

    public string? ChucDanh { get; set; }

    public string? ChucVu { get; set; }

    public string? BangCapHocVi { get; set; }

    public string? NgonNgu { get; set; }

    public string? KinhNghiemLamViec { get; set; }

    public string? DonViKhoa { get; set; }

    public string? UrlImage { get; set; }

    public string? NameImage { get; set; }

    public DateTime? ThoiGianTao { get; set; }

    public DateTime? ThoiGianCapNhap { get; set; }
    public string? SeoUrl { get; set; }

    public string? SeoTittile { get; set; }
}
