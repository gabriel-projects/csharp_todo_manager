using Api.GRRInnovations.TodoManager.Domain.Wrappers.In;
using Api.GRRInnovations.TodoManager.Domain.Wrappers.Out;
using Api.GRRInnovations.TodoManager.Infrastructure.Extensions;
using Api.GRRInnovations.TodoManager.Infrastructure.Services;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.GRRInnovations.TodoManager.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class TaskAIController : ControllerBase
    {
        private readonly IOpenAIService _openAIService;

        public TaskAIController(IOpenAIService openAIService)
        {
            _openAIService = openAIService;
        }

        [HttpPost("interpret")]
        [Authorize]
        public async Task<IActionResult> InterpretTask([FromBody] WrapperInInterpretTask wrapperInInterpretTask)
        {
            var jwtModel = await HttpContext.JwtInfo();
            if (jwtModel == null)
            {
                return Unauthorized();
            }

            var (isValid, errorMessage) = wrapperInInterpretTask.Validate();
            if (!isValid)
            {
                return BadRequest(new WrapperOutError(errorMessage));
            }

            var task = await _openAIService.InterpretTaskAsync(wrapperInInterpretTask.Message, jwtModel);
            if (task == null)
                return BadRequest("Não foi possível interpretar a mensagem.");

            return Ok(task);
        }
    }
}
