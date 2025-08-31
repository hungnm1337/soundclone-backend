using Data.DTOs;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.LikePlaylist;

namespace soundclone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikePlaylistController : ControllerBase
    {
        private readonly ILikePlaylistService _likePlaylistService;

        public LikePlaylistController(ILikePlaylistService likePlaylistService)
        {
            _likePlaylistService = likePlaylistService;
        }

        [HttpPost("isLiked")]

        public async Task<ActionResult<bool>> IsTrackLiked([FromBody] LikePlaylistInput input)
        {
            try
            {
                return await _likePlaylistService.IsLikedPlaylist(input.PlaylistId, input.UserId);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("toggleStatus")]
        [Authorize(Roles = "5")]

        public async Task<ActionResult<bool>> ToggleUserLikePlaylistStatus([FromBody] LikePlaylistInput input)
        {
            try
            {
                return await _likePlaylistService.ToggleUserLikePlaylistStatus(input.PlaylistId, input.UserId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("like-playlist-count/{playlistId:int}")]
        public async Task<ActionResult<int>> GetLikePlaylistCount(int playlistId)
        {
            try
            {
                int likePlaylistCount = await _likePlaylistService.GetLikePlaylistCount(playlistId);
                return likePlaylistCount;
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
