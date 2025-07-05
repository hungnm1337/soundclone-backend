using Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services.Login;

namespace soundclone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                // Validate input model
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (loginDTO == null)
                {
                    return BadRequest("Login data cannot be null");
                }

                // Validate required fields
                if (string.IsNullOrWhiteSpace(loginDTO.Username) || 
                    string.IsNullOrWhiteSpace(loginDTO.Password))
                {
                    return BadRequest("Username and Password are required");
                }

                var loginResponse = await _loginService.LoginAsync(loginDTO);
                
                if (loginResponse == null)
                {
                    return Unauthorized("Invalid username or password");
                }
                
                return Ok(loginResponse);
            } 
            catch (Exception ex) 
            {
                // Log the exception here if you have logging configured
                return StatusCode(500, "An error occurred during login. Please try again later.");
            }
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<LoginResponseDTO>> RefreshToken([FromBody] string refreshToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(refreshToken))
                {
                    return BadRequest("Refresh token is required");
                }

                var loginResponse = await _loginService.RefreshTokenAsync(refreshToken);
                
                if (loginResponse == null)
                {
                    return Unauthorized("Invalid refresh token");
                }
                
                return Ok(loginResponse);
            } 
            catch (Exception ex) 
            {
                return StatusCode(500, "An error occurred while refreshing token. Please try again later.");
            }
        }

        [HttpGet("validate-token")]
        public ActionResult ValidateToken([FromHeader(Name = "Authorization")] string authorization)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(authorization) || !authorization.StartsWith("Bearer "))
                {
                    return Unauthorized("Invalid token format");
                }

                var token = authorization.Substring("Bearer ".Length);
                
                // You can inject IJWTService here to validate the token
                // For now, we'll return a simple response
                return Ok(new { message = "Token is valid" });
            } 
            catch (Exception ex) 
            {
                return StatusCode(500, "An error occurred while validating token.");
            }
        }
    }
} 