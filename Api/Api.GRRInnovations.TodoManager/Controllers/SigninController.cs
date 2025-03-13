using Api.GRRInnovations.TodoManager.Application.Services;
using Api.GRRInnovations.TodoManager.Domain.Wrappers.In;
using Api.GRRInnovations.TodoManager.Domain.Wrappers.Out;
using Api.GRRInnovations.TodoManager.Infrastructure.Security.Authentication;
using Api.GRRInnovations.TodoManager.Interfaces.Authentication;
using Api.GRRInnovations.TodoManager.Interfaces.Services;
using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.GRRInnovations.TodoManager.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class SigninController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public SigninController(UserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost]
        public async Task<ActionResult<WrapperOutJwtResult>> SigninUid([FromBody] WrapperInLogin wrapperInLogin)
        {
            if (string.IsNullOrEmpty(wrapperInLogin.Login) || string.IsNullOrEmpty(wrapperInLogin.Password)) return new BadRequestObjectResult(new WrapperOutError ("Dados inválidos."));

            var remoteUser = await _userService.ValidateAsync(wrapperInLogin.Login, wrapperInLogin.Password).ConfigureAwait(false);
            if (remoteUser == null) return new UnauthorizedResult();

            var userToken = _jwtService.GenerateToken(remoteUser);

            var response = await WrapperOutJwtResult.From(userToken).ConfigureAwait(false);
            return new OkObjectResult(response);
        }
    }
}
