using App.GRRInnovations.TodoManager.Domain.Models;
using App.GRRInnovations.TodoManager.Interfaces.ApiCommunic;
using App.GRRInnovations.TodoManager.Interfaces.Models;

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
