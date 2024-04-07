using Api.GRRInnovations.TodoManager.Domain.Wrappers.In;
using Api.GRRInnovations.TodoManager.Domain.Wrappers.Out;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.GRRInnovations.TodoManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController()
        {
            
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(WrapperInLogin wrapperInLogin) 
        {
            if (!wrapperInLogin.IsValid()) return new BadRequestObjectResult(new WrapperOutError { Title = "Login ou senha inválidos." });

            var remoteUser = await UserService.UserWithLogin(user.Login, user.Password, userOptions).ConfigureAwait(false);
            if (remoteUser == null) return new UnauthorizedResult();
        }
    }
}
