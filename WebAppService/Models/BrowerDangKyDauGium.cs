using System;
using System.Collections.Generic;

namespace WebAppService.Models.Updates;

public partial class BrowerDangKyDauGium
{
    public long IdDangKy { get; set; }

    public long? IdTaiKhoan { get; set; }

    public long? IdTaiSan { get; set; }

    public string? SoLenh { get; set; }

    public double? GiaDaTra { get; set; }

    public int? TrangThaiTra { get; set; }

    public DateTime? ThoiGianTra { get; set; }

    public string? MaTraGia { get; set; }

    public bool? IsDelete { get; set; }

    public bool? IsDuyet { get; set; }

    public bool? IsTrungDauGia { get; set; }

    public virtual BrowerTaiKhoanDangKy? IdTaiKhoanNavigation { get; set; }

    public virtual BrowerTaiSanDauGium? IdTaiSanNavigation { get; set; }
}
