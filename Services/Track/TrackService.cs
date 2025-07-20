using Data.DTOs;
using Repositories.Track;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Track
{
    public class TrackService : ITrackService
    {
        private readonly ITrackRepository _trackRepository;
        public TrackService(ITrackRepository trackRepository)
        {
            _trackRepository = trackRepository;
        }

        public async Task<TrackDTO> CreateTrackAsync(TrackDTO trackDto)
        {
            return await _trackRepository.CreateTrackAsync(trackDto);
        }

        public async Task<IEnumerable<TrackDTO>> GetAllTracksAsync()
        {
            return await _trackRepository.GetAllTracksAsync();
        }

        public async Task<TrackDTO> GetTrackByIdAsync(int trackId)
        {
            return await _trackRepository.GetTrackByIdAsync(trackId);
        }

        public async Task<TrackDTO> UpdateTrackAsync(TrackDTO trackDto)
        {
            return await _trackRepository.UpdateTrackAsync(trackDto);
        }

        public async Task<bool> DeleteTrackAsync(int trackId)
        {
            return await _trackRepository.DeleteTrackAsync(trackId);
        }

        public async Task<IEnumerable<CommentDTO>> GetTrackCommentsDetailAsync(int trackId)
        {
            return await _trackRepository.GetTrackCommentsDetailAsync(trackId);
        }
    }
}