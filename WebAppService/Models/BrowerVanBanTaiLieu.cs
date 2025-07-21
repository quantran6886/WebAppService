using System;
using System.Collections.Generic;

namespace WebAppService.Models.Updates;

public partial class BrowerVanBanTaiLieu
{
    public long IdVanBan { get; set; }

    public string? TenVanBan { get; set; }

    public string? UrlFile { get; set; }

    public string? NameFile { get; set; }

    public string? DuoiFile { get; set; }

    public bool? IsGui { get; set; }

    public bool? IsDelete { get; set; }
}
