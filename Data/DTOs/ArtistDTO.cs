﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class ArtistDTO
    {
        public int UserId { get; set; }

        public string Name { get; set; } = null!;

        public string? ProfilePictureUrl { get; set; }
    }
}
