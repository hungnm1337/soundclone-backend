using Data.DTOs;
using Repositories.Playlist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Playlist
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IPlaylistRepository _playlistRepository;
        public PlaylistService(IPlaylistRepository playlistRepository)
        {

            _playlistRepository = playlistRepository;
        }

        public async Task<bool> AddTrackToPlaylist(AddTrackToPlaylistDTO model)
        {
            return await _playlistRepository.AddTrackToPlaylist(model);
        }

        public async Task<bool> ChangeStatusPublicOfPlaylist(ChangeStatusPlaylistDTO model)
        {
            return await _playlistRepository.ChangeStatusPublicOfPlaylist(model);
        }

        public async Task<PlaylistDTO> CreateNewPlaylist(PlaylistDTO playlist)
        {
            return await _playlistRepository.CreateNewPlaylist(playlist);
        }

        public async Task<bool> DeletePlaylist(DeletePlaylistDTO model)
        {
            return await _playlistRepository.DeletePlaylist(model);
        }

        public async Task<IEnumerable<Data.Models.Playlist>> GetPlaylistByUserId(int userId)
        {
            return await _playlistRepository.GetPlaylistByUserId(userId);
        }

        public async Task<PlayListDetailDTO> GetPlayListDetail(int playlistId)
        {
            return await _playlistRepository.GetPlayListDetail(playlistId);
        }

        public async Task<IEnumerable<PlaylistMenuDTO>> GetPlaylistMenu(int userId)
        {
            return await _playlistRepository.GetPlaylistMenu(userId);
        }

        public async Task<UpdatePlaylistDTO> UpdatePlaylist(UpdatePlaylistDTO playlist)
        {
            return await _playlistRepository.UpdatePlaylist(playlist);
        }
    }
}
