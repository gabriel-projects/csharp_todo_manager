using App.GRRInnovations.TodoManager.Domain.ApiTodoManagerCommunic.Models;
using App.GRRInnovations.TodoManager.Interfaces.ApiTodoManagerCommunic;
using App.GRRInnovations.TodoManager.Interfaces.ApiTodoManagerCommunic.Controllers;
using App.GRRInnovations.TodoManager.Interfaces.ApiTodoManagerCommunic.Models;

namespace App.GRRInnovations.TodoManager.Infrastructure.ApiCommunic.Controllers
{
    public class TaskController : BaseCommunic, ITaskController
    {
        public TaskController() : base("v1/task")
        {
        }

        public async Task<IResultCommunic<List<ITaskModel>>> GetTasks()
        {
            var tasks = await GetAsync<List<TaskModel>>("tasks");

            List<ITaskModel> taskModels = tasks.Value.ConvertAll(t => (ITaskModel)t);

            var result = new ResultCommunic<List<ITaskModel>>
            {
                ResultType = tasks.ResultType,
                Message = tasks.Message, 
                Value = taskModels
            };

            return result;
        }
    }
}
