using Microsoft.AspNetCore.Http;

namespace Data.DTOs
{
    public class TrackUploadDTO
    {
        public IFormFile File { get; set; }
    }
} 