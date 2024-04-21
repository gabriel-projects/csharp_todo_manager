using App.GRRInnovations.TodoManager.Integration.TodoManager.Api.Interfaces;
using App.GRRInnovations.TodoManager.Integration.TodoManager.Api.Models;
using App.GRRInnovations.TodoManager.Integration.TodoManager.Api.ServicesConfiguration;

namespace App.GRRInnovations.TodoManager.Integration.TodoManager.Api.Services
{
    public class TaskService : ServiceBase, ITaskService
    {
        public TaskService() : base("v1/task")
        {
        }

        public async Task<IServiceResult<List<ITaskModel>>> GetTasks()
        {
            var tasks = await GetAsync<List<TaskModel>>("tasks");

            List<ITaskModel> taskModels = tasks.Value.ConvertAll(t => (ITaskModel)t);

            var result = new ServiceResult<List<ITaskModel>>
            {
                ResultType = tasks.ResultType,
                Message = tasks.Message,
                Value = taskModels
            };

            return result;
        }
    }
}
