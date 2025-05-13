using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Comment
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int SongId { get; set; }

    public TimeOnly Time { get; set; }

    public string Content { get; set; } = null!;

    public int? FatherCommentId { get; set; }

    public int NumLike { get; set; }

    public virtual Song Song { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
