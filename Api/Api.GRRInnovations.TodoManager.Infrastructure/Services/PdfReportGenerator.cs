using Api.GRRInnovations.TodoManager.Domain.Models;
using Api.GRRInnovations.TodoManager.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using QuestPDF.Fluent;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Services
{
    public class PdfReportGenerator : IPdfReportGenerator
    {
        private readonly ILogger<PdfReportGenerator> _logger;

        public PdfReportGenerator(ILogger<PdfReportGenerator> logger)
        {
            _logger = logger;
        }

        public byte[] GenerateDailyTasksReport(List<ITaskModel> tasks, string userName)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);
                    page.Header().Text($"Daily Tasks Report - {userName}").FontSize(20).Bold();

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(); // Título
                            columns.ConstantColumn(100); // Data
                            columns.ConstantColumn(100); // Hora
                        });

                        table.Cell().Text("Título").Bold();
                        table.Cell().Text("Data").Bold();
                        table.Cell().Text("Hora").Bold();

                        foreach (var task in tasks)
                        {
                            table.Cell().Text(task.Title);
                            table.Cell().Text(task.Start.ToString("dd/MM/yyyy"));
                            table.Cell().Text(task.Start.ToString("HH:mm"));
                        }
                    });
                });
            });

            return document.GeneratePdf();
        }
    }
}
