using System;
using System.Collections.Generic;

namespace WebAppService.Models;

public partial class WebAbousU
{
    public Guid IdAbousUs { get; set; }

    public string? NoiDung { get; set; }

    public bool? IsCongKhai { get; set; }
}
