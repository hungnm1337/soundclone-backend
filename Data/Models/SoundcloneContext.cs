using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=Soundclone;User Id=sa;Password=123;Encrypt=False;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comments__E7957687CA92A4D4");

            entity.Property(e => e.CommentId).HasColumnName("comment_id");
            entity.Property(e => e.Content)
                .HasMaxLength(225)
                .IsUnicode(false)
                .HasColumnName("content");
            entity.Property(e => e.ParentCommentId).HasColumnName("parent_comment_id");
            entity.Property(e => e.TrackId).HasColumnName("track_id");
            entity.Property(e => e.WriteBy).HasColumnName("write_by");
            entity.Property(e => e.WriteDate)
                .HasColumnType("datetime")
                .HasColumnName("write_date");

            entity.HasOne(d => d.ParentComment).WithMany(p => p.InverseParentComment)
                .HasForeignKey(d => d.ParentCommentId)
                .HasConstraintName("FK__Comments__parent__5DCAEF64");

            entity.HasOne(d => d.Track).WithMany(p => p.Comments)
                .HasForeignKey(d => d.TrackId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comments__track___5CD6CB2B");

            entity.HasOne(d => d.WriteByNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.WriteBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comments__write___5EBF139D");
        });

        modelBuilder.Entity<Follow>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Follows__3213E83FDC62D6AE");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ArtistId).HasColumnName("artist_id");
            entity.Property(e => e.FollowerId).HasColumnName("follower_id");

            entity.HasOne(d => d.Artist).WithMany(p => p.FollowArtists)
                .HasForeignKey(d => d.ArtistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Follows__artist___534D60F1");

            entity.HasOne(d => d.Follower).WithMany(p => p.FollowFollowers)
                .HasForeignKey(d => d.FollowerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Follows__followe__5441852A");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoices__F58DFD49AA72399D");

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
                .HasConstraintName("FK__Invoices__servic__4F7CD00D");
        });

        modelBuilder.Entity<LikePlaylist>(entity =>
        {
            entity.HasKey(e => e.LikePlaylistId).HasName("PK__LikePlay__23AB7E9BAC90D998");

            entity.Property(e => e.LikePlaylistId).HasColumnName("like_playlist_id");
            entity.Property(e => e.PlaylistId).HasColumnName("playlist_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Playlist).WithMany(p => p.LikePlaylists)
                .HasForeignKey(d => d.PlaylistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LikePlayl__playl__5BE2A6F2");

            entity.HasOne(d => d.User).WithMany(p => p.LikePlaylists)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LikePlayl__user___59063A47");
        });

        modelBuilder.Entity<LikeTrack>(entity =>
        {
            entity.HasKey(e => e.LikeTrackId).HasName("PK__LikeTrac__C25CFAA738BD781F");

            entity.Property(e => e.LikeTrackId).HasColumnName("like_track_id");
            entity.Property(e => e.TrackId).HasColumnName("track_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Track).WithMany(p => p.LikeTracks)
                .HasForeignKey(d => d.TrackId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LikeTrack__track__5AEE82B9");

            entity.HasOne(d => d.User).WithMany(p => p.LikeTracks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LikeTrack__user___59FA5E80");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__E059842F9D8D1E30");

            entity.Property(e => e.NotificationId).HasColumnName("notification_id");
            entity.Property(e => e.ActorId).HasColumnName("actor_id");
            entity.Property(e => e.Content)
                .HasMaxLength(225)
                .IsUnicode(false)
                .HasColumnName("content");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.IsRead).HasColumnName("is_read");
            entity.Property(e => e.RecipienId).HasColumnName("recipien_id");
            entity.Property(e => e.Title)
                .HasMaxLength(225)
                .IsUnicode(false)
                .HasColumnName("title");

            entity.HasOne(d => d.Actor).WithMany(p => p.NotificationActors)
                .HasForeignKey(d => d.ActorId)
                .HasConstraintName("FK__Notificat__actor__52593CB8");

            entity.HasOne(d => d.Recipien).WithMany(p => p.NotificationRecipiens)
                .HasForeignKey(d => d.RecipienId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__recip__5165187F");
        });

        modelBuilder.Entity<Playlist>(entity =>
        {
            entity.HasKey(e => e.PlaylistId).HasName("PK__Playlist__FB9C14100D3B0C73");

            entity.Property(e => e.PlaylistId).HasColumnName("playlist_id");
            entity.Property(e => e.MakeBy).HasColumnName("make_by");
            entity.Property(e => e.MakeDate)
                .HasColumnType("datetime")
                .HasColumnName("make_date");
            entity.Property(e => e.PicturePlaylistUrl)
                .HasMaxLength(2048)
                .IsUnicode(false)
                .HasColumnName("picture_playlist_url");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");

            entity.HasOne(d => d.MakeByNavigation).WithMany(p => p.Playlists)
                .HasForeignKey(d => d.MakeBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Playlists__make___5629CD9C");
        });

        modelBuilder.Entity<PlaylistTrack>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Playlist__3213E83F1CF58DF5");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PlaylistId).HasColumnName("playlist_id");
            entity.Property(e => e.TrackId).HasColumnName("track_id");

            entity.HasOne(d => d.Playlist).WithMany(p => p.PlaylistTracks)
                .HasForeignKey(d => d.PlaylistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PlaylistT__playl__571DF1D5");

            entity.HasOne(d => d.Track).WithMany(p => p.PlaylistTracks)
                .HasForeignKey(d => d.TrackId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PlaylistT__track__5812160E");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__760965CC1E4ECC45");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleDescription).HasColumnName("role_description");
            entity.Property(e => e.RoleName)
                .HasMaxLength(225)
                .IsUnicode(false)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Services__3E0DB8AFD27D551C");

            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.Description)
                .HasMaxLength(225)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(225)
                .IsUnicode(false)
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
            entity.HasKey(e => e.SystemReportId).HasName("PK__SystemRe__668F938B42F4138F");

            entity.Property(e => e.SystemReportId).HasColumnName("system_report_id");
            entity.Property(e => e.Content)
                .HasMaxLength(2048)
                .IsUnicode(false)
                .HasColumnName("content");
            entity.Property(e => e.ReplyContent)
                .HasMaxLength(2048)
                .IsUnicode(false)
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
                .HasConstraintName("FK__SystemRep__user___5070F446");
        });

        modelBuilder.Entity<Track>(entity =>
        {
            entity.HasKey(e => e.TrackId).HasName("PK__Tracks__24ECC82E3B722E9F");

            entity.Property(e => e.TrackId).HasColumnName("track_id");
            entity.Property(e => e.AudioFileUrl)
                .HasMaxLength(2048)
                .IsUnicode(false)
                .HasColumnName("audio_file_url");
            entity.Property(e => e.CoverArtUrl)
                .HasMaxLength(2048)
                .IsUnicode(false)
                .HasColumnName("cover_art_url");
            entity.Property(e => e.Description)
                .HasMaxLength(225)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.DurationInSeconds).HasColumnName("duration_in_seconds");
            entity.Property(e => e.IsPublic).HasColumnName("is_public");
            entity.Property(e => e.PlayCount).HasColumnName("play_count");
            entity.Property(e => e.Title)
                .HasMaxLength(225)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.UpdateBy).HasColumnName("update_by");
            entity.Property(e => e.UploadDate)
                .HasColumnType("datetime")
                .HasColumnName("upload_date");
            entity.Property(e => e.WaveformUrl)
                .HasMaxLength(2048)
                .IsUnicode(false)
                .HasColumnName("waveform_url");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370F7ACC61DE");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E6164E343084B").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__Users__F3DBC572FEE4F907").IsUnique();

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");
            entity.Property(e => e.Bio)
                .HasMaxLength(225)
                .IsUnicode(false)
                .HasColumnName("bio");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.DayOfBirth).HasColumnName("day_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.HashedPassword)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("hashed_password");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.ProfilePictureUrl)
                .HasMaxLength(2048)
                .IsUnicode(false)
                .HasColumnName("profile_picture_url");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Status)
                .HasMaxLength(225)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__role_id__5535A963");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
