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
                ProfilePictureUrl = artist.ProfilePictureUrl,
                Bio = artist.Bio,
                DayOfBirth = artist.DayOfBirth,
                Email = artist.Email,
                PhoneNumber = artist.PhoneNumber,

            };
        }

        public async Task<IEnumerable<ArtistDTO>> GetTop5Artist()
        {
            var ids = await _soundcloneContext.Users
                .Where(u => u.Status == "ACTIVE")
                .Select(u => u.UserId)
                .ToListAsync();

            var random = new Random();
            var randomIds = ids.OrderBy(x => random.Next()).Take(5).ToList();

            var artists = await _soundcloneContext.Users
                .Where(u => randomIds.Contains(u.UserId))
                .Select(u => new ArtistDTO
                {
                    Name = u.Name,
                    ProfilePictureUrl = u.ProfilePictureUrl,
                    UserId = u.UserId
                }).ToListAsync();

            return artists;
        }

    }
}
