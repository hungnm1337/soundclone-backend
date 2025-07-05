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
        public async Task<ActionResult> Register([FromBody] SignUpDTO model)
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

                bool isEmailExisted = await _signUpService.checkEmail(model.Email);
                if (isEmailExisted) 
                {
                    return BadRequest("Email existed, try another email.");
                }
                bool isUsernameExisted = await _signUpService.checkUsername(model.Username);
                if (isUsernameExisted)
                {
                    return BadRequest("Username existed, try another username.");
                }
                bool isPhonenumberExisted = await _signUpService.checkPhoneNumber(model.PhoneNumber);
                if (isPhonenumberExisted)
                {
                    return BadRequest("Phone number existed, try anothe phone number.");
                }


                var modelSignUp = await _signUpService.Register(model);

                if (modelSignUp == null)
                {
                    return BadRequest("Registration failed. Please check your information and try again.");
                }

                return Ok("Register successfully");
            }
            catch (Exception ex)
            {
                // Log the exception here if you have logging configured
                return StatusCode(500, "An error occurred during registration. Please try again later.");
            }
        }
    }
}
