using Api.GRRInnovations.TodoManager.Interfaces.Models;

namespace Api.GRRInnovations.TodoManager.Domain.Entities
{
    public class TaskCategoryModel : BaseModel, ITaskCategoryModel
    {
        public Guid TaskUid { get; set; }
        public TaskModel? Task { get; set; }

        public Guid CategoryUid { get; set; }
        public CategoryModel? Category { get; set; }
    }
}
