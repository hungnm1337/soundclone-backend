using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Follow
{
    public int Id { get; set; }

    public int ArtistId { get; set; }

    public int FollowerId { get; set; }

    public virtual User Artist { get; set; } = null!;

    public virtual User Follower { get; set; } = null!;
}
