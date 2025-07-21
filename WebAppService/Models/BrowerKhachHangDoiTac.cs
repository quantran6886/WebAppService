using System;
using System.Collections.Generic;

namespace WebAppService.Models.Updates;

public partial class BrowerKhachHangDoiTac
{
    public long IdKhachHang { get; set; }

    public string? UrlFile { get; set; }

    public string? NameFile { get; set; }

    public string? MoTa { get; set; }

    public bool? IsDangHopTac { get; set; }
}
