using Api.GRRInnovations.TodoManager.Domain.Wrappers.In;
using Api.GRRInnovations.TodoManager.Domain.Wrappers.Out;
using Api.GRRInnovations.TodoManager.Infrastructure.Helpers;
using Api.GRRInnovations.TodoManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.GRRInnovations.TodoManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SigninController : ControllerBase
    {
        private readonly UserService UserService;

        public SigninController(UserService userService)
        {
            this.UserService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<WrapperOutJwtResult>> SigninUid([FromBody] WrapperInLogin wrapperInLogin)
        {
            if (string.IsNullOrEmpty(wrapperInLogin.Login) || string.IsNullOrEmpty(wrapperInLogin.Password)) return new BadRequestObjectResult(new WrapperOutError { Title = "Dados inválidos." });

            var remoteUser = await this.UserService.Validade(wrapperInLogin.Login, wrapperInLogin.Password).ConfigureAwait(false);
            if (remoteUser == null) return new UnauthorizedResult();

            var response = await JwtHelper.JwtResult(remoteUser).ConfigureAwait(false);

            return response;
        }
    }
}
