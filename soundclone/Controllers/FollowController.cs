using Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services.Follow;

namespace soundclone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly IFollowService _followService;
        public FollowController(IFollowService followService) 
        {
            _followService = followService;
        }

        [HttpPost("is-following")]
        public async Task<ActionResult<bool>> IsFollowing([FromBody] FollowDTO model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                var result = await _followService.isFollowing(model);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut("toggle-follow-status")]
        [Authorize(Roles = "5")]
        public async Task<ActionResult<bool>> ToggleUserFollowStatus([FromBody] FollowDTO model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                var result = await _followService.toggleUserFollowStatus(model);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
