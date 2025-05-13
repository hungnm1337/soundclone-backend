using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Follow
{
    public int Id { get; set; }

    public int FollowerId { get; set; }

    public int FollowedId { get; set; }

    public DateOnly FollowedAt { get; set; }

    public virtual User Followed { get; set; } = null!;

    public virtual User Follower { get; set; } = null!;
}
