using Api.GRRInnovations.TodoManager.Application.Interfaces;
using Api.GRRInnovations.TodoManager.Domain.Enuns;
using Api.GRRInnovations.TodoManager.Domain.Wrappers.Out;
using Api.GRRInnovations.TodoManager.Infrastructure.Extensions;
using Api.GRRInnovations.TodoManager.Infrastructure.Repositories;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.GRRInnovations.TodoManager.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IReportService _reportService;

        public ReportController(ITaskService taskService, IReportService reportService)
        {
            _taskService = taskService;
            _reportService = reportService;
        }

        [HttpGet("{format}")]
        public async Task<ActionResult> GetDailyReport(EReportType format)
        {
            var jwtModel = await HttpContext.JwtInfo();
            if (jwtModel == null)
            {
                return Unauthorized();
            }

            var userId = jwtModel.Uid; // extensão que você pode já ter
            var fileBytes = await _reportService.GenerateDailyTasksReport(userId, format);

            var contentType = format == EReportType.Pdf
                ? "application/pdf"
                : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            var fileName = $"Relatorio_Tarefas_{DateTime.UtcNow:yyyyMMdd}.{(format == EReportType.Pdf ? "pdf" : "xlsx")}";
            return File(fileBytes, contentType, fileName);
        }
    }
}
