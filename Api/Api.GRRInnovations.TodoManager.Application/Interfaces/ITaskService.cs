using Api.GRRInnovations.TodoManager.Domain.Entities;
using Api.GRRInnovations.TodoManager.Domain.Models;
using Api.GRRInnovations.TodoManager.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TodoManager.Application.Interfaces
{
    public interface ITaskService
    {
        Task<ITaskModel> CreatAsync(TaskModel taskModel, IUserModel inUser, ICategoryModel inCategory);
        Task<bool> DeleteAsync(ITaskModel task);
        Task<List<ITaskModel>> GetAllAsync(TaskOptions options);
        Task<ITaskModel> GetAsync(Guid uid, TaskOptions options);
        Task<ITaskModel> UpdateAsync(string json, ITaskModel task);

        Task<ITaskModel> TaskCompletedAsync(ITaskModel model);
    }
}
