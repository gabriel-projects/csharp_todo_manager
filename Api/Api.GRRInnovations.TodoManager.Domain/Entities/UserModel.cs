using Api.GRRInnovations.TodoManager.Interfaces.Models;

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

        public List<ITaskModel> Tasks
        {

        }


    }
}
