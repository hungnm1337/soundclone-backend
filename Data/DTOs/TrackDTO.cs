using System;
using System.Collections.Generic;

namespace Data.DTOs
{
    public class TrackDTO
    {
        public int TrackId { get; set; }
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
} 