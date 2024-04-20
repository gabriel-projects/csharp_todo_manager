using App.GRRInnovations.TodoManager.Interfaces.ApiTodoManagerCommunic.Models;

namespace App.GRRInnovations.TodoManager.Domain.Repositories
{
    public interface ITaskRepository
    {
        Task<List<ITaskModel>> Appointments();
    }
}
