using Data.DTOs;
using Repositories.Follow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Follow
{
    public class FollowService : IFollowService
    {
        private readonly IFollowRepository _followRepository;

        public FollowService(IFollowRepository followRepository)
        {
            _followRepository = followRepository;
        }
        public async Task<bool> isFollowing(FollowDTO model)
        {
            return await _followRepository.isFollowing(model);
        }

        public async Task<bool> toggleUserFollowStatus(FollowDTO model)
        {
            return await _followRepository.toggleUserFollowStatus(model);
        }
    }
}
