using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Song
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Source { get; set; } = null!;

    public DateTime Date { get; set; }

    public bool Public { get; set; }

    public string Picture { get; set; } = null!;

    public short Duration { get; set; }

    public int Author { get; set; }

    public int NumPlays { get; set; }

    public int NumLike { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}
