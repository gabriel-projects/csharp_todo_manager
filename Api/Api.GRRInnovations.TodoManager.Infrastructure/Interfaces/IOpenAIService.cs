using Api.GRRInnovations.TodoManager.Domain.Models;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Services
{
    public interface IOpenAIService
    {
        Task<ITaskModel?> InterpretTaskAsync(string message, IUserModel user);
    }
}
