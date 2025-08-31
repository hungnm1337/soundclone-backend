using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Artist;

namespace soundclone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _artistService;
        public ArtistController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet("get-artist")]
        public async Task<IActionResult> GetTop5Artist()
        {
            var artists = await _artistService.GetTop5Artist();
            if (artists == null)
            {
                return NotFound();
            }
            return Ok(artists);
        }

        [HttpGet("get-artist-detail/{userId:int}")]
        public async Task<IActionResult> GetArtistDetail(int userId)
        {
            var artistDetail = await _artistService.GetArtistDetail(userId);
            if (artistDetail == null)
                return NotFound();
            return Ok(artistDetail);
        }
    }
}
