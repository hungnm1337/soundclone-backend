using System;
using System.Collections.Generic;

namespace Data.DTOs
{
    public class TrackDTO
    {
        public int? TrackId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AudioFileUrl { get; set; }
        public string CoverArtUrl { get; set; }
        public string WaveformUrl { get; set; }
        public int DurationInSeconds { get; set; }
        public bool IsPublic { get; set; }
        public DateTime UploadDate { get; set; }
        public int UpdateBy { get; set; }
        public int PlayCount { get; set; }
    }

    public class CreateNewTrack
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string AudioFileUrl { get; set; }
        public string CoverArtUrl { get; set; }
        public int? DurationInSeconds { get; set; } = 0;
        public bool IsPublic { get; set; }
        public int UpdateBy { get; set; }
    }

    public class Album
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Artist { get; set; }

        public int Year { get; set; }

        public int View { get; set; }

        public string ImageUrl { get; set; }
    }
} 