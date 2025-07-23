using Data.DTOs;
using Repositories.Artist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Artist
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistService(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public async Task<ArtistDetailDTO> GetArtistDetail(int UserId)
        {
            return await _artistRepository.GetArtistDetail(UserId);
        }

        public async Task<IEnumerable<ArtistDTO>> GetTop5Artist()
        {
            return await _artistRepository.GetTop5Artist();
        }
    }
}
