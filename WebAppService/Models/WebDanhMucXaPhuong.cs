using System;
using System.Collections.Generic;

namespace WebAppService.Models;

public partial class WebDanhMucXaPhuong
{
    public long MaXaPhuong { get; set; }

    public string? TenXaPhuong { get; set; }

    public long? MaQuanHuyen { get; set; }

    public string? LoaiXaPhuong { get; set; }

    public bool? IsDeleted { get; set; }

    public int? MaQuocGia { get; set; }

    public virtual WebDanhMucQuanHuyen? MaQuanHuyenNavigation { get; set; }
}
