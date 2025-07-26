using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAppService.Models;

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

    public virtual DbSet<BrowerHomePage> BrowerHomePages { get; set; }

    public virtual DbSet<BrowerTaiKhoanDangKy> BrowerTaiKhoanDangKies { get; set; }

    public virtual DbSet<BrowerVanBanTaiLieu> BrowerVanBanTaiLieus { get; set; }

    public virtual DbSet<ViewUserOnline> ViewUserOnlines { get; set; }

    public virtual DbSet<WebAbousU> WebAbousUs { get; set; }

    public virtual DbSet<WebCauHinhTrang> WebCauHinhTrangs { get; set; }

    public virtual DbSet<WebChucNangQuanTri> WebChucNangQuanTris { get; set; }

    public virtual DbSet<WebDanhMucHeThong> WebDanhMucHeThongs { get; set; }

    public virtual DbSet<WebDanhMucQuanHuyen> WebDanhMucQuanHuyens { get; set; }

    public virtual DbSet<WebDanhMucTinhThanh> WebDanhMucTinhThanhs { get; set; }

    public virtual DbSet<WebDanhMucXaPhuong> WebDanhMucXaPhuongs { get; set; }

    public virtual DbSet<WebDichVu> WebDichVus { get; set; }

    public virtual DbSet<WebFaq> WebFaqs { get; set; }

    public virtual DbSet<WebNguoiDungQuanTri> WebNguoiDungQuanTris { get; set; }

    public virtual DbSet<WebNhanSu> WebNhanSus { get; set; }

    public virtual DbSet<WebPhanQuyenQuanTri> WebPhanQuyenQuanTris { get; set; }

    public virtual DbSet<WebThongBao> WebThongBaos { get; set; }

    public virtual DbSet<WebTinTucBaiViet> WebTinTucBaiViets { get; set; }

    public virtual DbSet<WebUserOnline> WebUserOnlines { get; set; }

    public virtual DbSet<WebVideo> WebVideos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-VU39D0Q;Database=WebMedical;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.Property(e => e.RoleId).HasMaxLength(450);

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
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
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
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

        modelBuilder.Entity<WebAbousU>(entity =>
        {
            entity.HasKey(e => e.IdAbousUs);

            entity.Property(e => e.IdAbousUs).ValueGeneratedNever();
        });

        modelBuilder.Entity<WebCauHinhTrang>(entity =>
        {
            entity.HasKey(e => e.MaTrang).HasName("PK__WebCauHi__399828AFCE34751C");

            entity.ToTable("WebCauHinhTrang");

            entity.Property(e => e.MaTrang).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CbGiaoDien)
                .HasMaxLength(50)
                .HasColumnName("cbGiaoDien");
            entity.Property(e => e.IsCard1).HasDefaultValue(false);
            entity.Property(e => e.IsCard2).HasDefaultValue(false);
            entity.Property(e => e.ThoiGianCapNhap).HasColumnType("datetime");
            entity.Property(e => e.ThoiGianTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TieuDe).HasMaxLength(250);
            entity.Property(e => e.TxtCard1)
                .HasMaxLength(50)
                .HasColumnName("txtCard1");
            entity.Property(e => e.TxtCard21)
                .HasMaxLength(50)
                .HasColumnName("txtCard21");
            entity.Property(e => e.TxtCard22)
                .HasMaxLength(50)
                .HasColumnName("txtCard22");
            entity.Property(e => e.TxtCard31)
                .HasMaxLength(50)
                .HasColumnName("txtCard31");
            entity.Property(e => e.TxtIcon1)
                .HasMaxLength(50)
                .HasColumnName("txtIcon1");
            entity.Property(e => e.TxtIcon21)
                .HasMaxLength(50)
                .HasColumnName("txtIcon21");
            entity.Property(e => e.TxtIcon23)
                .HasMaxLength(50)
                .HasColumnName("txtIcon23");
            entity.Property(e => e.TxtIcon32)
                .HasMaxLength(50)
                .HasColumnName("txtIcon32");
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
            entity.HasKey(e => e.MaQuanHuyen).HasName("PK__Web.Danh__B86B827AE1718AC9");

            entity.ToTable("Web.DanhMucQuanHuyen");

            entity.Property(e => e.MaQuanHuyen).ValueGeneratedNever();
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.LoaiQuanHuyen).HasMaxLength(50);
            entity.Property(e => e.MaQuocGia).HasDefaultValue(84);
            entity.Property(e => e.TenQuanHuyen).HasMaxLength(100);

            entity.HasOne(d => d.MaTinhThanhNavigation).WithMany(p => p.WebDanhMucQuanHuyens)
                .HasForeignKey(d => d.MaTinhThanh)
                .HasConstraintName("FK__Web.DanhM__MaTin__10566F31");
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
            entity.HasKey(e => e.MaXaPhuong).HasName("PK__Web.Danh__92E67F278CE2FB40");

            entity.ToTable("Web.DanhMucXaPhuong");

            entity.Property(e => e.MaXaPhuong).ValueGeneratedNever();
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.LoaiXaPhuong).HasMaxLength(50);
            entity.Property(e => e.MaQuocGia).HasDefaultValue(84);
            entity.Property(e => e.TenXaPhuong).HasMaxLength(100);

            entity.HasOne(d => d.MaQuanHuyenNavigation).WithMany(p => p.WebDanhMucXaPhuongs)
                .HasForeignKey(d => d.MaQuanHuyen)
                .HasConstraintName("FK__Web.DanhM__MaQua__114A936A");
        });

        modelBuilder.Entity<WebDichVu>(entity =>
        {
            entity.HasKey(e => e.IdDichVu).HasName("PK__WebDichV__C817D5DCEC638C83");

            entity.ToTable("WebDichVu");

            entity.Property(e => e.IdDichVu).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CbLoaiBaiDang)
                .HasMaxLength(250)
                .HasColumnName("cbLoaiBaiDang");
            entity.Property(e => e.CbNhomBaiViet).HasMaxLength(250);
            entity.Property(e => e.MoTaNgan).HasMaxLength(500);
            entity.Property(e => e.NameImage).HasMaxLength(250);
            entity.Property(e => e.NguoiTao).HasMaxLength(250);
            entity.Property(e => e.ThoiGianCapNhap).HasColumnType("datetime");
            entity.Property(e => e.ThoiGianTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TieuDeBaiViet).HasMaxLength(500);
            entity.Property(e => e.TieuDeNgan).HasMaxLength(250);
            entity.Property(e => e.UrlImage).HasMaxLength(350);
        });

        modelBuilder.Entity<WebFaq>(entity =>
        {
            entity.HasKey(e => e.IdFaqs).HasName("PK__WebFAQS__1BFA1307410C0D53");

            entity.ToTable("WebFAQS");

            entity.Property(e => e.IdFaqs).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CauHoi).HasMaxLength(500);
            entity.Property(e => e.GhiChu).HasMaxLength(250);
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

        modelBuilder.Entity<WebNhanSu>(entity =>
        {
            entity.HasKey(e => e.IdNhanSu).HasName("PK__WebNhanS__876A20DBB2645F67");

            entity.ToTable("WebNhanSu");

            entity.Property(e => e.IdNhanSu).HasDefaultValueSql("(newid())");
            entity.Property(e => e.BangCapHocVi).HasMaxLength(250);
            entity.Property(e => e.CbGioiTinh)
                .HasMaxLength(50)
                .HasColumnName("cbGioiTinh");
            entity.Property(e => e.ChucDanh).HasMaxLength(150);
            entity.Property(e => e.ChucVu).HasMaxLength(150);
            entity.Property(e => e.DonViKhoa).HasMaxLength(250);
            entity.Property(e => e.HoTen).HasMaxLength(250);
            entity.Property(e => e.NameImage).HasMaxLength(250);
            entity.Property(e => e.NgaySinh).HasColumnType("datetime");
            entity.Property(e => e.NgonNgu).HasMaxLength(250);
            entity.Property(e => e.ThoiGianCapNhap).HasColumnType("datetime");
            entity.Property(e => e.ThoiGianTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UrlImage).HasMaxLength(350);
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

        modelBuilder.Entity<WebThongBao>(entity =>
        {
            entity.HasKey(e => e.IdNoti).HasName("PK__WebThong__4B2ACFFA7B57E9D4");

            entity.ToTable("WebThongBao");

            entity.Property(e => e.IdNoti).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CbLoaiTin)
                .HasMaxLength(100)
                .HasColumnName("cbLoaiTin");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FileDinhKem).HasMaxLength(250);
            entity.Property(e => e.GioiTinh).HasMaxLength(150);
            entity.Property(e => e.HoTenNguoiGui).HasMaxLength(150);
            entity.Property(e => e.IdNguoiGui)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IdNguoiNhan)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsDaDoc).HasDefaultValue(false);
            entity.Property(e => e.LoiNhan).HasMaxLength(150);
            entity.Property(e => e.NoiDung).HasMaxLength(250);
            entity.Property(e => e.SoDienThoai).HasMaxLength(150);
            entity.Property(e => e.ThoiGianCapNhap).HasColumnType("datetime");
            entity.Property(e => e.ThoiGianTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TieuDe).HasMaxLength(150);
        });

        modelBuilder.Entity<WebTinTucBaiViet>(entity =>
        {
            entity.HasKey(e => e.IdBaiViet).HasName("PK__WebTinTu__42161C7A6B926181");

            entity.ToTable("WebTinTucBaiViet");

            entity.Property(e => e.IdBaiViet).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CbLoaiBaiDang)
                .HasMaxLength(250)
                .HasColumnName("cbLoaiBaiDang");
            entity.Property(e => e.CbNhomBaiViet).HasMaxLength(250);
            entity.Property(e => e.IsBaiVietNoiBat).HasDefaultValue(false);
            entity.Property(e => e.IsCongKhai).HasDefaultValue(true);
            entity.Property(e => e.MoTaNgan).HasMaxLength(500);
            entity.Property(e => e.NameImage).HasMaxLength(250);
            entity.Property(e => e.NguoiTao).HasMaxLength(250);
            entity.Property(e => e.ThoiGianCapNhap).HasColumnType("datetime");
            entity.Property(e => e.ThoiGianTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TieuDeBaiViet).HasMaxLength(500);
            entity.Property(e => e.TieuDeNgan).HasMaxLength(250);
            entity.Property(e => e.UrlImage).HasMaxLength(350);
        });

        modelBuilder.Entity<WebUserOnline>(entity =>
        {
            entity.ToTable("Web.UserOnline");

            entity.Property(e => e.ComputerName).HasMaxLength(100);
            entity.Property(e => e.IpAddress).HasMaxLength(50);
            entity.Property(e => e.LastActive).HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(100);
        });

        modelBuilder.Entity<WebVideo>(entity =>
        {
            entity.HasKey(e => e.IdVideo).HasName("PK__WebVideo__54BA87FA9D8E57F2");

            entity.ToTable("WebVideo");

            entity.Property(e => e.IdVideo).HasDefaultValueSql("(newid())");
            entity.Property(e => e.MoTaNgan).HasMaxLength(500);
            entity.Property(e => e.NameImage).HasMaxLength(250);
            entity.Property(e => e.NameVideo).HasMaxLength(250);
            entity.Property(e => e.NguoiTao).HasMaxLength(250);
            entity.Property(e => e.ThoiGianCapNhap).HasColumnType("datetime");
            entity.Property(e => e.ThoiGianTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TieuDeBaiViet).HasMaxLength(500);
            entity.Property(e => e.TieuDeNgan).HasMaxLength(250);
            entity.Property(e => e.UrlImage).HasMaxLength(350);
            entity.Property(e => e.UrlVideo).HasMaxLength(350);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
