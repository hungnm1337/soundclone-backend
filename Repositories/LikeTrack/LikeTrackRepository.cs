using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.LikeTrack
{
    public class LikeTrackRepository : ILikeTrackRepository
    {
        private readonly SoundcloneContext _soundcloneContext;

        public LikeTrackRepository(SoundcloneContext soundcloneContext)
        {
            _soundcloneContext = soundcloneContext;
        }

        public async Task<int> GetLikeTrackCount(int trackId)
        {
            var count = await _soundcloneContext.LikeTracks.Where(x => x.TrackId == trackId).CountAsync();
            return count;
        }

        public async Task<bool> isLikedTrack(int trackId, int userId)
        {
            var like = await _soundcloneContext.LikeTracks
                               .FirstOrDefaultAsync(x => x.UserId == userId && x.TrackId == trackId);

            return like != null;

        }

        public async Task<bool> toggleUserLikeTrackStatus(int trackId, int userId)
        {
            try
            {
                var like = await _soundcloneContext.LikeTracks
                    .FirstOrDefaultAsync(x => x.UserId == userId && x.TrackId == trackId);

                if (like == null)
                {
                    var newLikeTrack = new Data.Models.LikeTrack
                    {
                        UserId = userId,
                        TrackId = trackId
                    };
                    _soundcloneContext.LikeTracks.Add(newLikeTrack);
                }
                else
                {
                    _soundcloneContext.LikeTracks.Remove(like);
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
