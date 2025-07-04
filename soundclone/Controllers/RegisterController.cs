using Data.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.SignUp;

namespace soundclone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly ISignUpService _signUpService;

        public RegisterController(ISignUpService signUpService)
        {
            _signUpService = signUpService;
        }

        [HttpPost]
        public async Task<ActionResult<SignUpDTO>> Register([FromBody] SignUpDTO model)
        {
            try
            {
                // Validate input model
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (model == null)
                {
                    return BadRequest("Model cannot be null");
                }

                // Validate required fields
                if (string.IsNullOrWhiteSpace(model.Email) ||
                    string.IsNullOrWhiteSpace(model.Username) ||
                    string.IsNullOrWhiteSpace(model.HashedPassword))
                {
                    return BadRequest("Email, Username, and Password are required");
                }

                var modelSignUp = await _signUpService.Register(model);

                if (modelSignUp == null)
                {
                    return BadRequest("Registration failed. Please check your information and try again.");
                }

                return Ok(modelSignUp);
            }
            catch (Exception ex)
            {
                // Log the exception here if you have logging configured
                return StatusCode(500, "An error occurred during registration. Please try again later.");
            }
        }
    }
}
