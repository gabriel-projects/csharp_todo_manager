using Api.GRRInnovations.TodoManager.Domain.Models;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Interfaces
{
    public interface ITaskReminderService
    {
        Task SendReminderEmail(Guid taskUid);
    }
}
