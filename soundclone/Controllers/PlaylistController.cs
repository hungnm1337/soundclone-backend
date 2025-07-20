using Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Playlist;

namespace soundclone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly IPlaylistService _playlistService;
        public PlaylistController(IPlaylistService playlistService)
        {
            _playlistService = playlistService;
        }
        [HttpGet("getplaylist/{userId:int}")]
        [Authorize(Roles = "5")]
        public async Task<IActionResult> GetPlaylistsByUserId(int userId)
        {
            try
            {
                var userPlaylists = await _playlistService.GetPlaylistByUserId(userId);

                if (userPlaylists == null || !userPlaylists.Any())
                {
                    return NotFound("Không tìm thấy playlist nào cho người dùng này.");
                }

                return Ok(userPlaylists);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã xảy ra lỗi trong quá trình xử lý.");
            }
        }

        [HttpPost("create")]
        [Authorize(Roles = "5")]
        public async Task<IActionResult> CreateNewPlaylist([FromBody] PlaylistDTO playlist)
        {
            try
            {
                var resultCreate = await _playlistService.CreateNewPlaylist(playlist);
                if (resultCreate == null)
                {
                    return BadRequest(resultCreate);
                }
                return Ok(resultCreate);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("update-playlist")]
        [Authorize(Roles = "5")]
        public async Task<IActionResult> UpdatePlaylist([FromBody] UpdatePlaylistDTO playlist)
        {
            try
            {
                var resultUpdate = await _playlistService.UpdatePlaylist(playlist);
                if (resultUpdate == null)
                {
                    return BadRequest(resultUpdate);
                }
                return Ok(resultUpdate);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("status-change")]
        [Authorize(Roles = "5")]
        public async Task<IActionResult> ChangeStatusPublishOfPlaylist([FromBody] ChangeStatusPlaylistDTO model)
        {
            try
            {
                bool resultOfChange = await _playlistService.ChangeStatusPublicOfPlaylist(model);
                if (resultOfChange)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
