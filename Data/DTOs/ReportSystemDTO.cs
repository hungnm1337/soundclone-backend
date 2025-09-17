using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class SystemReportDetailDTO
    {
        public int SystemReportId { get; set; }

        public int UserId { get; set; }

        public string Content { get; set; } = null!;

        public DateTime ReportDate { get; set; }

        public string? ReplyContent { get; set; }

        public DateTime? ReplyDate { get; set; }
    }

    public class SystemReportDTO
    {
        public int SystemReportId { get; set; }

        public int UserId { get; set; }

        public string Content { get; set; } = null!;

        public bool? isReplied {  get; set; }
    }

    public class ReplySystemReportDTO
    {
        public int SystemReportId { get; set; }

        public string ReplyContent { get; set; }
    }
}
