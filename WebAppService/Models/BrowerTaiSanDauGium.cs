using System;
using System.Collections.Generic;

namespace WebAppService.Models.Updates;

public partial class BrowerTaiSanDauGium
{
    public long IdTaiSan { get; set; }

    public string? MaTaiSan { get; set; }

    public string? TenTaiSan { get; set; }

    public DateTime? ThoiGianMo { get; set; }

    public DateTime? ThoiGianDong { get; set; }

    public double? PhiDangKyThamGia { get; set; }

    public int? BuocGia { get; set; }

    public double? GiaKhoiDiem { get; set; }

    public string? DonViGia { get; set; }

    public string? HinhThucDauGia { get; set; }

    public string? PhuongThucDauGia { get; set; }

    public string? NguoiCoTaiSan { get; set; }

    public string? NoiXemTaiSan { get; set; }

    public string? TextThoiGianXemTaiSan { get; set; }

    public DateTime? ThoiGianBatDauTraGia { get; set; }

    public DateTime? ThoiGianKetThucTraGia { get; set; }

    public string? MoTa { get; set; }

    public string? ToChucDauGia { get; set; }

    public string? DauGiaVien { get; set; }

    public string? DiaChi { get; set; }

    public int? TrangThai { get; set; }

    public string? Tag { get; set; }

    public bool? IsCongKhai { get; set; }

    public bool? IsDuyetBaiViet { get; set; }

    public virtual ICollection<BrowerAnhTaiLieuDauGium> BrowerAnhTaiLieuDauGia { get; set; } = new List<BrowerAnhTaiLieuDauGium>();

    public virtual ICollection<BrowerDangKyDauGium> BrowerDangKyDauGia { get; set; } = new List<BrowerDangKyDauGium>();
}
