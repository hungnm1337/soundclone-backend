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

        [HttpGet]
        public async Task<IActionResult> GetAllTracks()
        {
            var tracks = await _trackService.GetAllTracksAsync();
            return Ok(tracks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrackById(int id)
        {
            var track = await _trackService.GetTrackByIdAsync(id);
            if (track == null) return NotFound();
            return Ok(track);
        }

        [HttpPost]
        [Authorize(Roles = "5")]
        public async Task<IActionResult> CreateTrack([FromBody] TrackDTO trackDto)
        {
            var created = await _trackService.CreateTrackAsync(trackDto);
            return Ok(created);
        }

        [HttpPut]
        [Authorize(Roles = "5")]
        public async Task<IActionResult> UpdateTrack([FromBody] TrackDTO trackDto)
        {
            var updated = await _trackService.UpdateTrackAsync(trackDto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "5")]
        public async Task<IActionResult> DeleteTrack(int id)
        {
            var deleted = await _trackService.DeleteTrackAsync(id);
            if (!deleted) return NotFound();
            return Ok();
        }

        [HttpGet("{id}/comments")]
        public async Task<IActionResult> GetTrackCommentsDetail(int id)
        {
            var comments = await _trackService.GetTrackCommentsDetailAsync(id);
            return Ok(comments);
        }

       
    }
} 