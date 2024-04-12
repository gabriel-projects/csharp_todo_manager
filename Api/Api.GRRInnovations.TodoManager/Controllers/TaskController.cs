using Api.GRRInnovations.TodoManager.Domain.Entities;
using Api.GRRInnovations.TodoManager.Domain.Wrappers.In;
using Api.GRRInnovations.TodoManager.Domain.Wrappers.Out;
using Api.GRRInnovations.TodoManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.GRRInnovations.TodoManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateTask([FromBody] WrapperInUser<UserModel, UserDetailModel> wrapperInUser)
        {
            if (string.IsNullOrEmpty(wrapperInUser.Login) || string.IsNullOrEmpty(wrapperInUser.Password)) return new BadRequestObjectResult(new WrapperOutError { Title = "Dados inválidos." });

            if (string.IsNullOrEmpty(wrapperInUser.Login) == false)
            {
                var available = await UserService.LoginAvailable(wrapperInUser.Login);
                if (!available) return new BadRequestObjectResult(new WrapperOutError { Title = "Login já registrado." });
            }

            var wrapperModel = await wrapperInUser.Result();

            var model = await UserService.Create(wrapperModel).ConfigureAwait(false);
            if (model == null) return new BadRequestObjectResult(new WrapperOutError { Title = "Falha ao criar usuário." });

            var response = await WrapperOutUser.From(model).ConfigureAwait(false);
            return new OkObjectResult(response);
        }
    }
}
