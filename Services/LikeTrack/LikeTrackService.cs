using Data.Models;
using Repositories.LikeTrack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.LikeTrack
{
    public class LikeTrackService : ILikeTrackService
    {
        private readonly ILikeTrackRepository _likeTrackRepository;
        public LikeTrackService(ILikeTrackRepository likeTrackRepository)
        {
            _likeTrackRepository = likeTrackRepository;
        }

        public async Task<int> GetLikeTrackCount(int trackId)
        {
            return await _likeTrackRepository.GetLikeTrackCount(trackId);
        }

        public async Task<bool> isLikedTrack(int trackId, int userId)
        {
            return await _likeTrackRepository.isLikedTrack(trackId, userId);
        }

        public async Task<bool> toggleUserLikeTrackStatus(int trackId, int userId)
        {
            return await _likeTrackRepository.toggleUserLikeTrackStatus(trackId, userId);
        }
    }
}
