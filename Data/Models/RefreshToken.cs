using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class RefreshToken
{
    public int Id { get; set; }

    public string? Token { get; set; }

    public int? UserId { get; set; }

    public DateTime? ExpiresAt { get; set; }
}
