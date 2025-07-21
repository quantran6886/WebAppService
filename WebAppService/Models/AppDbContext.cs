using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAppService.Models.Updates;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<BrowerAnhTaiLieuDauGium> BrowerAnhTaiLieuDauGia { get; set; }

    public virtual DbSet<BrowerDangKyDauGium> BrowerDangKyDauGia { get; set; }

    public virtual DbSet<BrowerHomePage> BrowerHomePages { get; set; }

    public virtual DbSet<BrowerKhachHangDoiTac> BrowerKhachHangDoiTacs { get; set; }

    public virtual DbSet<BrowerTaiKhoanDangKy> BrowerTaiKhoanDangKies { get; set; }

    public virtual DbSet<BrowerTaiSanDauGium> BrowerTaiSanDauGia { get; set; }

    public virtual DbSet<BrowerTaiSanDinhKem> BrowerTaiSanDinhKems { get; set; }

    public virtual DbSet<BrowerVanBanTaiLieu> BrowerVanBanTaiLieus { get; set; }

    public virtual DbSet<ViewUserOnline> ViewUserOnlines { get; set; }

    public virtual DbSet<WebChucNangQuanTri> WebChucNangQuanTris { get; set; }

    public virtual DbSet<WebDanhMucHeThong> WebDanhMucHeThongs { get; set; }

    public virtual DbSet<WebDanhMucQuanHuyen> WebDanhMucQuanHuyens { get; set; }

    public virtual DbSet<WebDanhMucTinhThanh> WebDanhMucTinhThanhs { get; set; }

    public virtual DbSet<WebDanhMucXaPhuong> WebDanhMucXaPhuongs { get; set; }

    public virtual DbSet<WebNguoiDungQuanTri> WebNguoiDungQuanTris { get; set; }

    public virtual DbSet<WebPhanQuyenQuanTri> WebPhanQuyenQuanTris { get; set; }

    public virtual DbSet<WebUserOnline> WebUserOnlines { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-VU39D0Q;Initial Catalog=WebApp;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<BrowerAnhTaiLieuDauGium>(entity =>
        {
            entity.HasKey(e => e.IdFile);

            entity.ToTable("Brower.AnhTaiLieuDauGia");

            entity.Property(e => e.IdFile).ValueGeneratedNever();
            entity.Property(e => e.NameFile)
                .HasMaxLength(250)
                .HasColumnName("nameFile");
            entity.Property(e => e.UrlFile).HasColumnName("urlFile");

            entity.HasOne(d => d.IdTaiSanNavigation).WithMany(p => p.BrowerAnhTaiLieuDauGia)
                .HasForeignKey(d => d.IdTaiSan)
                .HasConstraintName("FK_Brower.AnhTaiLieuDauGia_Brower.TaiSanDauGia");
        });

        modelBuilder.Entity<BrowerDangKyDauGium>(entity =>
        {
            entity.HasKey(e => e.IdDangKy);

            entity.ToTable("Brower.DangKyDauGia");

            entity.Property(e => e.MaTraGia).HasMaxLength(250);
            entity.Property(e => e.SoLenh).HasMaxLength(60);
            entity.Property(e => e.ThoiGianTra).HasColumnType("datetime");

            entity.HasOne(d => d.IdTaiKhoanNavigation).WithMany(p => p.BrowerDangKyDauGia)
                .HasForeignKey(d => d.IdTaiKhoan)
                .HasConstraintName("FK_Brower.DangKyDauGia_Brower.TaiKhoanDangKy");

            entity.HasOne(d => d.IdTaiSanNavigation).WithMany(p => p.BrowerDangKyDauGia)
                .HasForeignKey(d => d.IdTaiSan)
                .HasConstraintName("FK_Brower.DangKyDauGia_Brower.TaiSanDauGia");
        });

        modelBuilder.Entity<BrowerHomePage>(entity =>
        {
            entity.ToTable("Brower.HomePage");

            entity.Property(e => e.Link).HasColumnType("text");
            entity.Property(e => e.MoTaHienThi).HasColumnType("text");
            entity.Property(e => e.PhanLoai).HasMaxLength(250);
            entity.Property(e => e.Tag).HasMaxLength(250);
            entity.Property(e => e.TenTieuDe).HasMaxLength(250);
        });

        modelBuilder.Entity<BrowerKhachHangDoiTac>(entity =>
        {
            entity.HasKey(e => e.IdKhachHang);

            entity.ToTable("Brower.KhachHangDoiTac");

            entity.Property(e => e.MoTa).HasColumnType("text");
            entity.Property(e => e.NameFile)
                .HasMaxLength(250)
                .HasColumnName("nameFile");
            entity.Property(e => e.UrlFile).HasColumnName("urlFile");
        });

        modelBuilder.Entity<BrowerTaiKhoanDangKy>(entity =>
        {
            entity.HasKey(e => e.IdTaiKhoan);

            entity.ToTable("Brower.TaiKhoanDangKy");

            entity.Property(e => e.ChiNhanNganHang).HasMaxLength(250);
            entity.Property(e => e.ChucVu).HasMaxLength(150);
            entity.Property(e => e.DiaChiChiTiet).HasMaxLength(250);
            entity.Property(e => e.DiaChiDoanhNghiep).HasMaxLength(250);
            entity.Property(e => e.Email).HasMaxLength(250);
            entity.Property(e => e.Ho).HasMaxLength(150);
            entity.Property(e => e.IsNhapSaiMk).HasColumnName("IsNhapSaiMK");
            entity.Property(e => e.LoaiGioiTinh).HasMaxLength(150);
            entity.Property(e => e.LoaiNganHang).HasMaxLength(250);
            entity.Property(e => e.MaSoThue).HasMaxLength(150);
            entity.Property(e => e.NameShMatSau)
                .HasMaxLength(250)
                .HasColumnName("nameShMatSau");
            entity.Property(e => e.NameShMatTruoc)
                .HasMaxLength(250)
                .HasColumnName("nameShMatTruoc");
            entity.Property(e => e.NameTaiLieu)
                .HasMaxLength(250)
                .HasColumnName("nameTaiLieu");
            entity.Property(e => e.NoiCap).HasMaxLength(250);
            entity.Property(e => e.NoiCapMst)
                .HasMaxLength(250)
                .HasColumnName("NoiCapMST");
            entity.Property(e => e.SoDienThoai).HasMaxLength(150);
            entity.Property(e => e.SoHieuGiayTo).HasMaxLength(250);
            entity.Property(e => e.SoTaiKhoan).HasMaxLength(250);
            entity.Property(e => e.Ten).HasMaxLength(150);
            entity.Property(e => e.TenChuTaiKhoan).HasMaxLength(250);
            entity.Property(e => e.TenDem).HasMaxLength(150);
            entity.Property(e => e.TenToChuc).HasMaxLength(150);
            entity.Property(e => e.UrlShMatSau).HasColumnName("urlShMatSau");
            entity.Property(e => e.UrlShMatTruoc).HasColumnName("urlShMatTruoc");
            entity.Property(e => e.UrlTaiLieu).HasColumnName("urlTaiLieu");
            entity.Property(e => e.Username).HasMaxLength(150);
        });

        modelBuilder.Entity<BrowerTaiSanDauGium>(entity =>
        {
            entity.HasKey(e => e.IdTaiSan);

            entity.ToTable("Brower.TaiSanDauGia");

            entity.Property(e => e.DauGiaVien).HasMaxLength(250);
            entity.Property(e => e.DiaChi).HasMaxLength(250);
            entity.Property(e => e.DonViGia).HasMaxLength(150);
            entity.Property(e => e.HinhThucDauGia).HasMaxLength(250);
            entity.Property(e => e.MaTaiSan)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.NguoiCoTaiSan).HasMaxLength(250);
            entity.Property(e => e.NoiXemTaiSan).HasMaxLength(250);
            entity.Property(e => e.PhuongThucDauGia).HasMaxLength(150);
            entity.Property(e => e.Tag).HasMaxLength(150);
            entity.Property(e => e.TenTaiSan).HasMaxLength(250);
            entity.Property(e => e.TextThoiGianXemTaiSan).HasMaxLength(250);
            entity.Property(e => e.ThoiGianBatDauTraGia).HasColumnType("datetime");
            entity.Property(e => e.ThoiGianDong).HasColumnType("datetime");
            entity.Property(e => e.ThoiGianKetThucTraGia).HasColumnType("datetime");
            entity.Property(e => e.ThoiGianMo).HasColumnType("datetime");
            entity.Property(e => e.ToChucDauGia).HasMaxLength(250);
        });

        modelBuilder.Entity<BrowerTaiSanDinhKem>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Brower.TaiSanDinhKem");

            entity.HasOne(d => d.IdNavigation).WithMany()
                .HasForeignKey(d => d.Id)
                .HasConstraintName("FK_Brower.TaiSanDinhKem_Brower.HomePage");

            entity.HasOne(d => d.IdTaiSanNavigation).WithMany()
                .HasForeignKey(d => d.IdTaiSan)
                .HasConstraintName("FK_Brower.TaiSanDinhKem_Brower.TaiSanDauGia");
        });

        modelBuilder.Entity<BrowerVanBanTaiLieu>(entity =>
        {
            entity.HasKey(e => e.IdVanBan);

            entity.ToTable("Brower.VanBanTaiLieu");

            entity.Property(e => e.DuoiFile).HasMaxLength(50);
            entity.Property(e => e.NameFile).HasMaxLength(250);
            entity.Property(e => e.TenVanBan).HasMaxLength(250);
        });

        modelBuilder.Entity<ViewUserOnline>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_UserOnline");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Fax).HasMaxLength(50);
            entity.Property(e => e.GioiTinh).HasMaxLength(50);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.Id).HasMaxLength(450);
            entity.Property(e => e.MaSoThue).HasMaxLength(100);
            entity.Property(e => e.SoDienThoai).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(256);
        });

        modelBuilder.Entity<WebChucNangQuanTri>(entity =>
        {
            entity.HasKey(e => e.IdChucNang);

            entity.ToTable("Web.ChucNangQuanTri");

            entity.Property(e => e.Action).HasMaxLength(100);
            entity.Property(e => e.Controller).HasMaxLength(100);
            entity.Property(e => e.TenMenu).HasMaxLength(250);
            entity.Property(e => e.UrlDuongDan)
                .HasMaxLength(250)
                .HasColumnName("urlDuongDan");
        });

        modelBuilder.Entity<WebDanhMucHeThong>(entity =>
        {
            entity.HasKey(e => e.IdHeThong);

            entity.ToTable("Web.DanhMucHeThong");

            entity.Property(e => e.GhiChu).HasMaxLength(250);
            entity.Property(e => e.LoaiDanhMuc).HasMaxLength(150);
            entity.Property(e => e.MauSac).HasMaxLength(250);
            entity.Property(e => e.TenGoi).HasMaxLength(250);
            entity.Property(e => e.ThuTuLdm).HasColumnName("ThuTuLDM");
            entity.Property(e => e.ThuTuTg).HasColumnName("ThuTuTG");
        });

        modelBuilder.Entity<WebDanhMucQuanHuyen>(entity =>
        {
            entity.HasKey(e => e.MaQuanHuyen).HasName("PK__Web.Danh__B86B827ABAB18191");

            entity.ToTable("Web.DanhMucQuanHuyen");

            entity.Property(e => e.MaQuanHuyen).ValueGeneratedNever();
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.LoaiQuanHuyen).HasMaxLength(50);
            entity.Property(e => e.MaQuocGia).HasDefaultValue(84);
            entity.Property(e => e.TenQuanHuyen).HasMaxLength(100);

            entity.HasOne(d => d.MaTinhThanhNavigation).WithMany(p => p.WebDanhMucQuanHuyens)
                .HasForeignKey(d => d.MaTinhThanh)
                .HasConstraintName("FK__Web.DanhM__MaTin__7D439ABD");
        });

        modelBuilder.Entity<WebDanhMucTinhThanh>(entity =>
        {
            entity.HasKey(e => e.MaTinhThanh);

            entity.ToTable("Web.DanhMucTinhThanh");

            entity.Property(e => e.MaTinhThanh).ValueGeneratedNever();
            entity.Property(e => e.LoaiTinhThanh).HasMaxLength(50);
            entity.Property(e => e.TenTinhThanh).HasMaxLength(100);
        });

        modelBuilder.Entity<WebDanhMucXaPhuong>(entity =>
        {
            entity.HasKey(e => e.MaXaPhuong).HasName("PK__Web.Danh__92E67F27D480320C");

            entity.ToTable("Web.DanhMucXaPhuong");

            entity.Property(e => e.MaXaPhuong).ValueGeneratedNever();
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.LoaiXaPhuong).HasMaxLength(50);
            entity.Property(e => e.MaQuocGia).HasDefaultValue(84);
            entity.Property(e => e.TenXaPhuong).HasMaxLength(100);

            entity.HasOne(d => d.MaQuanHuyenNavigation).WithMany(p => p.WebDanhMucXaPhuongs)
                .HasForeignKey(d => d.MaQuanHuyen)
                .HasConstraintName("FK__Web.DanhM__MaQua__02084FDA");
        });

        modelBuilder.Entity<WebNguoiDungQuanTri>(entity =>
        {
            entity.HasKey(e => e.IdQuanTri);

            entity.ToTable("Web.NguoiDungQuanTri");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Fax).HasMaxLength(50);
            entity.Property(e => e.GioiTinh).HasMaxLength(50);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.Id).HasMaxLength(450);
            entity.Property(e => e.LoaiGiayTo).HasMaxLength(150);
            entity.Property(e => e.MaSoThue).HasMaxLength(100);
            entity.Property(e => e.NgaySinhNhap).HasMaxLength(100);
            entity.Property(e => e.SoDienThoai).HasMaxLength(100);
            entity.Property(e => e.SoHieuGiayTo).HasMaxLength(150);

            entity.HasOne(d => d.IdNavigation).WithMany(p => p.WebNguoiDungQuanTris)
                .HasForeignKey(d => d.Id)
                .HasConstraintName("FK_Web.NguoiDungQuanTri_AspNetUsers");
        });

        modelBuilder.Entity<WebPhanQuyenQuanTri>(entity =>
        {
            entity.HasKey(e => e.IdPhanQuyen);

            entity.ToTable("Web.PhanQuyenQuanTri");

            entity.Property(e => e.Id).HasMaxLength(450);

            entity.HasOne(d => d.IdNavigation).WithMany(p => p.WebPhanQuyenQuanTris)
                .HasForeignKey(d => d.Id)
                .HasConstraintName("FK_Web.PhanQuyenQuanTri_AspNetUsers");

            entity.HasOne(d => d.IdChucNangNavigation).WithMany(p => p.WebPhanQuyenQuanTris)
                .HasForeignKey(d => d.IdChucNang)
                .HasConstraintName("FK_Web.PhanQuyenQuanTri_Web.ChucNangQuanTri");
        });

        modelBuilder.Entity<WebUserOnline>(entity =>
        {
            entity.ToTable("Web.UserOnline");

            entity.Property(e => e.ComputerName).HasMaxLength(100);
            entity.Property(e => e.IpAddress).HasMaxLength(50);
            entity.Property(e => e.LastActive).HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
