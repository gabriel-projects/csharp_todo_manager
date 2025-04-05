using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.GRRInnovations.TodoManager.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        [HttpGet("report/{userId}")]
        public IActionResult GetReport(Guid userId)
        {
            //todo: gerar um pdf ou um excel com os dados do usuário
            //todo: ter uma tabela de contabilização de tarefas por usuario
            var report = new
            {
                UserId = userId,
                ReportDate = DateTime.UtcNow,
                TasksCompleted = 5,
                TasksPending = 2
            };
            return Ok(report);
        }
    }
}
