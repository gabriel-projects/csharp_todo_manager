using Api.GRRInnovations.TodoManager.Domain.Enuns;
using Api.GRRInnovations.TodoManager.Infrastructure.Interfaces;
using Api.GRRInnovations.TodoManager.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Hangfire.Jobs
{
    public class TaskCleanupService : ITaskCleanupService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ILogger<TaskCleanupService> _logger;

        public TaskCleanupService(ITaskRepository taskRepository, ILogger<TaskCleanupService> logger)
        {
            _taskRepository = taskRepository;
            _logger = logger;
        }

        public async Task DeleteOldCompletedTasksAsync()
        {
            _logger.LogInformation("🧹 [TaskCleanupService] Iniciando limpeza de tarefas concluídas com mais de 30 dias...");

            var tasks = await _taskRepository.GetAllAsync(new TaskOptions
            {
                FilterStatus = EStatusTask.Completed,
                CreatedAtLessThanDays = DateTime.UtcNow.AddDays(-30),
                Recurrent = false
            });

            _logger.LogInformation("🔍 {Count} tarefas encontradas para exclusão.", tasks.Count);

            foreach (var task in tasks)
            {
                _logger.LogInformation("🗑️ Deletando tarefa ID: {TaskId}, Criada em: {CreatedAt}", task.Uid, task.CreatedAt);
                await _taskRepository.DeleteAsync(task);
            }

            _logger.LogInformation("✅ Limpeza finalizada. {Count} tarefas excluídas.", tasks.Count);
        }
    }
}
