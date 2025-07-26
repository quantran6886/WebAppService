using System;
using System.Collections.Generic;

namespace WebAppService.Models;

public partial class WebFaq
{
    public Guid IdFaqs { get; set; }

    public int? SoThuTu { get; set; }

    public string? CauHoi { get; set; }

    public string? CauTraLoi { get; set; }

    public string? GhiChu { get; set; }
}
