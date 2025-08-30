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

        public async Task<List<LikePlaylistDTO>> GetLikePlaylistOfUser(int userId)
        {
            return await _likePlaylistRepository.GetLikePlaylistOfUser(userId);
        }

        public async Task<bool> LikePlaylist(LikePlaylistDTO playlist)
        {
            return await _likePlaylistRepository.LikePlaylist(playlist);
        }

        public async Task<bool> UnlikePlaylist(int likePlaylistId)
        {
            return await _likePlaylistRepository.UnlikePlaylist(likePlaylistId);
        }
    }
}
