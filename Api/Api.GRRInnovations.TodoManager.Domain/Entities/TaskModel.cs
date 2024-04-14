using Api.GRRInnovations.TodoManager.Interfaces.Enuns;
using Api.GRRInnovations.TodoManager.Interfaces.Models;

namespace Api.GRRInnovations.TodoManager.Domain.Entities
{
    public class TaskModel : BaseModel, ITaskModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Recurrent { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public EStatusTask Status { get; set; } = EStatusTask.Pending;
        public EPriorityTask Priority { get; set; } = EPriorityTask.None;

        public UserModel? DbUser { get; set; }
        public IUserModel? User
        {
            get => DbUser;
            set => DbUser = value as UserModel;
        }
        public Guid UserUid { get; set; }

        public CategoryModel? DbCategory { get; set; }
        public ICategoryModel? Category
        {
            get => DbCategory;
            set => DbCategory = value as CategoryModel;
        }

        public Guid CategoryUid { get; set; }
    }
}
