using Api.GRRInnovations.TodoManager.Domain.Models;
using Api.GRRInnovations.TodoManager.Domain.ValueObjects;
using System.Security.Claims;

namespace Api.GRRInnovations.TodoManager.Application.Interfaces
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
