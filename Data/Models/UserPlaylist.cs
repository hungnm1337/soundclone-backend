using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class UserPlaylist
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int PlaylistId { get; set; }

    public virtual Playlist Playlist { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
