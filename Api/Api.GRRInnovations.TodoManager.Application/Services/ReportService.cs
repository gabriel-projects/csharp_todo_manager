using Api.GRRInnovations.TodoManager.Application.Interfaces;
using Api.GRRInnovations.TodoManager.Domain.Enuns;
using Api.GRRInnovations.TodoManager.Infrastructure.Interfaces;
using Api.GRRInnovations.TodoManager.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TodoManager.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IPdfReportGenerator _pdfGenerator;
        private readonly IExcelReportGenerator _excelGenerator;

        public ReportService(IExcelReportGenerator excelGenerator, IPdfReportGenerator pdfGenerator, ITaskRepository taskRepository)
        {
            _excelGenerator = excelGenerator;
            _pdfGenerator = pdfGenerator;
            _taskRepository = taskRepository;
        }

        public async Task<byte[]> GenerateDailyTasksReport(Guid userId, EReportType format)
        {
            var tasks = await _taskRepository.GetAllAsync(new TaskOptions());

            return format switch
            {
                EReportType.Pdf => _pdfGenerator.GenerateDailyTasksReport(tasks, userId.ToString()),
                EReportType.Excel => _excelGenerator.GenerateDailyTasksReport(tasks),
                _ => throw new ArgumentException("Formato inválido")
            };
        }
    }
}
