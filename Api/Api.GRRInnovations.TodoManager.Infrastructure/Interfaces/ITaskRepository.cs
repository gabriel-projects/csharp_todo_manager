using Api.GRRInnovations.TodoManager.Domain.Models;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Repositories
{
    public interface ITaskRepository
    {
        Task<ITaskModel> GetAsync(Guid id, TaskOptions options);

        Task<List<ITaskModel>> GetAllAsync(TaskOptions options);

        Task<ITaskModel> UpdateAsync(string json, ITaskModel task);

        Task<bool> DeleteAsync(ITaskModel model);

        Task<ITaskModel> CreatAsync(ITaskModel model, IUserModel inUser, ICategoryModel inCategory);

        Task<ITaskModel> TaskCompletedAsync(ITaskModel model);
    }

    public class TaskOptions
    {
        public List<Guid> FilterUsers { get; set; }

        public List<Guid> FilterUids { get; set; }
    }
}
