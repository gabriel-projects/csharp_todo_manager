using Api.GRRInnovations.TodoManager.Interfaces.Models;

namespace Api.GRRInnovations.TodoManager.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        Task<ITaskModel> GetAsync(Guid id);

        Task<ITaskModel> GetAllAsync(TaskOptions options);

        Task<ITaskModel> UpdateAsync(ITaskModel task);

        Task DeleteAsync(Guid Uid);

        Task<ITaskModel> CreatAsync(ITaskModel model, IUserModel inUser, ICategoryModel inCategory);
    }

    public class TaskOptions
    {
        public List<Guid> FilterUsers { get; set; }

        public List<Guid> FilterUids { get; set; }
    }
}
