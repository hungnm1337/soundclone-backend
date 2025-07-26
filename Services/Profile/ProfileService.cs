using Data.DTOs;
using Data.Models;
using Repositories.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Profile
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<ProfileDTO> GetProfileInformation(int userId)
        {
            return await _profileRepository.GetProfileInformation(userId);
           
        }

        public async Task<bool> UpdateAvatar(UpdateAvatar model)
        {
            return await _profileRepository.UpdateAvatar(model);
        }

        public async Task<bool> UpdateProfileInformation(UpdateProfileDTO model)
        {
            return await _profileRepository.UpdateProfileInformation(model);
        }
    }
}
