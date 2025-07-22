using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Artist
{
    public interface IArtistService
    {
        Task<IEnumerable<ArtistDTO>> GetTop5Artist();

    }
}
