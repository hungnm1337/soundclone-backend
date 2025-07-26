using Data.DTOs;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Profile
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly SoundcloneContext _soundcloneContext;

        public ProfileRepository(SoundcloneContext soundcloneContext)
        {
            _soundcloneContext = soundcloneContext;
        }

        public async Task<ProfileDTO> GetProfileInformation(int userId)
        {
            try
            {
                var userInformation = await _soundcloneContext.Users.FindAsync(userId);
                if (userInformation == null) {
                    return null;
                }
                return new ProfileDTO 
                {
                    UserId = userId,
                    Bio = userInformation.Bio,
                    CreateAt = userInformation.CreateAt,
                    DayOfBirth = userInformation.DayOfBirth,
                    Email = userInformation.Email,
                    Name = userInformation.Name,
                    PhoneNumber = userInformation.PhoneNumber,
                    ProfilePictureUrl = userInformation.ProfilePictureUrl,
                    RoleId = userInformation.RoleId,
                    Status = userInformation.Status,
                    Username = userInformation.Username,
                    UpdateAt = userInformation.UpdateAt,
                    HashedPassword = userInformation.HashedPassword,
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> UpdateAvatar(UpdateAvatar model)
        {
            try
            {
                User oldUserInformation = await _soundcloneContext.Users.FindAsync(model.UserId);
                if (oldUserInformation == null)
                {
                    return false;
                }
                oldUserInformation.ProfilePictureUrl = model.ProfilePictureUrl;
                _soundcloneContext.Users.Update(oldUserInformation);
                await _soundcloneContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateProfileInformation(UpdateProfileDTO model)
        {
            try
            {
                User oldUserInformation = await _soundcloneContext.Users.FindAsync(model.UserId);
               
                if (oldUserInformation == null)
                {
                    return false;
                }
                oldUserInformation.Bio = model.Bio;
                oldUserInformation.Name = model.Name;
                oldUserInformation.Email = model.Email;
                oldUserInformation.DayOfBirth = model.DayOfBirth;
                oldUserInformation.PhoneNumber = model.PhoneNumber;
                oldUserInformation.UpdateAt = DateTime.Now;
                _soundcloneContext.Users.Update(oldUserInformation);
                await _soundcloneContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
