using Data.DTOs;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ListData
{
    public class ListDataRepository : IListDataRepository
    {
        private readonly SoundcloneContext _soundcloneContext;

        public ListDataRepository(SoundcloneContext soundcloneContext)
        {
            _soundcloneContext = soundcloneContext;
        }

        public async Task<List<ArtistDTO>> GetUsers()
        {
            var artists = await _soundcloneContext.Users.Select(
                x => new ArtistDTO
                {
                    Name = x.Name,
                    ProfilePictureUrl = x.ProfilePictureUrl,
                    UserId = x.UserId,
                }
                ).ToListAsync();
            return artists;
        }

        public async Task<List<FollowDTO>> GetFollow()
        {
            return await _soundcloneContext.Follows.Select(
                x => new FollowDTO
                {
                    Id = x.Id,
                    ArtistId = x.ArtistId,
                    FollowerId = x.FollowerId,
                }
                ).ToListAsync();
        }

        public async Task<List<LikePlaylistDTO>> GetLikePlaylist()
        {
            return await _soundcloneContext.LikePlaylists.Select(
                x => new LikePlaylistDTO
                {
                    LikePlaylistId = x.LikePlaylistId,
                    PlaylistId = x.PlaylistId,
                    UserId = x.UserId
                }
                ).ToListAsync();
        }

        public async Task<List<LikeTrackDTO>> GetLikeTracks()
        {
            return await _soundcloneContext.LikeTracks.Select(
                x => new LikeTrackDTO
                {
                    UserId = x.UserId,
                    LikeTrackId = x.LikeTrackId,
                    TrackId = x.TrackId,
                }
                ).ToListAsync();
        }

        public async Task<List<ListPlaylistDTO>> GetPlaylist()
        {
            var playlists = await _soundcloneContext.Playlists.Where(x => x.IsPublish)
                .Select(x => new
                {
                    x.MakeBy,
                    x.PlaylistId,
                    x.Title,
                    x.PicturePlaylistUrl,

                }).ToListAsync();
            var userIds = playlists.Select(t => t.MakeBy).Distinct().ToList();
            var users = await _soundcloneContext.Users
                .Where(u => userIds.Contains(u.UserId))
                .ToDictionaryAsync(u => u.UserId, u => u.Name);
            
            // Đếm số lượng bài hát cho mỗi playlist
            var playlistIds = playlists.Select(p => p.PlaylistId).ToList();
            var trackCounts = await _soundcloneContext.PlaylistTracks
                .Where(pt => playlistIds.Contains(pt.PlaylistId))
                .GroupBy(pt => pt.PlaylistId)
                .ToDictionaryAsync(g => g.Key, g => g.Count());
            
            var dtos = playlists.Select(p => new ListPlaylistDTO
            {
                PicturePlaylistUrl = p.PicturePlaylistUrl,
                Title = p.Title,
                PlaylistId = p.PlaylistId,
                ArtistName = users.TryGetValue(p.MakeBy, out var name) ? name : null,
                NumTrack = trackCounts.TryGetValue(p.PlaylistId, out var count) ? count : 0,
            }).ToList();
            return dtos;
        }

        public async Task<List<ListTrackDTO>> GetTracks()
        {
            var tracks = await _soundcloneContext.Tracks.Where(x => x.IsPublic)
                .Select(x => new
                {
                    x.CoverArtUrl,
                    x.Title,
                    x.TrackId,
                    x.UpdateBy
                })
                .ToListAsync();

            var userIds = tracks.Select(t => t.UpdateBy).Distinct().ToList();
            var users = await _soundcloneContext.Users
                .Where(u => userIds.Contains(u.UserId))
                .ToDictionaryAsync(u => u.UserId, u => u.Name);

            var dtos = tracks.Select(t => new ListTrackDTO
            {
                ArtistName = users.TryGetValue(t.UpdateBy, out var name) ? name : null,
                CoverArtUrl = t.CoverArtUrl,
                Title = t.Title,
                TrackId = t.TrackId
            }).ToList();

            return dtos;
        }

        public async Task<List<ListTrackDTO>> GetTracksByArtistId(int artistId)
        {
            var tracks = await _soundcloneContext.Tracks
                .Where(x => x.IsPublic == true && x.UpdateBy == artistId)
                .Select(x => new
                {
                    x.UpdateBy,
                    x.Title,
                    x.TrackId,
                    x.CoverArtUrl,
                }).ToListAsync();
            var userIds = tracks.Select(t => t.UpdateBy).Distinct().ToList();
            var users = await _soundcloneContext.Users
                .Where(u => userIds.Contains(u.UserId))
                .ToDictionaryAsync(u => u.UserId, u => u.Name);
            var dtos = tracks.Select(t => new ListTrackDTO
            {
                ArtistName = users.TryGetValue(t.UpdateBy, out var name) ? name : null,
                CoverArtUrl = t.CoverArtUrl,
                Title = t.Title,
                TrackId = t.TrackId
            }).ToList();
            return dtos;
        }

        public async Task<List<ListPlaylistDTO>> GetPlaylistByArtistId(int artistId)
        {
            var playlists = await _soundcloneContext.Playlists
                .Where(x => x.IsPublish && x.MakeBy == artistId)
                .Select(x => new
                {
                    x.MakeBy,
                    x.Title,
                    x.PlaylistId,
                    x.PicturePlaylistUrl,
                }).ToListAsync();
            var userIds = playlists.Select(t => t.MakeBy).Distinct().ToList();
            var users = await _soundcloneContext.Users
                .Where(u => userIds.Contains(u.UserId))
                .ToDictionaryAsync(u => u.UserId, u => u.Name);
            
            // Đếm số lượng bài hát cho mỗi playlist
            var playlistIds = playlists.Select(p => p.PlaylistId).ToList();
            var trackCounts = await _soundcloneContext.PlaylistTracks
                .Where(pt => playlistIds.Contains(pt.PlaylistId))
                .GroupBy(pt => pt.PlaylistId)
                .ToDictionaryAsync(g => g.Key, g => g.Count());
            
            var dtos = playlists.Select(t => new ListPlaylistDTO
            {
                ArtistName = users.TryGetValue(t.MakeBy, out var name) ? name : null,
                PicturePlaylistUrl = t.PicturePlaylistUrl,
                Title = t.Title,
                PlaylistId = t.PlaylistId,
                NumTrack = trackCounts.TryGetValue(t.PlaylistId, out var count) ? count : 0,
            }).ToList();
            return dtos;

        }

        public async Task<List<ListTrackDTO>> GetTracksByPlaylistId(int playlistId)
        {
            var tracksInPlaylist = await _soundcloneContext.PlaylistTracks
                .Where(x => x.PlaylistId == playlistId)
                .Select(x => x.TrackId)
                .ToListAsync();
            var tracks = await _soundcloneContext.Tracks
                .Where(x => x.IsPublic && tracksInPlaylist.Contains(x.TrackId))
                .Select(x => new 
                {
                    x.UpdateBy,
                    x.Title,
                    x.CoverArtUrl,
                    x.TrackId
                }).ToListAsync();

            var userIds = tracks.Select(t => t.UpdateBy).Distinct().ToList();
            var users = await _soundcloneContext.Users
                .Where(u => userIds.Contains(u.UserId))
                .ToDictionaryAsync(u => u.UserId, u => u.Name);
            var dtos = tracks.Select(t => new ListTrackDTO
            {
                ArtistName = users.TryGetValue(t.UpdateBy, out var name) ? name : null,
                CoverArtUrl = t.CoverArtUrl,
                Title = t.Title,
                TrackId = t.TrackId
            }).ToList();
            return dtos;

        }
    }
}
