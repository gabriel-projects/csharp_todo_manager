using Api.GRRInnovations.TodoManager.Domain.Models;

namespace Api.GRRInnovations.TodoManager.Domain.Entities
{
    public class UserModel : BaseModel, IUserModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool BlockedAccess { get; set; }
        public bool PendingConfirm { get; set; }

        public UserDetailModel? DbUserDetail { get; set; }
        public IUserDetailModel? UserDetail
        {
            get => DbUserDetail;
            set => DbUserDetail = value as UserDetailModel;
        }

        public List<TaskModel>? DbTasks { get; set; }

        public List<ITaskModel>? Tasks
        {
            get => DbTasks?.Cast<ITaskModel>()?.ToList();
            set => DbTasks = value?.Cast<TaskModel>()?.ToList();
        }

        public UserModel()
        {
            DbTasks = new List<TaskModel>();
        }
    }
}
