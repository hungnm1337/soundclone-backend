using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateOnly DayOfBirth { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string? Bio { get; set; }

    public string? ProfilePictureUrl { get; set; }

    public DateTime CreateAt { get; set; }

    public DateTime UpdateAt { get; set; }

    public string Username { get; set; } = null!;

    public string HashedPassword { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Follow> FollowArtists { get; set; } = new List<Follow>();

    public virtual ICollection<Follow> FollowFollowers { get; set; } = new List<Follow>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<LikePlaylist> LikePlaylists { get; set; } = new List<LikePlaylist>();

    public virtual ICollection<LikeTrack> LikeTracks { get; set; } = new List<LikeTrack>();

    public virtual ICollection<Notification> NotificationActors { get; set; } = new List<Notification>();

    public virtual ICollection<Notification> NotificationRecipiens { get; set; } = new List<Notification>();

    public virtual ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<SystemReport> SystemReports { get; set; } = new List<SystemReport>();

    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
}
