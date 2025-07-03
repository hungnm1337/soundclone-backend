using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public int WriteBy { get; set; }

    public DateTime WriteDate { get; set; }

    public int TrackId { get; set; }

    public int? ParentCommentId { get; set; }

    public string Content { get; set; } = null!;

    public virtual ICollection<Comment> InverseParentComment { get; set; } = new List<Comment>();

    public virtual Comment? ParentComment { get; set; }

    public virtual Track Track { get; set; } = null!;

    public virtual User WriteByNavigation { get; set; } = null!;
}
