using Data.DTOs;

namespace Repositories.Login
{
    public interface ILoginRepository
    {
        Task<UserInfoDTO?> ValidateUserAsync(LoginDTO loginDTO);
        Task<bool> UpdateRefreshTokenAsync(int userId, string refreshToken, DateTime expiresAt);
        Task<string?> GetRefreshTokenAsync(int userId);
    }
} 