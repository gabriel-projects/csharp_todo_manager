using Api.GRRInnovations.TodoManager.Domain.Models;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Repositories
{
    public interface ITaskRepository
    {
        Task<ITaskModel> GetAsync(Guid id);

        Task<List<ITaskModel>> GetAllAsync(TaskOptions options);

        Task<ITaskModel> UpdateAsync(string json, ITaskModel task);

        Task<ITaskModel> UpdateAsync(ITaskModel task);

        Task<bool> DeleteAsync(ITaskModel model);

        Task<ITaskModel> CreatAsync(ITaskModel model, IUserModel inUser, ICategoryModel inCategory);
    }

    public class TaskOptions
    {
        public List<Guid> FilterUsers { get; set; }

        public List<Guid> FilterUids { get; set; }
    }
}
