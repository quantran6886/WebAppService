using System;
using System.Collections.Generic;

namespace WebAppService.Models;

public partial class WebNguoiDungQuanTri
{
    public long IdQuanTri { get; set; }

    public string? HoTen { get; set; }

    public string? NgaySinhNhap { get; set; }

    public DateOnly? NgaySinhDate { get; set; }

    public string? LoaiGiayTo { get; set; }

    public string? SoHieuGiayTo { get; set; }

    public long? ThuocDonVi { get; set; }

    public string? GioiTinh { get; set; }

    public string? Email { get; set; }

    public string? SoDienThoai { get; set; }

    public string? MaSoThue { get; set; }

    public string? Fax { get; set; }

    public DateOnly? NgayTaoTaiKhoan { get; set; }

    public long? MaTinh { get; set; }

    public long? MaQuanHuyen { get; set; }

    public long? MaXaPhuong { get; set; }

    public bool? NhanThongBao { get; set; }

    public bool? IsLock { get; set; }

    public string? Id { get; set; }

    public virtual AspNetUser? IdNavigation { get; set; }
}
