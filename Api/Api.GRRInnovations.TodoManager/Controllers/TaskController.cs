using Api.GRRInnovations.TodoManager.Domain.Entities;
using Api.GRRInnovations.TodoManager.Domain.Wrappers.In;
using Api.GRRInnovations.TodoManager.Domain.Wrappers.Out;
using Api.GRRInnovations.TodoManager.Infrastructure.Extensions;
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
        public async Task<ActionResult> CreateTask([FromBody] WrapperInTask<TaskModel, CategoryModel> wrapperInTask)
        {
            //validar
            var user = await HttpContext.JwtInfo();
            if (user == null) return Unauthorized();


            return Ok();
        }
    }
}
