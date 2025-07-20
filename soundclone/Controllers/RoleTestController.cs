using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace soundclone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleTestController : ControllerBase
    {
        // 1. Public endpoint
        [HttpGet("public")]
        [AllowAnonymous]
        public IActionResult PublicEndpoint()
        {
            return Ok(new { message = "This is a public endpoint. Anyone can access it." });
        }

        // 2. Endpoint for role id = 5
        [HttpGet("role5")]
        [Authorize(Roles = "5")]
        public IActionResult Role5Endpoint()
        {
            return Ok(new { message = "This endpoint is only for users with role id = 5." });
        }

        // 3. Endpoint for role id = 6
        [HttpGet("role6")]
        [Authorize(Roles = "6")]
        public IActionResult Role6Endpoint()
        {
            return Ok(new { message = "This endpoint is only for users with role id = 6." });
        }
    }
} 