using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Profile
{
    public interface IProfileService
    {
        Task<ProfileDTO> GetProfileInformation(int userId);

        Task<bool> UpdateProfileInformation(UpdateProfileDTO model);

        Task<bool> UpdateAvatar(UpdateAvatar model);
    }
}
