using App.GRRInnovations.TodoManager.Domain.Repositories;
using App.GRRInnovations.TodoManager.Interfaces.ApiCommunic;
using App.GRRInnovations.TodoManager.Interfaces.Models;

namespace App.GRRInnovations.TodoManager.Infrastructure.Repositories
{
    public class AppointmentRepository : ITaskRepository
    {
        private readonly ITaskController TaskController;

        public AppointmentRepository(ITaskController taskController)
        {
            TaskController = taskController;
        }

        public async Task<List<ITaskModel>> Appointments()
        {
            var tasks = await TaskController.GetTasks();

            return tasks.Value;
        }
    }
}
