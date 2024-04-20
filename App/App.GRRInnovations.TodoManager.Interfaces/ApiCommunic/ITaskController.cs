using App.GRRInnovations.TodoManager.Interfaces.Models;

namespace App.GRRInnovations.TodoManager.Interfaces.ApiCommunic
{
    public interface ITaskController
    {
        Task<IResultCommunic<List<ITaskModel>>> GetTasks();
    }
}
