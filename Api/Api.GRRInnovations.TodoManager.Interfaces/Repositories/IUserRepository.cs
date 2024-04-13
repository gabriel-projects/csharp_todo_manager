using Api.GRRInnovations.TodoManager.Interfaces.Models;

namespace Api.GRRInnovations.TodoManager.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<List<IUserModel>> UsersAsync(UserOptions userOptions);

        Task<IUserModel> GetAsync(Guid uid);

        Task<IUserModel> CreateAsync(IUserModel userModel);
    }

    public class UserOptions
    {
        public List<Guid> FilterUsers { get; set; }

        public List<string> FilterLogins { get; set; }
    }
}
