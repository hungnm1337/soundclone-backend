using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Artist
{
    public interface IArtistRepository
    {
        Task<IEnumerable<ArtistDTO>> GetTop5Artist();

        Task<ArtistDetailDTO> GetArtistDetail(int UserId);
    }
}
