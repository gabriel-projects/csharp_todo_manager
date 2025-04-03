using System.Security.Claims;
using Api.GRRInnovations.TodoManager.Application.Interfaces;
using Api.GRRInnovations.TodoManager.Domain.Models;
using Api.GRRInnovations.TodoManager.Domain.ValueObjects;
using Api.GRRInnovations.TodoManager.Domain.Wrappers.In;
using Api.GRRInnovations.TodoManager.Domain.Wrappers.Out;
using Api.GRRInnovations.TodoManager.Infrastructure.Interfaces;
using Api.GRRInnovations.TodoManager.Infrastructure.Security.Authentication;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Api.GRRInnovations.TodoManager.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly IAuthManager _authManager;
        private readonly IUserService _userService;
        private readonly IUserClaimsMapper _userClaimsMapper;

        public AuthController(IJwtService jwtService, ILogger<AuthController> logger, IAuthManager authManager, IUserService userService, IUserClaimsMapper userClaimsMapper)
        {
            _jwtService = jwtService;
            _authManager = authManager;
            _userService = userService;
            _userClaimsMapper = userClaimsMapper;
        }

        [HttpGet("signin-google")]
        public IActionResult LoginGoogle()
        {
            return _authManager.HandleLogin("Google", nameof(GoogleResponse));
        }

        [HttpGet("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!result.Succeeded) return BadRequest("Falha ao autenticar com GitHub.");

            return await HandleExternalLoginAsync(result.Principal);
        }

        [HttpGet("signin-github")]
        public IActionResult LoginGitHub()
        {
            return _authManager.HandleLogin("GitHub", nameof(GitHubResponse));
        }

        [HttpGet("github-response")]
        public async Task<IActionResult> GitHubResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!result.Succeeded) return BadRequest("Falha ao autenticar com GitHub.");

            return await HandleExternalLoginAsync(result.Principal);
        }

        [HttpPost]
        public async Task<ActionResult<WrapperOutJwtResult>> SignInWithCredentials([FromBody] WrapperInLogin wrapperInLogin)
        {
            if (string.IsNullOrEmpty(wrapperInLogin.Login) || string.IsNullOrEmpty(wrapperInLogin.Password)) return new BadRequestObjectResult(new WrapperOutError("Dados inválidos."));

            var remoteUser = await _userService.ValidateAsync(wrapperInLogin.Login, wrapperInLogin.Password).ConfigureAwait(false);
            if (remoteUser == null) return new UnauthorizedResult();

            var userToken = _jwtService.GenerateToken(remoteUser);

            var response = await WrapperOutJwtResult.From(userToken).ConfigureAwait(false);
            return new OkObjectResult(response);
        }

        private async Task<IActionResult> GenerateTokenResponse(IUserModel? user)
        {
            var userToken = _jwtService.GenerateToken(user);

            var response = await WrapperOutJwtResult.From(userToken).ConfigureAwait(false);
            return new OkObjectResult(response);
        }

        private async Task<IActionResult> HandleExternalLoginAsync(ClaimsPrincipal principal)
        {
            var userClaims = _userClaimsMapper.MapFromClaimsPrincipal(principal);
            if (string.IsNullOrEmpty(userClaims.Email))
                return BadRequest("Email não encontrado.");

            var options = UserOptions.Create().WithLogins(new List<string> { userClaims.Email }).Build();
            var existingUser = await _userService.GetAllAsync(options);

            if (existingUser?.Any() == true)
                return await GenerateTokenResponse(existingUser.FirstOrDefault());

            var userModel = await _userService.CreateUserModelFromClains(userClaims);
            var createdUser = await _userService.CreateAsync(userModel);

            return createdUser != null
                ? await GenerateTokenResponse(createdUser)
                : Unauthorized();
        }
    }
}
