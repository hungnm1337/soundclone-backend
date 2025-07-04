using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class SignUpDTO
    {
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateOnly DayOfBirth { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string HashedPassword { get; set; } = null!;
    }
}
