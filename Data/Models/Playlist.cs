using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Playlist
{
    public int Id { get; set; }

    public int AuthorId { get; set; }

    public int PlaylistId { get; set; }

    public string Picture { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int SongId { get; set; }

    public bool Public { get; set; }

    public virtual User Author { get; set; } = null!;

    public virtual Song Song { get; set; } = null!;

    public virtual ICollection<UserPlaylist> UserPlaylists { get; set; } = new List<UserPlaylist>();
}
