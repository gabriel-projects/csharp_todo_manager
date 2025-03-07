using Api.GRRInnovations.TodoManager.Interfaces.Authentication;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Api.GRRInnovations.TodoManager.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public AuthController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpGet("signin-google")]
        public IActionResult LoginGoogle()
        {
            var redirectUrl = Url.Action(nameof(GoogleResponse), "Auth", null, Request.Scheme, Request.Host.Value);

            return Challenge(new AuthenticationProperties { RedirectUri = redirectUrl }, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!authenticateResult.Succeeded)
                return BadRequest("Falha ao autenticar com Google.");

            var claims = authenticateResult.Principal.Identities.FirstOrDefault()?.Claims
                .Select(c => new { c.Type, c.Value });

            return Ok(claims);
        }

        //todo: ajustar
        [HttpGet("signin-github")]
        public IActionResult LoginGitHub()
        {
            var redirectUrl = Url.Action(nameof(GoogleResponse), "Auth", null, Request.Scheme, Request.Host.Value);

            return Challenge(new AuthenticationProperties { RedirectUri = redirectUrl }, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("github-response")]
        public async Task<IActionResult> GitHubResponse()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!authenticateResult.Succeeded)
                return BadRequest("Falha ao autenticar com Google.");

            var claims = authenticateResult.Principal.Identities.FirstOrDefault()?.Claims
                .Select(c => new { c.Type, c.Value });

            return Ok(claims);
        }

    }
}
