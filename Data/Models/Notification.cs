using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public bool IsRead { get; set; }

    public DateTime CreateAt { get; set; }

    public int RecipienId { get; set; }

    public int? ActorId { get; set; }

    public virtual User? Actor { get; set; }

    public virtual User Recipien { get; set; } = null!;
}
