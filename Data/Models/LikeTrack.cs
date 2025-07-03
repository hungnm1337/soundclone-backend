using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class LikeTrack
{
    public int LikeTrackId { get; set; }

    public int UserId { get; set; }

    public int TrackId { get; set; }

    public virtual Track Track { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
