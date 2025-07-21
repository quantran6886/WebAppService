using System;
using System.Collections.Generic;

namespace WebAppService.Models.Updates;

public partial class WebPhanQuyenQuanTri
{
    public long IdPhanQuyen { get; set; }

    public long? IdChucNang { get; set; }

    public string? Id { get; set; }

    public bool? IsLockCreate { get; set; }

    public bool? IsLockRemove { get; set; }

    public bool? IsLockDetail { get; set; }

    public virtual WebChucNangQuanTri? IdChucNangNavigation { get; set; }

    public virtual AspNetUser? IdNavigation { get; set; }
}
