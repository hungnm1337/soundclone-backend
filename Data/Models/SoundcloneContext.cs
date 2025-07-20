using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data.Models;

public partial class SoundcloneContext : DbContext
{
    public SoundcloneContext()
    {
    }

    public SoundcloneContext(DbContextOptions<SoundcloneContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Follow> Follows { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<LikePlaylist> LikePlaylists { get; set; }

    public virtual DbSet<LikeTrack> LikeTracks { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Playlist> Playlists { get; set; }

    public virtual DbSet<PlaylistTrack> PlaylistTracks { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<SystemReport> SystemReports { get; set; }

    public virtual DbSet<Track> Tracks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public async Task<User> FindAsync(int userId)
    {
        throw new NotImplementedException();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        var configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comments__E7957687EA239FA6");

            entity.HasIndex(e => e.ParentCommentId, "IX_Comments_parent_comment_id");

            entity.HasIndex(e => e.TrackId, "IX_Comments_track_id");

            entity.HasIndex(e => e.WriteBy, "IX_Comments_write_by");

            entity.Property(e => e.CommentId).HasColumnName("comment_id");
            entity.Property(e => e.Content)
                .HasMaxLength(225)
                .HasColumnName("content");
            entity.Property(e => e.ParentCommentId).HasColumnName("parent_comment_id");
            entity.Property(e => e.TrackId).HasColumnName("track_id");
            entity.Property(e => e.WriteBy).HasColumnName("write_by");
            entity.Property(e => e.WriteDate)
                .HasColumnType("datetime")
                .HasColumnName("write_date");

            entity.HasOne(d => d.ParentComment).WithMany(p => p.InverseParentComment)
                .HasForeignKey(d => d.ParentCommentId)
                .HasConstraintName("FK_Comments_Parent");

            entity.HasOne(d => d.Track).WithMany(p => p.Comments)
                .HasForeignKey(d => d.TrackId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Tracks");

            entity.HasOne(d => d.WriteByNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.WriteBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Users");
        });

        modelBuilder.Entity<Follow>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Follows__3213E83F7F2DA566");

            entity.HasIndex(e => e.ArtistId, "IX_Follows_artist_id");

            entity.HasIndex(e => e.FollowerId, "IX_Follows_follower_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ArtistId).HasColumnName("artist_id");
            entity.Property(e => e.FollowerId).HasColumnName("follower_id");

            entity.HasOne(d => d.Artist).WithMany(p => p.FollowArtists)
                .HasForeignKey(d => d.ArtistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Follows_Artist");

            entity.HasOne(d => d.Follower).WithMany(p => p.FollowFollowers)
                .HasForeignKey(d => d.FollowerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Follows_Follower");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoices__F58DFD4907B2EFDD");

            entity.HasIndex(e => e.ServiceId, "IX_Invoices_service_id");

            entity.HasIndex(e => e.UserId, "IX_Invoices_user_id");

            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("total_amount");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Service).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoices_Services");

            entity.HasOne(d => d.User).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoices_Users");
        });

        modelBuilder.Entity<LikePlaylist>(entity =>
        {
            entity.HasKey(e => e.LikePlaylistId).HasName("PK__LikePlay__23AB7E9BC00BE761");

            entity.HasIndex(e => e.PlaylistId, "IX_LikePlaylists_playlist_id");

            entity.HasIndex(e => e.UserId, "IX_LikePlaylists_user_id");

            entity.Property(e => e.LikePlaylistId).HasColumnName("like_playlist_id");
            entity.Property(e => e.PlaylistId).HasColumnName("playlist_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Playlist).WithMany(p => p.LikePlaylists)
                .HasForeignKey(d => d.PlaylistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LikePlaylists_Playlists");

            entity.HasOne(d => d.User).WithMany(p => p.LikePlaylists)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LikePlaylists_Users");
        });

        modelBuilder.Entity<LikeTrack>(entity =>
        {
            entity.HasKey(e => e.LikeTrackId).HasName("PK__LikeTrac__C25CFAA759CAA246");

            entity.HasIndex(e => e.TrackId, "IX_LikeTracks_track_id");

            entity.HasIndex(e => e.UserId, "IX_LikeTracks_user_id");

            entity.Property(e => e.LikeTrackId).HasColumnName("like_track_id");
            entity.Property(e => e.TrackId).HasColumnName("track_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Track).WithMany(p => p.LikeTracks)
                .HasForeignKey(d => d.TrackId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LikeTracks_Tracks");

            entity.HasOne(d => d.User).WithMany(p => p.LikeTracks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LikeTracks_Users");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__E059842FEB61BD7D");

            entity.HasIndex(e => e.ActorId, "IX_Notifications_actor_id");

            entity.HasIndex(e => e.RecipienId, "IX_Notifications_recipien_id");

            entity.Property(e => e.NotificationId).HasColumnName("notification_id");
            entity.Property(e => e.ActorId).HasColumnName("actor_id");
            entity.Property(e => e.Content)
                .HasMaxLength(225)
                .HasColumnName("content");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.IsRead).HasColumnName("is_read");
            entity.Property(e => e.RecipienId).HasColumnName("recipien_id");
            entity.Property(e => e.Title)
                .HasMaxLength(225)
                .HasColumnName("title");

            entity.HasOne(d => d.Actor).WithMany(p => p.NotificationActors)
                .HasForeignKey(d => d.ActorId)
                .HasConstraintName("FK_Notifications_Actor");

            entity.HasOne(d => d.Recipien).WithMany(p => p.NotificationRecipiens)
                .HasForeignKey(d => d.RecipienId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notifications_Recipient");
        });

        modelBuilder.Entity<Playlist>(entity =>
        {
            entity.HasKey(e => e.PlaylistId).HasName("PK__Playlist__FB9C1410280ABA50");

            entity.HasIndex(e => e.MakeBy, "IX_Playlists_make_by");

            entity.Property(e => e.PlaylistId).HasColumnName("playlist_id");
            entity.Property(e => e.IsPublish).HasColumnName("is_publish");
            entity.Property(e => e.MakeBy).HasColumnName("make_by");
            entity.Property(e => e.MakeDate)
                .HasColumnType("datetime")
                .HasColumnName("make_date");
            entity.Property(e => e.PicturePlaylistUrl)
                .HasMaxLength(2048)
                .HasColumnName("picture_playlist_url");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.HasOne(d => d.MakeByNavigation).WithMany(p => p.Playlists)
                .HasForeignKey(d => d.MakeBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Playlists_Users");
        });

        modelBuilder.Entity<PlaylistTrack>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Playlist__3213E83F7A64EDD4");

            entity.HasIndex(e => e.PlaylistId, "IX_PlaylistTracks_playlist_id");

            entity.HasIndex(e => e.TrackId, "IX_PlaylistTracks_track_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PlaylistId).HasColumnName("playlist_id");
            entity.Property(e => e.TrackId).HasColumnName("track_id");

            entity.HasOne(d => d.Playlist).WithMany(p => p.PlaylistTracks)
                .HasForeignKey(d => d.PlaylistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlaylistTracks_Playlists");

            entity.HasOne(d => d.Track).WithMany(p => p.PlaylistTracks)
                .HasForeignKey(d => d.TrackId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlaylistTracks_Tracks");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__760965CCCC573E9E");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleDescription).HasColumnName("role_description");
            entity.Property(e => e.RoleName)
                .HasMaxLength(225)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Services__3E0DB8AF195D3BB2");

            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.Description)
                .HasMaxLength(225)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(225)
                .HasColumnName("name");
            entity.Property(e => e.OldPrice)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("old_price");
            entity.Property(e => e.SellPrice)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("sell_price");
        });

        modelBuilder.Entity<SystemReport>(entity =>
        {
            entity.HasKey(e => e.SystemReportId).HasName("PK__SystemRe__668F938B69F7A9E4");

            entity.HasIndex(e => e.UserId, "IX_SystemReports_user_id");

            entity.Property(e => e.SystemReportId).HasColumnName("system_report_id");
            entity.Property(e => e.Content)
                .HasMaxLength(2048)
                .HasColumnName("content");
            entity.Property(e => e.ReplyContent)
                .HasMaxLength(2048)
                .HasColumnName("reply_content");
            entity.Property(e => e.ReplyDate)
                .HasColumnType("datetime")
                .HasColumnName("reply_date");
            entity.Property(e => e.ReportDate)
                .HasColumnType("datetime")
                .HasColumnName("report_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.SystemReports)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SystemReports_Users");
        });

        modelBuilder.Entity<Track>(entity =>
        {
            entity.HasKey(e => e.TrackId).HasName("PK__Tracks__24ECC82EB4CD4B2A");

            entity.HasIndex(e => e.UpdateBy, "IX_Tracks_update_by");

            entity.Property(e => e.TrackId).HasColumnName("track_id");
            entity.Property(e => e.AudioFileUrl)
                .HasMaxLength(2048)
                .HasColumnName("audio_file_url");
            entity.Property(e => e.CoverArtUrl)
                .HasMaxLength(2048)
                .HasColumnName("cover_art_url");
            entity.Property(e => e.Description)
                .HasMaxLength(225)
                .HasColumnName("description");
            entity.Property(e => e.DurationInSeconds).HasColumnName("duration_in_seconds");
            entity.Property(e => e.IsPublic).HasColumnName("is_public");
            entity.Property(e => e.PlayCount).HasColumnName("play_count");
            entity.Property(e => e.Title)
                .HasMaxLength(225)
                .HasColumnName("title");
            entity.Property(e => e.UpdateBy).HasColumnName("update_by");
            entity.Property(e => e.UploadDate)
                .HasColumnType("datetime")
                .HasColumnName("upload_date");
            entity.Property(e => e.WaveformUrl)
                .HasMaxLength(2048)
                .HasColumnName("waveform_url");

            entity.HasOne(d => d.UpdateByNavigation).WithMany(p => p.Tracks)
                .HasForeignKey(d => d.UpdateBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tracks_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370F3633A4C2");

            entity.HasIndex(e => e.RoleId, "IX_Users_role_id");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E61641B9BDFBE").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__Users__F3DBC572E07BA08A").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Bio)
                .HasMaxLength(225)
                .HasColumnName("bio");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.DayOfBirth).HasColumnName("day_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.HashedPassword)
                .HasMaxLength(255)
                .HasColumnName("hashed_password");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .HasColumnName("phone_number");
            entity.Property(e => e.ProfilePictureUrl)
                .HasMaxLength(2048)
                .HasColumnName("profile_picture_url");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Status)
                .HasMaxLength(225)
                .HasColumnName("status");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
