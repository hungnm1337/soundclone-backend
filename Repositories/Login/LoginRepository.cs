using Data.DTOs;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Repositories.Login
{
    public class LoginRepository : ILoginRepository
    {
        private readonly SoundcloneContext _soundcloneContext;

        public LoginRepository(SoundcloneContext soundcloneContext)
        {
            _soundcloneContext = soundcloneContext;
        }

        public async Task<UserInfoDTO?> ValidateUserAsync(LoginDTO loginDTO)
        {
            try
            {
                // Hash the password for comparison
                var hashedPassword = HashPasswordSHA256(loginDTO.Password);

                // Find user by username and password
                var user = await _soundcloneContext.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u =>
                        u.Username == loginDTO.Username &&
                        u.HashedPassword == hashedPassword &&
                        u.Status == "ACTIVE");

                if (user == null)
                    return null;

                return new UserInfoDTO
                {
                    Id = user.UserId,
                    Name = user.Name,
                    Email = user.Email,
                    Username = user.Username,
                    RoleId = user.Role?.RoleId ?? 0,
                    Avt = user.ProfilePictureUrl ?? ""
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateRefreshTokenAsync(int userId, string refreshToken, DateTime expiresAt)
        {
            try
            {
                var user = await _soundcloneContext.Users.FindAsync(userId);
                if (user == null)
                    return false;

                // For now, we'll store refresh token in a simple way
                // In a real application, you might want to create a separate table for refresh tokens
                user.UpdateAt = DateTime.Now;
                // You can add a RefreshToken field to User model if needed

                await _soundcloneContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<string?> GetRefreshTokenAsync(int userId)
        {
            try
            {
                var user = await _soundcloneContext.Users.FindAsync(userId);
                // Return the stored refresh token
                // For now, return null as we haven't implemented refresh token storage
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static string HashPasswordSHA256(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}