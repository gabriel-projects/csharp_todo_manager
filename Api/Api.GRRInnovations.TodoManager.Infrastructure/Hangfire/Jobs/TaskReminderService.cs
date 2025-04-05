using Api.GRRInnovations.TodoManager.Domain.Models;
using Api.GRRInnovations.TodoManager.Infrastructure.Interfaces;
using Api.GRRInnovations.TodoManager.Infrastructure.Repositories;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Hangfire.Jobs
{
    public class TaskReminderService : ITaskReminderService
    {
        public readonly ITaskRepository _taskRepository;

        public TaskReminderService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task SendReminderEmail(ITaskModel taskModel)
        {
            var task = await _taskRepository.GetAsync(taskModel.Uid);
            if (task == null) return;

            if (task.Status != Domain.Enuns.EStatusTask.Pending) return;

            //todo enviar email
            //adicioanr logs
        }
    }
}
