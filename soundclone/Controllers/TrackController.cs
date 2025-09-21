using Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Track;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace soundclone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        private readonly ITrackService _trackService;
        public TrackController(ITrackService trackService)
        {
            _trackService = trackService;
        }

        [HttpGet("get-tracks")]
        public async Task<IActionResult> GetAllTracks()
        {
            var tracks = await _trackService.GetAllTracksAsync();
            return Ok(tracks);
        }

        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetTrackById(int id)
        {
            var track = await _trackService.GetTrackByIdAsync(id);
            if (track == null) return NotFound();
            return Ok(track);
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<CreateNewTrack>> CreateTrack([FromBody] CreateNewTrack trackDto)
        {
            var created = await _trackService.CreateTrackAsync(trackDto);
            return Ok(created);
        }

        [HttpPut("update-track")]
        [Authorize]
        public async Task<IActionResult> UpdateTrack([FromBody] TrackDTO trackDto)
        {
            var updated = await _trackService.UpdateTrackAsync(trackDto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("delete-track/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteTrack(int id)
        {
            var deleted = await _trackService.DeleteTrackAsync(id);
            if (!deleted) return NotFound();
            return Ok();
        }

        [HttpGet("comments/{trackId}")]
        public async Task<IActionResult> GetTrackCommentsDetail(int trackId)
        {
            var comments = await _trackService.GetTrackCommentsDetailAsync(trackId);
            return Ok(comments);
        }

        [HttpGet("albums")]
        public async Task<IActionResult> GetAlbum()
        {
            try
            {
                var albums = await _trackService.GetAlbums();
                return Ok(albums);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

        [HttpGet("top-albums")]
        public async Task<IActionResult> GetTop5Album()
        {
            try
            {
                var albums = await _trackService.GetTop5Albums();
                return Ok(albums);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("albums/{userId}")]
        public async Task<IActionResult> GetAlbumsByArtistId(int userId)
        {
            try
            {
                var albums = await _trackService.GetAlbumsByArtistId(userId);
                return Ok(albums);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
} 