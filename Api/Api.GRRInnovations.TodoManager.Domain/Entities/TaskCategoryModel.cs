using Api.GRRInnovations.TodoManager.Interfaces.Models;

namespace Api.GRRInnovations.TodoManager.Domain.Entities
{
    public class TaskCategoryModel : BaseModel, ITaskCategoryModel
    {
        public Guid TaskUid { get; set; }
        public TaskModel DbTask { get; set; }

        public ITaskModel Task
        {
            get => DbTask;
            set => DbTask = value as TaskModel;
        }

        public Guid CategoryUid { get; set; }

        public CategoryModel DbCategory { get; set; }

        public ICategoryModel Category
        {
            get => DbCategory;
            set => DbCategory = value as CategoryModel;
        }
    }
}
