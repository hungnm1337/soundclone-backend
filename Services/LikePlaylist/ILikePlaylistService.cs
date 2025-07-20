using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.LikePlaylist
{
    public interface ILikePlaylistService
    {
        Task<List<LikePlaylistDTO>> GetLikePlaylistOfUser(int userId);
        Task<bool> LikePlaylist(LikePlaylistDTO playlist);
        Task<bool> UnlikePlaylist(int likePlaylistId);
    }
}
