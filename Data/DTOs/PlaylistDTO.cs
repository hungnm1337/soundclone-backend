using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class PlaylistMenuDTO
    {
        public int PlaylistId { get; set; }
        public string Title { get; set; }
        public string? PicturePlaylistUrl { get; set; }
        public int TrackQuantity {  get; set; }
        public bool IsPublish { get; set; }

    }

    public class PlaylistDTO
    {
        public string Title { get; set; } = null!;

        public int MakeBy { get; set; }

        public string PicturePlaylistUrl { get; set; }

        public bool IsPublish {  get; set; }
    }

    public class UpdatePlaylistDTO
    {

        public int PlaylistId { get; set; }

        public int UserId {  get; set; }

        public string Title { get; set; } = null!;

        public string? PicturePlaylistUrl { get; set; }

        public bool IsPublish {  get; set; }
    }

    public class DeletePlaylistDTO
    {
        public int UserId { get; set; }

        public int PlaylistId { get; set; }
    }

    public class RemoveTrackDTO
    {
        public int UserId { get; set; }

        public int PlaylistId { get; set; }

        public int TrackId {  get; set; }
    }

    public class GetPlaylistDTO
    {
        public int PlaylistId { get; set; }

        public string Title { get; set; } = null!;

        public int MakeBy { get; set; }

        public DateTime MakeDate { get; set; }

        public string? PicturePlaylistUrl { get; set; }
    }

    public class ChangeStatusPlaylistDTO 
    {
        public int PlaylistId { get; set; }

        public int MakeBy { get; set; }
    }

    public class LikePlaylistDTO
    {
        public int? LikePlaylistId { get; set; }

        public int UserId { get; set; }

        public int PlaylistId { get; set; }
    }

    public class AddTrackToPlaylistDTO
    {
        public int PlaylistId { get; set; }
        public int TrackId { get; set; }

    }

    public class ListPlaylistDTO
    {
        public int PlaylistId { get; set; }

        public string Title { get; set; }

        public int NumTrack {  get; set; }
        public string? PicturePlaylistUrl { get; set; }
        public string ArtistName { get; set; }
    }

    public class PlayListDetailDTO
    {
        public int PlaylistId { get; set; }

        public string Title { get; set; }

        public string? PicturePlaylistUrl { get; set; }

        public int ArtistId {  get; set; }
        public string ArtistName { get; set; }

        public bool IsPublish { get; set; }

        public int TrackQuantity { get; set; }
    }
}
