using Api.GRRInnovations.TodoManager.Domain.Attributes;
using Api.GRRInnovations.TodoManager.Interfaces.Models;
using Api.GRRInnovations.TodoManager.Interfaces.Repositories;

namespace Api.GRRInnovations.TodoManager.Services
{
    public class UserService
    {
        public IUserRepository UserRepository { get; }

        public UserService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public async Task<bool> LoginAvailable(string login)
        {
            var users = await UserRepository.Users(new UserOptions { FilterLogins = new List<string> { login } });

            return users.Count == 0;
        }

        public async Task<IUserModel> Create(IUserModel userModel)
        {
            if (userModel == null) return null;

            return await UserRepository.Create(userModel);
        }
    }
}
