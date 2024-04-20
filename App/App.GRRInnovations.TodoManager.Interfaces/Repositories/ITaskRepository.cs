
using App.GRRInnovations.TodoManager.Interfaces.Models;

namespace App.GRRInnovations.TodoManager.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        Task<List<ITaskModel>> Appointments();
    }
}
