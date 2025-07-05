using Data.DTOs;

namespace Services.Login
{
    public interface ILoginService
    {
        Task<LoginResponseDTO?> LoginAsync(LoginDTO loginDTO);
        Task<LoginResponseDTO?> RefreshTokenAsync(string refreshToken);
    }
} 