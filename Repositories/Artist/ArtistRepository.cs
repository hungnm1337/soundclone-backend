using Data.DTOs;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Artist
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly SoundcloneContext _soundcloneContext;

        public ArtistRepository(SoundcloneContext soundcloneContext)
        {
            _soundcloneContext = soundcloneContext;
        }

        public async Task<ArtistDetailDTO> GetArtistDetail(int UserId)
        {
            int FollowingQuantity = _soundcloneContext.Follows.
                Where(x => x.FollowerId == UserId).Count();
            int FollowerQuantity = _soundcloneContext.Follows.
                Where(x => x.ArtistId == UserId).Count();
            var artist = await _soundcloneContext.Users.FindAsync(UserId);
            return new ArtistDetailDTO
            {
                UserId = artist.UserId,
                FollowerQuantity = FollowerQuantity,
                FollowingQuantity = FollowingQuantity,
                Name = artist.Name,
                ProfilePictureUrl = artist.ProfilePictureUrl
            };
        }

        public async Task<IEnumerable<ArtistDTO>> GetTop5Artist()
        {
            var artists = await (from user  in _soundcloneContext.Users
                                 where user.Status.Equals("ACTIVE")
                                 orderby new Guid()
                                 select new ArtistDTO
                                 {
                                     Name = user.Name,
                                     ProfilePictureUrl = user.ProfilePictureUrl,
                                     UserId = user.UserId
                                 }
                                 ).Take( 5 ).ToListAsync();
            return artists;
        }
    }
}
