using Data.DTOs;
using Repositories.ListData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ListData
{
    public class ListDataService : IListDataService
    {
        private readonly IListDataRepository _listDataRepository;
        public ListDataService(IListDataRepository listDataRepository)
        {
            _listDataRepository = listDataRepository;
        }

        public async Task<List<ArtistDTO>> GetArtistsBySearch(string query)
        {
            var artists = await _listDataRepository.GetUsers();
            var dtos = artists
                .Where(x => x.Name != null && x.Name.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
            return dtos;
        }


        public async Task<List<ArtistDTO>> GetFollowers(int artistId)
        {
            var users = await _listDataRepository.GetUsers();                  
            var follow = await _listDataRepository.GetFollow();
            var follower = follow.Where(x => x.ArtistId == artistId).Select(x => x.FollowerId).ToList();
            var dtos = users.Where(x => follower.Contains(x.UserId)).ToList();         
            return dtos;
        }

        public async Task<List<ArtistDTO>> GetFollowing(int userId)
        {
            var users = await _listDataRepository.GetUsers();
            var follow = await _listDataRepository.GetFollow();
            var following = follow.Where(x => x.FollowerId == userId).Select(x => x.ArtistId).ToList();
            var dtos = users.Where(x => following.Contains(x.UserId)).ToList();
            return dtos;
        }

        public async Task<List<ListPlaylistDTO>> GetLikedPlaylist(int userId)
        {
            var playlists = await _listDataRepository.GetPlaylist();
            var likePlaylist = await _listDataRepository.GetLikePlaylist();
            var userLikePlaylist = likePlaylist.Where(x => x.UserId == userId).Select(x => x.PlaylistId).ToList();
            var dtos = playlists.Where(x => userLikePlaylist.Contains(x.PlaylistId)).ToList();
            return dtos;
        }

        public async Task<List<ListTrackDTO>> GetLikedTracks(int userId)
        {
            var tracks = await _listDataRepository.GetTracks();
            var likeTracks = await _listDataRepository.GetLikeTracks();
            var userLikeTracks = likeTracks.Where(x => x.UserId == userId).Select(x => x.TrackId).ToList();
            var dtos = tracks.Where(x=> userLikeTracks.Contains(x.TrackId)).ToList();
            return dtos;
        }

        public async Task<List<ListPlaylistDTO>> GetPlaylistByArtistId(int artistId)
        {
            return await _listDataRepository.GetPlaylistByArtistId(artistId);
        }

        public async Task<List<ListPlaylistDTO>> GetPlaylistBySearch(string query)
        {
            var playlists = await _listDataRepository.GetPlaylist();
            var dtos = playlists
                .Where(x => x.Title != null && x.Title.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
            return dtos;
        }


        public async Task<List<ListTrackDTO>> GetTracksByArtistId(int artistId)
        {
            return await _listDataRepository.GetTracksByArtistId(artistId);
        }

        public async Task<List<ListTrackDTO>> GetTracksByPlaylistId(int playlistId)
        {
           return await  _listDataRepository.GetTracksByPlaylistId(playlistId);
        }

        public async Task<List<ListTrackDTO>> GetTracksBySearch(string query)
        {
            var tracks = await _listDataRepository.GetTracks();
            var dtos = tracks
                .Where(x => x.Title != null && x.Title.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
            return dtos;
        }

    }
}
