using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Services.JWT;

namespace soundclone.Controllers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class JWTAuthAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IJWTService _jwtService;

        public JWTAuthAttribute(IJWTService jwtService)
        {
            _jwtService = jwtService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!_jwtService.ValidateToken(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
} 