using Api.GRRInnovations.TodoManager.Application.Interfaces;
using Api.GRRInnovations.TodoManager.Domain.Entities;
using Api.GRRInnovations.TodoManager.Domain.Enuns;
using Api.GRRInnovations.TodoManager.Domain.Models;
using Api.GRRInnovations.TodoManager.Infrastructure.Repositories;

namespace Api.GRRInnovations.TodoManager.Application.Services
{
    public class TaskService : ITaskService
    {
        public readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public Task<ITaskModel> CreatAsync(TaskModel taskModel, IUserModel inUser, ICategoryModel inCategory)
        {
            if (taskModel == null || inUser == null) return null;

            return _taskRepository.CreatAsync(taskModel, inUser, inCategory);
        }

        public Task<ITaskModel> UpdateAsync(string json, ITaskModel task)
        {
            if (task is not TaskModel taskModel || string.IsNullOrEmpty(json)) return null;

            return _taskRepository.UpdateAsync(json, taskModel);
        }

        public Task<bool> DeleteAsync(ITaskModel task)
        {
            return _taskRepository.DeleteAsync(task);
        }

        public async Task<List<ITaskModel>> GetAllAsync(TaskOptions options)
        {
            return await _taskRepository.GetAllAsync(options);
        }

        public async Task<ITaskModel> GetAsync(Guid uid)
        {
            return await _taskRepository.GetAsync(uid);
        }

        public async Task<ITaskModel> TaskCompletedAsync(ITaskModel model)
        {
            var data = model as TaskModel;
            if (data == null) return null;

            data.Status = EStatusTask.Completed;

            var task = await _taskRepository.UpdateAsync(model);
            return task;
        }

    }
}
