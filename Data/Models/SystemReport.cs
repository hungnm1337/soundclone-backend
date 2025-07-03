using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class SystemReport
{
    public int SystemReportId { get; set; }

    public int UserId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime ReportDate { get; set; }

    public string? ReplyContent { get; set; }

    public DateTime? ReplyDate { get; set; }

    public virtual User User { get; set; } = null!;
}
