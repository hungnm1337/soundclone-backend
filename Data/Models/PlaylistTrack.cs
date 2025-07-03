using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class PlaylistTrack
{
    public int Id { get; set; }

    public int PlaylistId { get; set; }

    public int TrackId { get; set; }

    public virtual Playlist Playlist { get; set; } = null!;

    public virtual Track Track { get; set; } = null!;
}
