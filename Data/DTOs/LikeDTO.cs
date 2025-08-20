using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class PlaylistLikedDTO
    {
        public int PlaylistId { get; set; }

        public string Title { get; set; } = null!;

        public string? PicturePlaylistUrl { get; set; }

        public ArtistDTO Artist { get; set; }
    }
    public class TrackLikedDTO
    {
        public int TrackId { get; set; }

        public string Title { get; set; } = null!;

        public string CoverArtUrl { get; set; } = null!;

        public ArtistDTO Artist { get; set; }
    }

    public class LikeTrackInput
    {
        public int TrackId { get; set; }

        public int UserId { get; set; }
    }

}
