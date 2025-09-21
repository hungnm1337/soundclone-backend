using Data.DTOs;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.Track
{
    public class TrackRepository : ITrackRepository
    {
        private readonly SoundcloneContext _context;
        public TrackRepository(SoundcloneContext context)
        {
            _context = context;
        }

        public async Task<CreateNewTrack> CreateTrackAsync(CreateNewTrack trackDto)
        {
            try
            {
                var track = new Data.Models.Track
                {
                    Title = trackDto.Title,
                    Description = trackDto.Description,
                    AudioFileUrl = trackDto.AudioFileUrl,
                    CoverArtUrl = trackDto.CoverArtUrl,
                    WaveformUrl = "",
                    DurationInSeconds = (int)trackDto.DurationInSeconds,
                    IsPublic = trackDto.IsPublic,
                    UploadDate = DateTime.Now,
                    UpdateBy = trackDto.UpdateBy,
                    PlayCount = 0
                };
                _context.Tracks.Add(track);
                await _context.SaveChangesAsync();
                return trackDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<TrackDTO>> GetAllTracksAsync()
        {
            return await _context.Tracks.Select(t => new TrackDTO
            {
                TrackId = t.TrackId,
                Title = t.Title,
                Description = t.Description,
                AudioFileUrl = t.AudioFileUrl,
                CoverArtUrl = t.CoverArtUrl,
                WaveformUrl = t.WaveformUrl,
                DurationInSeconds = t.DurationInSeconds,
                IsPublic = t.IsPublic,
                UploadDate = t.UploadDate,
                UpdateBy = t.UpdateBy,
                PlayCount = t.PlayCount
            }).ToListAsync();
        }

        public async Task<TrackDTO> GetTrackByIdAsync(int trackId)
        {
            var t = await _context.Tracks.FindAsync(trackId);

            if (t == null) return null;
            t.PlayCount++;
            _context.Tracks.Update(t);
            await _context.SaveChangesAsync();
            return new TrackDTO
            {
                TrackId = t.TrackId,
                Title = t.Title,
                Description = t.Description,
                AudioFileUrl = t.AudioFileUrl,
                CoverArtUrl = t.CoverArtUrl,
                WaveformUrl = t.WaveformUrl,
                DurationInSeconds = t.DurationInSeconds,
                IsPublic = t.IsPublic,
                UploadDate = t.UploadDate,
                UpdateBy = t.UpdateBy,
                PlayCount = t.PlayCount
            };
        }

        public async Task<TrackDTO> UpdateTrackAsync(TrackDTO trackDto)
        {
            var t = await _context.Tracks.FindAsync(trackDto.TrackId);
            if (t == null) return null;
            t.Title = trackDto.Title;
            t.Description = trackDto.Description;
            t.AudioFileUrl = trackDto.AudioFileUrl;
            t.CoverArtUrl = trackDto.CoverArtUrl;
            t.WaveformUrl = trackDto.WaveformUrl;
            t.DurationInSeconds = trackDto.DurationInSeconds;
            t.IsPublic = trackDto.IsPublic;
            t.UploadDate = trackDto.UploadDate;
            t.UpdateBy = trackDto.UpdateBy;
            t.PlayCount = trackDto.PlayCount;
            _context.Tracks.Update(t);
            await _context.SaveChangesAsync();
            return trackDto;
        }

        public async Task<bool> DeleteTrackAsync(int trackId)
        {
            var t = await _context.Tracks.FindAsync(trackId);
            if (t == null) return false;
            _context.Tracks.Remove(t);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CommentDTO>> GetTrackCommentsDetailAsync(int trackId)
        {
            var comments = await _context.Comments
                .Where(c => c.TrackId == trackId)
                .Include(c => c.WriteByNavigation)
                .Select(c => new CommentDTO
                {
                    CommentId = c.CommentId,
                    WriteBy = c.WriteBy,
                    WriteDate = c.WriteDate,
                    TrackId = c.TrackId,
                    ParentCommentId = c.ParentCommentId,
                    Content = c.Content
                }).ToListAsync();
            return comments;
        }

        public async Task<IEnumerable<Album>> GetAlbums()
        {
            try
            {
                var listAlbum = await (
                  from track in _context.Tracks
                  join user in _context.Users on track.UpdateBy equals user.UserId into userGroup
                  from artist in userGroup.DefaultIfEmpty()
                  where track.IsPublic
                  orderby Guid.NewGuid()
                  select new Album
                  {
                      Id = track.TrackId,
                      Title = track.Title,
                      Artist = artist != null ? artist.Name : "No One",
                      ImageUrl = track.CoverArtUrl,
                      View = track.PlayCount,
                      Year = track.UploadDate.Date.ToString("yyyy MMM dd")
                  }
                ).Take(10).ToListAsync();

                return listAlbum;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Album>> GetAlbumsByArtistId(int userId)
        {
            try
            {
                var listAlbum = await (
                  from track in _context.Tracks
                  join user in _context.Users on track.UpdateBy equals user.UserId into userGroup
                  from artist in userGroup.DefaultIfEmpty()
                  where track.IsPublic && artist.UserId == userId
                  orderby Guid.NewGuid()
                  select new Album
                  {
                      Id = track.TrackId,
                      Title = track.Title,
                      Artist = artist != null ? artist.Name : "No One",
                      ImageUrl = track.CoverArtUrl,
                      View = track.PlayCount,
                      Year = track.UploadDate.Date.ToString("yyyy MMM dd")
                  }
                ).Take(10).ToListAsync();

                return listAlbum;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Album>> GetTop5Albums()
        {
            try
            {
                var listAlbum = await(
                  from track in _context.Tracks
                  join user in _context.Users on track.UpdateBy equals user.UserId into userGroup
                  from artist in userGroup.DefaultIfEmpty()
                  where track.IsPublic
                  orderby track.PlayCount descending
                  select new Album
                  {
                      Id = track.TrackId,
                      Title = track.Title,
                      Artist = artist != null ? artist.Name : "No One",
                      ImageUrl = track.CoverArtUrl,
                      View = track.PlayCount,
                      Year = track.UploadDate.Date.ToString("yyyy MMM dd")
                  }
                ).Take(5).ToListAsync();

                return listAlbum;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}