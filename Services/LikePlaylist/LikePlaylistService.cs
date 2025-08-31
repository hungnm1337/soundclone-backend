using Data.DTOs;
using Repositories.LikePlaylist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.LikePlaylist
{
    public class LikePlaylistService : ILikePlaylistService
    {
        private readonly ILikePlaylistRepository _likePlaylistRepository;

        public LikePlaylistService(ILikePlaylistRepository likePlaylistRepository)
        {
            _likePlaylistRepository = likePlaylistRepository;
        }

        public async Task<int> GetLikePlaylistCount(int playlistId)
        {
            return await _likePlaylistRepository.GetLikePlaylistCount(playlistId);
        }

        public async Task<bool> IsLikedPlaylist(int playlistId, int userId)
        {
            return await _likePlaylistRepository.IsLikedPlaylist(playlistId, userId);
        }

        public async Task<bool> ToggleUserLikePlaylistStatus(int playlistId, int userId)
        {
            return await _likePlaylistRepository.ToggleUserLikePlaylistStatus(playlistId, userId);
        }
    }
}
