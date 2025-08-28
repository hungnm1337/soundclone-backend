using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ListData
{
    public interface IListDataService
    {
        Task<List<ListTrackDTO>> GetLikedTracks(int userId);
        Task<List<ListTrackDTO>> GetTracksBySearch(string query);
        Task<List<ListTrackDTO>> GetTracksByPlaylistId(int playlistId);
        Task<List<ListTrackDTO>> GetTracksByArtistId(int artistId);

        Task<List<ListPlaylistDTO>> GetLikedPlaylist(int userId);
        Task<List<ListPlaylistDTO>> GetPlaylistBySearch(string query);
        Task<List<ListPlaylistDTO>> GetPlaylistByArtistId(int artistId);

        Task<List<ArtistDTO>> GetArtistsBySearch(string query);
        Task<List<ArtistDTO>> GetFollowers(int artistId);
        Task<List<ArtistDTO>> GetFollowing(int userId);
    }
}
