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
    }
}
