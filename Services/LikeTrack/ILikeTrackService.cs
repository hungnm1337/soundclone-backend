using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.LikeTrack
{
    public interface ILikeTrackService
    {
        Task<bool> toggleUserLikeTrackStatus(int trackId, int userId);

        Task<bool> isLikedTrack(int trackId, int userId);

        Task<int> GetLikeTrackCount(int trackId);
    }
}
