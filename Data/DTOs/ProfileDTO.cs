using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class ProfileDTO
    {
        public int UserId { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateOnly DayOfBirth { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public string? Bio { get; set; }

        public string? ProfilePictureUrl { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime UpdateAt { get; set; }

        public string Username { get; set; } = null!;

        public string HashedPassword { get; set; } = null!;

        public string Status { get; set; } = null!;

        public int RoleId { get; set; }
    }

    public class UpdateProfileDTO
    {
        public int UserId { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateOnly DayOfBirth { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public string? Bio { get; set; }

    }

    public class UpdateAvatar
    {
        public int UserId { get; set; }

        public string? ProfilePictureUrl { get; set; }
    }

}
