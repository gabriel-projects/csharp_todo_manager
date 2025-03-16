using Api.GRRInnovations.TodoManager.Domain.ValueObjects;
using Api.GRRInnovations.TodoManager.Domain.Models;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<List<IUserModel>> UsersAsync(UserOptions userOptions);

        Task<IUserModel> GetAsync(Guid uid);

        Task<IUserModel> CreateAsync(IUserModel userModel);
    }
}
