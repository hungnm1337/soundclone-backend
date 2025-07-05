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

        // 2. Endpoint for role id = 1
        [HttpGet("role1")]
        [Authorize(Roles = "1")]
        public IActionResult Role1Endpoint()
        {
            return Ok(new { message = "This endpoint is only for users with role id = 1." });
        }

        // 3. Endpoint for role id = 2
        [HttpGet("role2")]
        [Authorize(Roles = "2")]
        public IActionResult Role2Endpoint()
        {
            return Ok(new { message = "This endpoint is only for users with role id = 2." });
        }
    }
} 