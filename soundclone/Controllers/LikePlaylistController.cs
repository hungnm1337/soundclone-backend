using Data.DTOs;
using Data.Models;
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

        [HttpGet("like-playlist/{Userid:int}")]
        public async Task<ActionResult<IEnumerable<LikePlaylistDTO>>> GetLikePlaylistOfUser(int Userid)
        {
            try
            {
                var like_playlists = await _likePlaylistService.GetLikePlaylistOfUser(Userid);
                if (like_playlists == null)
                {
                    return NotFound();
                }
                return Ok(like_playlists);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("like-playlist")]
        public async Task<IActionResult> LikePlaylist([FromBody] LikePlaylistDTO model)
        {
            try
            {
                bool resultOfCreate = await _likePlaylistService.LikePlaylist(model);
                if (resultOfCreate)
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("unlike-playlist/{likePlaylistId:int}")]
        public async Task<IActionResult> UnlikePlaylist(int likePlaylistId)
        {
            try
            {
                bool resultOfUnlike = await _likePlaylistService.UnlikePlaylist(likePlaylistId);
                if (resultOfUnlike)
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
