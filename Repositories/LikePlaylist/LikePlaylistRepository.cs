using Data.DTOs;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.LikePlaylist
{
    public class LikePlaylistRepository : ILikePlaylistRepository
    {
        private readonly SoundcloneContext _soundcloneContext;

        public LikePlaylistRepository(SoundcloneContext soundcloneContext)
        {
            _soundcloneContext = soundcloneContext;
        }

        public async Task<int> GetLikePlaylistCount(int playlistId)
        {
            var count = await _soundcloneContext.LikePlaylists.Where(x => x.PlaylistId == playlistId).CountAsync();
            return count;
        }

        public async Task<List<LikePlaylistDTO>> GetLikePlaylistOfUser(int userId)
        {
            try
            {
                var playlistsLikeByUser = await _soundcloneContext.LikePlaylists.
                    Where(x => x.UserId == userId).
                    Select(x => new LikePlaylistDTO()
                {
                    LikePlaylistId = x.LikePlaylistId,
                    UserId = x.UserId,
                    PlaylistId = x.PlaylistId,

                }).ToListAsync();
                return playlistsLikeByUser;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> LikePlaylist(LikePlaylistDTO model)
        {
            try
            {
                Data.Models.User user = await _soundcloneContext.Users.FindAsync(model.UserId);
                Data.Models.Playlist playlist = await _soundcloneContext.Playlists.FindAsync(model.PlaylistId);

                if (user == null || playlist == null)
                {
                    return false;
                }

                var playlistUserLike = await _soundcloneContext.LikePlaylists.Where(x => x.UserId == model.UserId).ToListAsync();
                foreach (var item in playlistUserLike)
                {
                    if (item.PlaylistId == model.PlaylistId)
                    {
                        return false;
                    }
                }

                Data.Models.LikePlaylist newLikePlaylist = new Data.Models.LikePlaylist()
                {
                    UserId = model.UserId,
                    PlaylistId = model.PlaylistId
                };

                await _soundcloneContext.LikePlaylists.AddAsync(newLikePlaylist);
                await _soundcloneContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> UnlikePlaylist(int likePlaylistId)
        {
            try
            {
                var likePlaylist = await _soundcloneContext.LikePlaylists.FindAsync(likePlaylistId);
                          
                if (likePlaylist == null)
                {
                    return false;
                }
                _soundcloneContext.LikePlaylists.Remove(likePlaylist);
                await _soundcloneContext.SaveChangesAsync() ;
                return true;


            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
