using App.GRRInnovations.TodoManager.Integration.TodoManager.Api.Interfaces.Models;

namespace App.GRRInnovations.TodoManager.Integration.TodoManager.Api.Interfaces.Services
{
    public interface ITaskService
    {
        Task<IServiceResult<List<ITaskModel>>> GetTasks();
    }
}
