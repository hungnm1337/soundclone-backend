using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Playlist
{
    public int PlaylistId { get; set; }

    public string Title { get; set; } = null!;

    public int MakeBy { get; set; }

    public DateTime MakeDate { get; set; }

    public string? PicturePlaylistUrl { get; set; }

    public virtual ICollection<LikePlaylist> LikePlaylists { get; set; } = new List<LikePlaylist>();

    public virtual User MakeByNavigation { get; set; } = null!;

    public virtual ICollection<PlaylistTrack> PlaylistTracks { get; set; } = new List<PlaylistTrack>();
}
