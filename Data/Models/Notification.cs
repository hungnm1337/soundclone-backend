using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Notification
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public int UserId { get; set; }

    public bool IsRead { get; set; }

    public DateOnly CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
