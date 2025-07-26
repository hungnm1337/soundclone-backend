using Data.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Profile;

namespace soundclone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;
        public ProfileController(IProfileService profileService) 
        {
            _profileService = profileService;
        }

        [HttpGet("user-information/{userId:int}")]
        public async Task<ActionResult<ProfileDTO>> GetProfileInformation(int userId)
        {
            try
            {
                var profileInformation = await _profileService.GetProfileInformation(userId);
                if (profileInformation == null)
                {
                    return NotFound();
                }
                return Ok(profileInformation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }      
        }

        [HttpPut("update-avatar")]
        public async Task<ActionResult<bool>> UpdateAvatar([FromBody] UpdateAvatar model)
        {
            try
            {
                bool updated = await _profileService.UpdateAvatar(model);
                if (updated)
                {
                    return Ok(updated);
                }
                else
                {
                    return BadRequest(updated);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("update-profile")]
        public async Task<ActionResult<bool>> UpdateProfileInformation([FromBody] UpdateProfileDTO model)
        {
            try
            {
                bool updated = await _profileService.UpdateProfileInformation(model);
                if (updated)
                {
                    return Ok(updated);
                }
                else
                {
                    return BadRequest(updated);
                }

            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
