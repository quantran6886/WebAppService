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
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-F36LLIM;Initial Catalog=WebApp;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

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
            entity.Property(e => e.UserName).HasMaxLength(100);
            entity.Property(e => e.IpAddress).HasMaxLength(50);
            entity.Property(e => e.LastActive).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
