using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Track
{
    public int TrackId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string AudioFileUrl { get; set; } = null!;

    public string CoverArtUrl { get; set; } = null!;

    public string WaveformUrl { get; set; } = null!;

    public int DurationInSeconds { get; set; }

    public bool IsPublic { get; set; }

    public DateTime UploadDate { get; set; }

    public int UpdateBy { get; set; }

    public int PlayCount { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<LikeTrack> LikeTracks { get; set; } = new List<LikeTrack>();

    public virtual ICollection<PlaylistTrack> PlaylistTracks { get; set; } = new List<PlaylistTrack>();
}
