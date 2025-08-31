using Data.DTOs;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Playlist
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly SoundcloneContext _soundcloneContext;

        public PlaylistRepository(SoundcloneContext soundcloneContext)
        {
            _soundcloneContext = soundcloneContext;
        }

        public async Task<bool> AddTrackToPlaylist(AddTrackToPlaylistDTO model)
        {
            try
            {
                PlaylistTrack newPlaylistTrack = new PlaylistTrack()
                {
                    PlaylistId = model.PlaylistId,
                    TrackId = model.TrackId
                };
                _soundcloneContext.PlaylistTracks.Add(newPlaylistTrack);
                await _soundcloneContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> ChangeStatusPublicOfPlaylist(ChangeStatusPlaylistDTO model)
        {
            try
            {
                Data.Models.Playlist playlist = await _soundcloneContext.Playlists.FindAsync(model.PlaylistId);
                if (playlist == null || playlist.MakeBy != model.MakeBy)
                {
                    return false;
                }
                
                playlist.IsPublish = !playlist.IsPublish;
                _soundcloneContext.Playlists.Update(playlist);
                await _soundcloneContext.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<PlaylistDTO> CreateNewPlaylist(PlaylistDTO playlist)
        {
            try
            {
                var playlistOfUser = await _soundcloneContext.Playlists.
                Where(x => x.MakeBy == playlist.MakeBy)
                .ToListAsync();

                var playlistExistedHaveSameName = playlistOfUser.
                    Where(x => x.Title.Equals(playlist.Title)).
                    FirstOrDefault();

                if (playlistExistedHaveSameName != null)
                {
                    playlist.Title = playlist.Title + DateTime.Now.ToString();
                }

                Data.Models.Playlist newPlayList = new Data.Models.Playlist()
                {
                    Title = playlist.Title,
                    MakeBy = playlist.MakeBy,
                    MakeDate = DateTime.Now,
                    PicturePlaylistUrl = playlist.PicturePlaylistUrl,
                    IsPublish = playlist.IsPublish,
                    
                };

                _soundcloneContext.Playlists.Add(newPlayList);
                await _soundcloneContext.SaveChangesAsync();
                return playlist;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> DeletePlaylist(DeletePlaylistDTO model)
        {
            try
            {
                var likedPlaylists = await _soundcloneContext.LikePlaylists
                    .Where(x => x.PlaylistId == model.PlaylistId)
                    .ToListAsync();

                var tracksInPlaylist = await _soundcloneContext.PlaylistTracks
                    .Where(x => x.PlaylistId == model.PlaylistId)
                    .ToListAsync();

                var playlist = await _soundcloneContext.Playlists
                    .FindAsync(model.PlaylistId);

                if (playlist == null || playlist.MakeBy != model.UserId) return false;

                _soundcloneContext.LikePlaylists.RemoveRange(likedPlaylists);
                _soundcloneContext.PlaylistTracks.RemoveRange(tracksInPlaylist);
                _soundcloneContext.Playlists.Remove(playlist);

                await _soundcloneContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<IEnumerable<Data.Models.Playlist>> GetPlaylistByUserId(int userId)
        {
            try
            {
                var playlistOfUser = await _soundcloneContext.Playlists.
                    Where(x => x.MakeBy == userId).
                    ToListAsync();
                return playlistOfUser;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PlayListDetailDTO> GetPlayListDetail(int playlistId)
        {
            var playlist = await _soundcloneContext.Playlists.
               Include(x => x.PlaylistTracks).
               Where(x => x.PlaylistId == playlistId).Select(
               x => new PlayListDetailDTO()
               {
                   PlaylistId = x.PlaylistId,
                   Title = x.Title,
                   PicturePlaylistUrl = x.PicturePlaylistUrl,
                   TrackQuantity = x.PlaylistTracks.Count,
                   IsPublish = x.IsPublish,
                   ArtistId = x.MakeBy,
                   ArtistName = "Artist"

               })
               .FirstOrDefaultAsync();

            var artist = await _soundcloneContext.Users.FindAsync(playlist.ArtistId);
            playlist.ArtistName = artist != null ? artist.Name : "Playlist public";
            return playlist;
        }

        public async Task<IEnumerable<PlaylistMenuDTO>> GetPlaylistMenu(int userId)
        {
            var playlists = await _soundcloneContext.Playlists.
                Include(x => x.PlaylistTracks).
                Where(x => x.MakeBy == userId).Select(
                x => new PlaylistMenuDTO()
                {
                    PlaylistId = x.PlaylistId,
                    Title = x.Title,
                    PicturePlaylistUrl = x.PicturePlaylistUrl,
                    TrackQuantity = x.PlaylistTracks.Count,
                    IsPublish = x.IsPublish,
                })
                .ToListAsync();
            return playlists;
        }

        
        public async Task<UpdatePlaylistDTO> UpdatePlaylist(UpdatePlaylistDTO playlist)
        {
            try
            {
                var playlistCur = await _soundcloneContext.Playlists.FindAsync(playlist.PlaylistId);
                if (playlistCur == null || playlistCur.MakeBy!= playlist.UserId)
                {
                    return null;
                }
                playlistCur.Title = playlist.Title;
                playlistCur.PicturePlaylistUrl = playlist.PicturePlaylistUrl;
                playlistCur.IsPublish = playlist.IsPublish;
               

                _soundcloneContext.Playlists.Update(playlistCur);
                await _soundcloneContext.SaveChangesAsync();
                return playlist;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}
