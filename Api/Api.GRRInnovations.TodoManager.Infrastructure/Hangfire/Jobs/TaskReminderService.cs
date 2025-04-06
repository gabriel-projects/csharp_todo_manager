using Api.GRRInnovations.TodoManager.Domain.Models;
using Api.GRRInnovations.TodoManager.Infrastructure.Interfaces;
using Api.GRRInnovations.TodoManager.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Hangfire.Jobs
{
    public class TaskReminderService : ITaskReminderService
    {
        public readonly ITaskRepository _taskRepository;
        public readonly IEmailService _emailService;
        public readonly ILogger<TaskReminderService> _logger;

        public TaskReminderService(ITaskRepository taskRepository, IEmailService emailService, ILogger<TaskReminderService> logger)
        {
            _taskRepository = taskRepository;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task SendReminderEmail(Guid taskUid)
        {
            var task = await _taskRepository.GetAsync(taskUid, new TaskOptions { IncluseUser = true });
            if (task == null)
            {
                _logger.LogInformation("Task not found: {TaskUid}", taskUid);
                return;
            }

            if (task.Status != Domain.Enuns.EStatusTask.Pending) 
            {
                _logger.LogInformation("Task is not pending: {TaskUid}", taskUid);
                return;
            }

            var email = task?.User?.UserDetail?.Email;
            if (string.IsNullOrEmpty(email))
            {
                _logger.LogInformation("Email not found for user: {UserUid}", task.User.Uid);
                return;
            }

            var subject = "Lembrete de Tarefa";
            var body = $"Lembrete: {task.Title} - {task.Description}";

            await _emailService.SendEmailAsync(email, subject, body);
        }
    }
}
