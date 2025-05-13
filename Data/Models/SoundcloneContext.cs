using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
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

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Playlist> Playlists { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Song> Songs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPlaylist> UserPlaylists { get; set; }

    
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    var builder = new ConfigurationBuilder();
    builder.SetBasePath(Directory.GetCurrentDirectory());
    builder.AddJsonFile("appsettings.json", optional: true , reloadOnChange: true);
    var configuration = builder.Build();
    optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__comment__3213E83F00102BEF");

            entity.ToTable("comment");

            entity.HasIndex(e => e.Id, "UQ__comment__3213E83E90533DD5").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(255)
                .HasColumnName("content");
            entity.Property(e => e.FatherCommentId).HasColumnName("father_comment_id");
            entity.Property(e => e.NumLike).HasColumnName("num_like");
            entity.Property(e => e.SongId).HasColumnName("song_id");
            entity.Property(e => e.Time).HasColumnName("time");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Song).WithMany(p => p.Comments)
                .HasForeignKey(d => d.SongId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_comment_song");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_comment_user");
        });

        modelBuilder.Entity<Follow>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__follows__3213E83F0803EF63");

            entity.ToTable("follows");

            entity.HasIndex(e => e.Id, "UQ__follows__3213E83EB6684310").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FollowedAt).HasColumnName("followed_at");
            entity.Property(e => e.FollowedId).HasColumnName("followed_id");
            entity.Property(e => e.FollowerId).HasColumnName("follower_id");

            entity.HasOne(d => d.Followed).WithMany(p => p.FollowFolloweds)
                .HasForeignKey(d => d.FollowedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_follows_followed");

            entity.HasOne(d => d.Follower).WithMany(p => p.FollowFollowers)
                .HasForeignKey(d => d.FollowerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_follows_follower");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__notifica__3213E83FA025D17D");

            entity.ToTable("notifications");

            entity.HasIndex(e => e.Id, "UQ__notifica__3213E83E8A66C813").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(255)
                .HasColumnName("content");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.IsRead).HasColumnName("is_read");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_notifications_user");
        });

        modelBuilder.Entity<Playlist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__playlist__3213E83F63E5859B");

            entity.ToTable("playlist");

            entity.HasIndex(e => e.Id, "UQ__playlist__3213E83EE8208316").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Picture).HasColumnName("picture");
            entity.Property(e => e.PlaylistId).HasColumnName("playlist_id");
            entity.Property(e => e.Public).HasColumnName("public");
            entity.Property(e => e.SongId).HasColumnName("song_id");

            entity.HasOne(d => d.Author).WithMany(p => p.Playlists)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_playlist_user_author");

            entity.HasOne(d => d.Song).WithMany(p => p.Playlists)
                .HasForeignKey(d => d.SongId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_playlist_song");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RefreshT__3214EC0736668486");

            entity.Property(e => e.ExpiresAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__report__3213E83FB38120FF");

            entity.ToTable("report");

            entity.HasIndex(e => e.Id, "UQ__report__3213E83E9A6F3B16").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(255)
                .HasColumnName("content");
            entity.Property(e => e.SongId).HasColumnName("song_id");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Song).WithMany(p => p.Reports)
                .HasForeignKey(d => d.SongId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_report_song");

            entity.HasOne(d => d.User).WithMany(p => p.Reports)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_report_user");
        });

        modelBuilder.Entity<Song>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__song__3213E83F29068759");

            entity.ToTable("song");

            entity.HasIndex(e => e.Id, "UQ__song__3213E83EB01D3778").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Author).HasColumnName("author");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.NumLike).HasColumnName("num_like");
            entity.Property(e => e.NumPlays).HasColumnName("num_plays");
            entity.Property(e => e.Picture).HasColumnName("picture");
            entity.Property(e => e.Public).HasColumnName("public");
            entity.Property(e => e.Source).HasColumnName("source");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user__3213E83F04710E00");

            entity.ToTable("user");

            entity.HasIndex(e => e.Id, "UQ__user__3213E83E3F4EBA4F").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__user__AB6E616445AFF5F8").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__user__F3DBC5723E8C0CA1").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Avt).HasColumnName("avt");
            entity.Property(e => e.Bio)
                .HasMaxLength(255)
                .HasColumnName("bio");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Fullname)
                .HasMaxLength(255)
                .HasColumnName("fullname");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UserPlaylist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_pla__3213E83FD311D445");

            entity.ToTable("user_playlist");

            entity.HasIndex(e => e.Id, "UQ__user_pla__3213E83E0B82FBA5").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PlaylistId).HasColumnName("playlist_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Playlist).WithMany(p => p.UserPlaylists)
                .HasForeignKey(d => d.PlaylistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_playlist_playlist");

            entity.HasOne(d => d.User).WithMany(p => p.UserPlaylists)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_playlist_user");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
