using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.LikePlaylist
{
    public interface ILikePlaylistRepository
    {
        Task<bool> ToggleUserLikePlaylistStatus(int playlistId, int userId);
        Task<bool> IsLikedPlaylist(int playlistId, int userId);

        Task<int> GetLikePlaylistCount(int playlistId);
    }
}
