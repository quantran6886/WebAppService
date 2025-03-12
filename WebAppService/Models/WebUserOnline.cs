using System;
using System.Collections.Generic;

namespace WebAppService.Models;

public partial class WebUserOnline
{
    public string Id { get; set; } = null!;

    public DateTime? LastActive { get; set; }

    public string? IpAddress { get; set; }

    public string? ComputerName { get; set; }

    public string? UserName { get; set; }

    public bool? IsOnline { get; set; }
}
