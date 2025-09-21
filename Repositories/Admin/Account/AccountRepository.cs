using Data.DTOs;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.Admin.Account
{
    public class AccountRepository : IAccountRepository
    {
        private readonly SoundcloneContext _soundcloneContext;

        public AccountRepository(SoundcloneContext soundcloneContext)
        {
            _soundcloneContext = soundcloneContext;
        }

        public async Task<List<AccountListDTO>> GetAllAccountsAsync()
        {
            return await _soundcloneContext.Users
                .Include(u => u.Role)
                .Select(u => new AccountListDTO
                {
                    UserId = u.UserId,
                    Name = u.Name,
                    Email = u.Email,
                    Username = u.Username,
                    Status = u.Status,
                    RoleName = u.Role.RoleName,
                    CreateAt = u.CreateAt
                })
                .OrderByDescending(u => u.CreateAt)
                .ToListAsync();
        }

        public async Task<AccountDTO?> GetAccountByIdAsync(int userId)
        {
            return await _soundcloneContext.Users
                .Include(u => u.Role)
                .Where(u => u.UserId == userId)
                .Select(u => new AccountDTO
                {
                    UserId = u.UserId,
                    Name = u.Name,
                    Email = u.Email,
                    Username = u.Username,
                    PhoneNumber = u.PhoneNumber,
                    DayOfBirth = u.DayOfBirth,
                    Bio = u.Bio,
                    ProfilePictureUrl = u.ProfilePictureUrl,
                    CreateAt = u.CreateAt,
                    UpdateAt = u.UpdateAt,
                    Status = u.Status,
                    RoleId = u.RoleId,
                    RoleName = u.Role.RoleName
                })
                .FirstOrDefaultAsync();
        }

        private async Task<bool> UpdateAccountStatusAsync(int userId, string status)
        {
            var user = await _soundcloneContext.Users
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
                return false;

            user.Status = status;
            user.UpdateAt = DateTime.UtcNow;

            try
            {
                await _soundcloneContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> BlockAccountAsync(int userId)
        {
            return await UpdateAccountStatusAsync(userId, "BLOCKED");
        }

        public async Task<bool> UnblockAccountAsync(int userId)
        {
            return await UpdateAccountStatusAsync(userId, "ACTIVE");
        }

        public async Task<List<AccountListDTO>> SearchAccountsAsync(string searchTerm)
        {
            var searchLower = searchTerm.ToLower();
            
            return await _soundcloneContext.Users
                .Include(u => u.Role)
                .Where(u => u.Name.ToLower().Contains(searchLower) || 
                           u.Email.ToLower().Contains(searchLower) ||
                           u.Username.ToLower().Contains(searchLower))
                .Select(u => new AccountListDTO
                {
                    UserId = u.UserId,
                    Name = u.Name,
                    Email = u.Email,
                    Username = u.Username,
                    Status = u.Status,
                    RoleName = u.Role.RoleName,
                    CreateAt = u.CreateAt
                })
                .OrderByDescending(u => u.CreateAt)
                .ToListAsync();
        }

        public async Task<List<RoleDTO>> GetRoles()
        {
            var roles = await _soundcloneContext.Roles
                .Select(x => new RoleDTO()
                {
                    RoleDescription = x.RoleDescription,
                    RoleId = x.RoleId,
                    RoleName = x.RoleName
                }).ToListAsync();
            return roles;
        }

        public async Task<bool> ChangeRoleOfUser(int UserId, int NewRoleId)
        {
            try
            {
                var user = await _soundcloneContext.Users.FindAsync(UserId);
                if (user == null)
                {
                    return false;
                }
                user.RoleId = NewRoleId;
                _soundcloneContext.Users.Update(user);
                await _soundcloneContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
