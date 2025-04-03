using Api.GRRInnovations.TodoManager.Domain.Wrappers.In;
using Api.GRRInnovations.TodoManager.Domain.Wrappers.Out;
using Api.GRRInnovations.TodoManager.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.GRRInnovations.TodoManager.Controllers
{
    public class TaskAIController : ControllerBase
    {
        private readonly IOpenAIService _openAIService;
        public TaskAIController(IOpenAIService openAIService)
        {
            _openAIService = openAIService;
        }

        [HttpPost("interpret")]
        public async Task<IActionResult> InterpretTask([FromBody] WrapperInInterpretTask wrapperInInterpretTask)
        {
            var (isValid, errorMessage) = wrapperInInterpretTask.Validate();
            if (!isValid)
            {
                return BadRequest(new WrapperOutError(errorMessage));
            }

            var task = await _openAIService.InterpretTaskAsync(wrapperInInterpretTask.Message, "");
            if (task == null)
                return BadRequest("Não foi possível interpretar a mensagem.");

            return Ok(task);
        }
    }
}
