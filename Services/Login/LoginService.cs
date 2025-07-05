using Data.DTOs;
using Repositories.Login;
using Services.JWT;

namespace Services.Login
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IJWTService _jwtService;

        public LoginService(ILoginRepository loginRepository, IJWTService jwtService)
        {
            _loginRepository = loginRepository;
            _jwtService = jwtService;
        }

        public async Task<LoginResponseDTO?> LoginAsync(LoginDTO loginDTO)
        {
            try
            {
                // Validate user credentials
                var userInfo = await _loginRepository.ValidateUserAsync(loginDTO);
                if (userInfo == null)
                    return null;

                // Generate JWT token
                var token = _jwtService.GenerateToken(userInfo);
                
                // Generate refresh token
                var refreshToken = _jwtService.GenerateRefreshToken();
                var expiresAt = DateTime.UtcNow.AddHours(1);

                // Store refresh token (optional - for token revocation)
                await _loginRepository.UpdateRefreshTokenAsync(userInfo.Id, refreshToken, expiresAt);

                return new LoginResponseDTO
                {
                    Token = token,
                    RefreshToken = refreshToken,
                    ExpiresAt = expiresAt,
                    UserInfo = userInfo
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<LoginResponseDTO?> RefreshTokenAsync(string refreshToken)
        {
            try
            {
                // In a real application, you would validate the refresh token
                // For now, we'll return null as refresh token validation is not implemented
                // You can implement this by storing refresh tokens in database and validating them
                
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
} 