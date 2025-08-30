using Data.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.LikeTrack;

namespace soundclone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeTrackController : ControllerBase
    {
        private readonly ILikeTrackService _likeTrackService;
        public LikeTrackController(ILikeTrackService likeTrackService)
        {
            _likeTrackService = likeTrackService;
        }

        [HttpPost("isLiked")]
        public async Task<ActionResult<bool>> IsTrackLiked([FromBody] LikeTrackInput input)
        {
            try
            {
                return await _likeTrackService.isLikedTrack(input.TrackId, input.UserId);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("toggleStatus")]
        public async Task<ActionResult<bool>> ToggleUserLikeTrackStatus([FromBody] LikeTrackInput input)
        {
            try
            {
                return await _likeTrackService.toggleUserLikeTrackStatus(input.TrackId, input.UserId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("like-track-count/{trackId:int}")]
        public async Task<ActionResult<int>> GetLikeTrackCount(int trackId)
        {
            try
            {
                int likeTrackCount = await _likeTrackService.GetLikeTrackCount(trackId);
                return Ok(likeTrackCount);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
