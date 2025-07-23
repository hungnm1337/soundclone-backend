using Data.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Track
{
    public interface ITrackService
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