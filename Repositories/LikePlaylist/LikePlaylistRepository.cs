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

        public async Task<bool> IsLikedPlaylist(int playlistId, int userId)
        {
            var liked = await _soundcloneContext.LikePlaylists.
                FirstOrDefaultAsync(x => x.PlaylistId == playlistId && x.UserId == userId);
            return liked != null;
        }

        public async Task<bool> ToggleUserLikePlaylistStatus(int playlistId, int userId)
        {
            var liked = await _soundcloneContext.LikePlaylists.
              FirstOrDefaultAsync(x => x.PlaylistId == playlistId && x.UserId == userId);
            try
            {
                if (liked != null)
                {
                    _soundcloneContext.LikePlaylists.Remove(liked);
                  
                }
                else
                {
                    var newLike = new Data.Models.LikePlaylist()
                    {
                        PlaylistId = playlistId,
                        UserId = userId
                    };
                    _soundcloneContext.LikePlaylists.Add(newLike);
                 
                }
                await _soundcloneContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
