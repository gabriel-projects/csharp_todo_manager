using Api.GRRInnovations.TodoManager.Interfaces.Models;

namespace Api.GRRInnovations.TodoManager.Interfaces.Services
{
    public interface IUserService
    {
        Task<IUserModel> CreateAsync(IUserModel userModel);

        Task<IUserModel> ValidateAsync(string login, string password);

        Task<bool> LoginExistsAsync(string login);
    }
}
