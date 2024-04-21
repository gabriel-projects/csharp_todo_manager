namespace App.GRRInnovations.TodoManager.Integration.TodoManager.Api.Interfaces
{
    public interface ITaskService
    {
        Task<IServiceResult<List<ITaskModel>>> GetTasks();
    }
}
