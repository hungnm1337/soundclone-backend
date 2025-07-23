using Data.DTOs;
using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Track
{
    public interface ITrackRepository
    {
        Task<CreateNewTrack> CreateTrackAsync(CreateNewTrack trackDto);
        Task<IEnumerable<TrackDTO>> GetAllTracksAsync();
        Task<TrackDTO> GetTrackByIdAsync(int trackId);
        Task<TrackDTO> UpdateTrackAsync(TrackDTO trackDto);
        Task<bool> DeleteTrackAsync(int trackId);
        Task<IEnumerable<CommentDTO>> GetTrackCommentsDetailAsync(int trackId);
        Task<IEnumerable<Album>> GetAlbums();

        Task<IEnumerable<Album>> GetAlbumsByArtistId(int userId);

    }
}