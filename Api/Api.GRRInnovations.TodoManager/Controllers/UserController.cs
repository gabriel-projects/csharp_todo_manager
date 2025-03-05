using Api.GRRInnovations.TodoManager.Domain.Entities;
using Api.GRRInnovations.TodoManager.Domain.Extensions;
using Api.GRRInnovations.TodoManager.Domain.Wrappers.In;
using Api.GRRInnovations.TodoManager.Domain.Wrappers.Out;
using Api.GRRInnovations.TodoManager.Infrastructure.Extensions;
using Api.GRRInnovations.TodoManager.Services;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.GRRInnovations.TodoManager.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            this._userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] WrapperInUser<UserModel, UserDetailModel> wrapperInUser) 
        {
            if (string.IsNullOrEmpty(wrapperInUser.Login) || string.IsNullOrEmpty(wrapperInUser.Password)) return new BadRequestObjectResult(new WrapperOutError { Title = "Dados inválidos." });

            var available = await _userService.LoginAvailable(wrapperInUser.Login);
            if (!available) return new BadRequestObjectResult(new WrapperOutError { Title = "Login já registrado." });

            var wrapperModel = await wrapperInUser.Result();

            var model = await _userService.Create(wrapperModel).ConfigureAwait(false);
            if (model == null) return new BadRequestObjectResult(new WrapperOutError { Title = "Falha ao criar usuário." });

            var response = await WrapperOutUser.From(model).ConfigureAwait(false);
            return new OkObjectResult(response);
        }

        [HttpGet("users")]
        [Authorize]
        public async Task<ActionResult> Users()
        {
            var user = await HttpContext.JwtInfo();
            if (user == null) return Unauthorized(); //todo verificar se role é anonymus

            var model = await _userService.Users().ConfigureAwait(false);
            if (model == null) return new BadRequestObjectResult(new WrapperOutError { Title = "Falha ao criar usuário." });

            var response = await WrapperOutUser.From(model).ConfigureAwait(false);
            return new OkObjectResult(response);
        }
    }
}
