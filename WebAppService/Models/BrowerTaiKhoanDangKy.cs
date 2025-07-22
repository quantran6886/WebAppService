using System;
using System.Collections.Generic;

namespace WebAppService.Models.Updates;

public partial class BrowerTaiKhoanDangKy
{
    public long IdTaiKhoan { get; set; }

    public string? Username { get; set; }

    public string? PasswordHash { get; set; }

    public int? LoaiTaiKhoan { get; set; }

    public string? TenToChuc { get; set; }

    public string? ChucVu { get; set; }

    public string? MaSoThue { get; set; }

    public DateOnly? NgayCapThue { get; set; }

    public string? NoiCapMst { get; set; }

    public string? DiaChiDoanhNghiep { get; set; }

    public string? UrlTaiLieu { get; set; }

    public string? NameTaiLieu { get; set; }

    public string? Ho { get; set; }

    public string? TenDem { get; set; }

    public string? Ten { get; set; }

    public string? Email { get; set; }

    public bool? EmailConfirm { get; set; }

    public string? SoDienThoai { get; set; }

    public string? LoaiGioiTinh { get; set; }

    public int? NgaySinh { get; set; }

    public int? ThangSinh { get; set; }

    public int? NamSinh { get; set; }

    public long? TinhThanh { get; set; }

    public long? QuanHuyen { get; set; }

    public long? XaPhuong { get; set; }

    public string? DiaChiChiTiet { get; set; }

    public string? SoHieuGiayTo { get; set; }

    public DateOnly? NgayCap { get; set; }

    public string? NoiCap { get; set; }

    public string? UrlShMatTruoc { get; set; }

    public string? UrlShMatSau { get; set; }

    public string? NameShMatTruoc { get; set; }

    public string? NameShMatSau { get; set; }

    public string? SoTaiKhoan { get; set; }

    public string? LoaiNganHang { get; set; }

    public string? ChiNhanNganHang { get; set; }

    public string? TenChuTaiKhoan { get; set; }

    public bool? IsKhoaTaiKhoan { get; set; }

    public int? IsNhapSaiMk { get; set; }

    public bool? IsDongPhiThuongNien { get; set; }
}
