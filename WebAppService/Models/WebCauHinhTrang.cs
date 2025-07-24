using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppService.Models.Updates;

public partial class WebCauHinhTrang
{
    public Guid MaTrang { get; set; }

    public string? CbGiaoDien { get; set; }

    public string? TieuDe { get; set; }
    public string? NoiDung { get; set; }

    public bool? IsCard1 { get; set; }

    public string? TxtCard1 { get; set; }

    public string? TxtIcon1 { get; set; }

    public bool? IsCard2 { get; set; }

    public string? TxtCard21 { get; set; }

    public string? TxtIcon21 { get; set; }

    public string? TxtCard22 { get; set; }

    public string? TxtIcon23 { get; set; }

    public string? TxtCard31 { get; set; }

    public string? TxtIcon32 { get; set; }

    public int? SapXep { get; set; }

    public bool? IsCongKhai { get; set; }

    public DateTime? ThoiGianTao { get; set; }

    public DateTime? ThoiGianCapNhap { get; set; }
}
