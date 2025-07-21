using System;
using System.Collections.Generic;

namespace WebAppService.Models.Updates;

public partial class BrowerTaiSanDinhKem
{
    public long? IdDinhKem { get; set; }

    public long? IdTaiSan { get; set; }

    public long? Id { get; set; }

    public long? IsHienThi { get; set; }

    public int? SoThuTu { get; set; }

    public virtual BrowerHomePage? IdNavigation { get; set; }

    public virtual BrowerTaiSanDauGium? IdTaiSanNavigation { get; set; }
}
