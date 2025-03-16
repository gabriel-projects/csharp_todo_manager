using Api.GRRInnovations.TodoManager.Interfaces.Models;
using Api.GRRInnovations.TodoManager.Interfaces.Repositories;
using System.Security.Claims;

namespace Api.GRRInnovations.TodoManager.Interfaces.Services
{
    public interface IUserService
    {
        Task<IUserModel> CreateAsync(IUserModel userModel);

        Task<IUserModel> ValidateAsync(string login, string password);

        Task<bool> LoginExistsAsync(string login);

        Task<List<IUserModel>> GetAllAsync(UserOptions userOptions);

        Task<IUserModel> CreateUserModelFromClains(IUserClaimsModel claims);
    }
}
