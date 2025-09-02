using Data.DTOs;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Follow
{
    public class FollowRepository : IFollowRepository
    {
        private readonly SoundcloneContext _soundcloneContext;

        public FollowRepository(SoundcloneContext soundcloneContext)
        {
            _soundcloneContext = soundcloneContext;
        }
        public async Task<bool> isFollowing(FollowDTO model)
        {
            var isFollowing = await _soundcloneContext.Follows.FirstOrDefaultAsync(x => x.FollowerId == model.FollowerId && x.ArtistId == model.ArtistId);
            return isFollowing != null;
        }

        public async Task<bool> toggleUserFollowStatus(FollowDTO model)
        {
            try
            {
                var follow = await _soundcloneContext.Follows.FirstOrDefaultAsync(x => x.FollowerId == model.FollowerId && x.ArtistId == model.ArtistId);

                if (follow != null)
                {
                    _soundcloneContext.Follows.Remove(follow);
                }
                else
                {
                    await _soundcloneContext.Follows.AddAsync(
                        new Data.Models.Follow()
                        {
                            ArtistId = model.ArtistId,
                            FollowerId = model.FollowerId
                        });
                }
                await _soundcloneContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
