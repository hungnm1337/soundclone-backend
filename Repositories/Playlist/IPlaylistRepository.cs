using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Playlist
{
    public interface IPlaylistRepository
    {
        Task<IEnumerable<Data.Models.Playlist>> GetPlaylistByUserId(int userId);

        Task<PlaylistDTO> CreateNewPlaylist(PlaylistDTO playlist);

        Task<UpdatePlaylistDTO> UpdatePlaylist(UpdatePlaylistDTO playlist);

        Task<bool> ChangeStatusPublicOfPlaylist(ChangeStatusPlaylistDTO model);

        Task<IEnumerable<PlaylistMenuDTO>> GetPlaylistMenu(int userId);

        Task<bool> AddTrackToPlaylist(AddTrackToPlaylistDTO model);

        Task<bool> DeletePlaylist(DeletePlaylistDTO model);

        Task<bool> RemoveTrackOfPlaylist(RemoveTrackDTO model);

        Task<PlayListDetailDTO> GetPlayListDetail(int playlistId);
    }
}
