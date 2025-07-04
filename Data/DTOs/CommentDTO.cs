using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class CommentDTO
    {
        public int CommentId { get; set; }

        public int WriteBy { get; set; }

        public DateTime WriteDate { get; set; }

        public int TrackId { get; set; }

        public int? ParentCommentId { get; set; }

        public string Content { get; set; } = null!;
    }
}
