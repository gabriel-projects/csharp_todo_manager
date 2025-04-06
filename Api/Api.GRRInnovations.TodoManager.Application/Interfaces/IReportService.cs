using Api.GRRInnovations.TodoManager.Domain.Enuns;

namespace Api.GRRInnovations.TodoManager.Application.Interfaces
{
    public interface IReportService
    {
        public Task<byte[]> GenerateDailyTasksReport(Guid userId, EReportType format);
    }
}
