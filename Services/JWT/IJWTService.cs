using Data.DTOs;
using System.Security.Claims;

namespace Services.JWT
{
    public interface IJWTService
    {
        string GenerateToken(UserInfoDTO userInfo);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        bool ValidateToken(string token);
    }
} 