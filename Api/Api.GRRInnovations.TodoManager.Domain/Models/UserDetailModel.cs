using Api.GRRInnovations.TodoManager.Interfaces.Models;

namespace Api.GRRInnovations.TodoManager.Domain.Models
{
    public class UserDetailModel : BaseModel, IUserDetailModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string FirtsName { get; set; }
        public string LastName { get; set; }
    }
}
