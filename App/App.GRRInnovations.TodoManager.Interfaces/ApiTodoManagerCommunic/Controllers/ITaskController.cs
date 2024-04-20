using App.GRRInnovations.TodoManager.Interfaces.Enuns;

namespace App.GRRInnovations.TodoManager.Interfaces.ApiTodoManagerCommunic.Controllers
{
    public interface ITaskController
    {
        Task<IResultCommunic<List<ITaskModel>>> GetTasks();
    }
}
