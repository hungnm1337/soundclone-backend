using Data.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.ListData;

namespace soundclone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListDataController : ControllerBase
    {
        private readonly IListDataService _listDataService;

        public ListDataController(IListDataService listDataService)
        {
            _listDataService = listDataService;
        }

        [HttpGet("liked-tracks")]
        public async Task<ActionResult<List<ListTrackDTO>>> GetLikedTrack(int userId)
        {
            try
            {
                return await _listDataService.GetLikedTracks(userId);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

        [HttpGet("search-tracks")]
        public async Task<ActionResult<List<ListTrackDTO>>> GetTracksBySearch(string query)
        {
            try
            {
                return await _listDataService.GetTracksBySearch(query);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("playlist-tracks")]
        public async Task<ActionResult<List<ListTrackDTO>>> GetTracksByPlaylistId(int playlistId)
        {
            try
            {
                return await _listDataService.GetTracksByPlaylistId(playlistId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("artist-tracks")]
        public async Task<ActionResult<List<ListTrackDTO>>> GetTracksByArtistId(int artistId)
        {
            try
            {
                return await _listDataService.GetTracksByArtistId(artistId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("liked-playlists")]
        public async Task<ActionResult<List<ListPlaylistDTO>>> GetLikedPlaylist(int userId)
        {
            try
            {
                return await _listDataService.GetLikedPlaylist(userId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("search-playlists")]
        public async Task<ActionResult<List<ListPlaylistDTO>>> GetPlaylistBySearch(string query)
        {
            try
            {
                return await _listDataService.GetPlaylistBySearch(query);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("artist-playlists")]
        public async Task<ActionResult<List<ListPlaylistDTO>>> GetPlaylistByArtistId(int artistId)
        {
            try
            {
                return await _listDataService.GetPlaylistByArtistId(artistId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("search-artist")]
        public async Task<ActionResult<List<ArtistDTO>>> GetArtistsBySearch(string query)
        {
            try
            {
                return await _listDataService.GetArtistsBySearch(query);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("follower")]
        public async Task<ActionResult<List<ArtistDTO>>> GetFollowers(int artistId)
        {
            try
            {
                return await _listDataService.GetFollowers(artistId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("following")]
        public async Task<ActionResult<List<ArtistDTO>>> GetFollowing(int userId)
        {
            try
            {
                return await _listDataService.GetFollowing(userId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
