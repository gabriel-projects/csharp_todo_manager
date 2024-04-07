using Api.GRRInnovations.TodoManager.Interfaces.Models;

namespace Api.GRRInnovations.TodoManager.Domain.Models
{
    public class UserModel : BaseModel, IUserModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool BlockedAccess { get; set; }
        public bool PendingConfirm { get; set; }
    }
}
