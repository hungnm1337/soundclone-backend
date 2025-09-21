using System;

namespace Data.DTOs
{
    public class AccountDTO
    {
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateOnly DayOfBirth { get; set; }
        public string? Bio { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string Status { get; set; } = null!;
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
    }

    public class AccountListDTO
    {
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string RoleName { get; set; } = null!;
        public DateTime CreateAt { get; set; }
    }

    public class AccountStatusUpdateDTO
    {
        public int UserId { get; set; }
        public string Status { get; set; } = null!; // "Active", "Blocked", "Suspended"
    }
}
