using System;
using System.Collections.Generic;

namespace WebAppService.Models;

public partial class WebDanhMucQuanHuyen
{
    public long MaQuanHuyen { get; set; }

    public string? TenQuanHuyen { get; set; }

    public long? MaTinhThanh { get; set; }

    public string? LoaiQuanHuyen { get; set; }

    public bool? IsDeleted { get; set; }

    public int? MaQuocGia { get; set; }

    public virtual WebDanhMucTinhThanh? MaTinhThanhNavigation { get; set; }

    public virtual ICollection<WebDanhMucXaPhuong> WebDanhMucXaPhuongs { get; set; } = new List<WebDanhMucXaPhuong>();
}
