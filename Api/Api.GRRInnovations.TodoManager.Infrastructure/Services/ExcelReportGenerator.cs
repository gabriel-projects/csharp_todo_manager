using Api.GRRInnovations.TodoManager.Domain.Models;
using Api.GRRInnovations.TodoManager.Infrastructure.Interfaces;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Services
{
    public class ExcelReportGenerator : IExcelReportGenerator
    {
        public byte[] GenerateDailyTasksReport(List<ITaskModel> tasks)
        {
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Tarefas do Dia");

            worksheet.Cells[1, 1].Value = "Título";
            worksheet.Cells[1, 2].Value = "Data";
            worksheet.Cells[1, 3].Value = "Hora de Início";

            int row = 2;
            foreach (var task in tasks)
            {
                worksheet.Cells[row, 1].Value = task.Title;
                worksheet.Cells[row, 2].Value = task.End.ToString("dd/MM/yyyy");
                worksheet.Cells[row, 3].Value = task.End.ToString("HH:mm");
                row++;
            }

            // Ajustar largura das colunas
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            return package.GetAsByteArray();
        }
    }
}
