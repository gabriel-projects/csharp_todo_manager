
namespace Api.GRRInnovations.TodoManager.Infrastructure.Interfaces
{
    public interface ITaskCleanupService
    {
        Task DeleteOldCompletedTasksAsync();
    }
}
