using Api.GRRInnovations.TodoManager.Domain.Enuns;
using Api.GRRInnovations.TodoManager.Domain.Models;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Repositories
{
    public interface ITaskRepository
    {
        Task<ITaskModel> GetAsync(Guid id, TaskOptions options);

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

        public EStatusTask FilterStatus { get; set; }

        public bool Recurrent { get; set; }

        public DateTime? CreatedAtLessThanDays { get; set; }

        public bool DueWithinOneHour { get; set; }

        public bool IncluseUser { get; set; }
    }
}
