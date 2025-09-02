using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Follow
{
    public interface IFollowRepository
    {
        Task<bool> isFollowing(FollowDTO model);

        Task<bool> toggleUserFollowStatus(FollowDTO model);
    }
}
