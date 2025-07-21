using System;
using System.Collections.Generic;

namespace WebAppService.Models.Updates;

public partial class WebDanhMucTinhThanh
{
    public long MaTinhThanh { get; set; }

    public string? TenTinhThanh { get; set; }

    public string? LoaiTinhThanh { get; set; }

    public bool? IsDeleted { get; set; }

    public int? MaQuocGia { get; set; }

    public virtual ICollection<WebDanhMucQuanHuyen> WebDanhMucQuanHuyens { get; set; } = new List<WebDanhMucQuanHuyen>();
}
