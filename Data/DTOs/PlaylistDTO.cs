using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class PlaylistDTO
    {
        public string Title { get; set; } = null!;

        public int MakeBy { get; set; }

        public string? PicturePlaylistUrl { get; set; }
    }

    public class UpdatePlaylistDTO
    {
        public int PlaylistId { get; set; }

        public string Title { get; set; } = null!;



        public string? PicturePlaylistUrl { get; set; }
    }

    public class GetPlaylistDTO
    {
        public int PlaylistId { get; set; }

        public string Title { get; set; } = null!;

        public int MakeBy { get; set; }

        public DateTime MakeDate { get; set; }

        public string? PicturePlaylistUrl { get; set; }
    }

}
