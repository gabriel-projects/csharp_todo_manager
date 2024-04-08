using Api.GRRInnovations.TodoManager.Interfaces.Models;

namespace Api.GRRInnovations.TodoManager.Domain.Entities
{
    public class UserDetailModel : BaseModel<UserModel, IUserModel>, IUserDetailModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
