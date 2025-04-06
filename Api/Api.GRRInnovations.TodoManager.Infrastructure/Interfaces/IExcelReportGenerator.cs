using Api.GRRInnovations.TodoManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Interfaces
{
    public interface IExcelReportGenerator
    {
        public byte[] GenerateDailyTasksReport(List<ITaskModel> tasks);
    }
}
