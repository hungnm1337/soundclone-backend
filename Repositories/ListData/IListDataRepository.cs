using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ListData
{
    public interface IListDataRepository
    {
        Task<List<ListTrackDTO>> GetTracks();
       
        Task<List<ListPlaylistDTO>> GetPlaylist();

        Task<List<ArtistDTO>> GetUsers();

        Task<List<FollowDTO>> GetFollow();

        Task<List<LikePlaylistDTO>> GetLikePlaylist();

        Task<List<LikeTrackDTO>> GetLikeTracks();

        Task<List<ListTrackDTO>> GetTracksByArtistId(int artistId);

        Task<List<ListPlaylistDTO>> GetPlaylistByArtistId(int artistId);

        Task<List<ListTrackDTO>> GetTracksByPlaylistId(int playlistId);

    }
}
