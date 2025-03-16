using System.Security.Claims;
using Api.GRRInnovations.TodoManager.Application.Interfaces;
using Api.GRRInnovations.TodoManager.Domain.Entities;
using Api.GRRInnovations.TodoManager.Domain.Wrappers.In;
using Api.GRRInnovations.TodoManager.Domain.Wrappers.Out;
using Api.GRRInnovations.TodoManager.Infrastructure.Security.Authentication;
using Api.GRRInnovations.TodoManager.Interfaces.Authentication;
using Api.GRRInnovations.TodoManager.Interfaces.Models;
using Api.GRRInnovations.TodoManager.Interfaces.Services;
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

        public AuthController(IJwtService jwtService, ILogger<AuthController> logger, IAuthManager authManager, IUserService userService)
        {
            _jwtService = jwtService;
            _authManager = authManager;
            _userService = userService;
        }

        [HttpGet("signin-google")]
        public IActionResult LoginGoogle()
        {
            return _authManager.HandleLogin("Google", nameof(GoogleResponse));
        }

        [HttpGet("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!authenticateResult.Succeeded)
            {
                return BadRequest("Falha ao autenticar com Google.");
            }

            var userClaims = ExtractUserClaims(authenticateResult.Principal);
            if (string.IsNullOrEmpty(userClaims.Email))
            {
                return BadRequest("Email não encontrado.");
            }

            var existingUser = await _userService.GetAllAsync(new Interfaces.Repositories.UserOptions { FilterLogins = new List<string> { userClaims.Email } });
            if (existingUser != null && existingUser.Any())
            {
                return await GenerateTokenResponse(existingUser.FirstOrDefault() as UserModel).ConfigureAwait(false);
            }

            var userModel = await _userService.CreateUserModelFromClains(userClaims);
            var createdUser = await _userService.CreateAsync(userModel);

            if (createdUser != null)
            {
                return await GenerateTokenResponse(createdUser).ConfigureAwait(false);
            }

            return Unauthorized();
        }

        [HttpGet("signin-github")]
        public IActionResult LoginGitHub()
        {
            return _authManager.HandleLogin("GitHub", nameof(GitHubResponse));
        }

        [HttpGet("github-response")]
        public async Task<IActionResult> GitHubResponse()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!authenticateResult.Succeeded)
            {
                return this.BadRequest("Falha ao autenticar com Google.");
            }

            var userClaims = ExtractUserClaims(authenticateResult.Principal);
            if (string.IsNullOrEmpty(userClaims.Email))
            {
                return BadRequest("Email não encontrado.");
            }

            var existingUser = await _userService.GetAllAsync(new Interfaces.Repositories.UserOptions { FilterLogins = new List<string> { userClaims.Email } });
            if (existingUser != null && existingUser.Any())
            {
                return await GenerateTokenResponse(existingUser.FirstOrDefault() as UserModel).ConfigureAwait(false);
            }

            var userModel = await _userService.CreateUserModelFromClains(userClaims);
            var createdUser = await _userService.CreateAsync(userModel);

            if (createdUser != null)
            {
                return await GenerateTokenResponse(createdUser).ConfigureAwait(false);
            }

            return Unauthorized();
        }

        [HttpPost]
        public async Task<ActionResult<WrapperOutJwtResult>> SigninDefault([FromBody] WrapperInLogin wrapperInLogin)
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

        private UserClaimsModel ExtractUserClaims(ClaimsPrincipal principal)
        {
            var identity = principal?.Identities?.FirstOrDefault();
            if (identity == null)
                return null;

            string firstName = identity.FindFirst(ClaimTypes.GivenName)?.Value;
            string lastName = identity.FindFirst(ClaimTypes.Surname)?.Value;
            string fullName = identity.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName))
            {

                if (!string.IsNullOrEmpty(fullName))
                {
                    var nameParts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    if (nameParts.Length > 1)
                    {
                        firstName = nameParts[0];
                        lastName = nameParts[^1];
                    }
                    else
                    {
                        firstName = fullName;
                        lastName = "N/A";
                    }
                }
            }

            return new UserClaimsModel
            {
                Email = identity.FindFirst(ClaimTypes.Email)?.Value,
                FirstName = firstName,
                LastName = lastName,
                PrivateId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                Name = fullName
            };
        }
    }
}
