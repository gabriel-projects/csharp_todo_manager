using Api.GRRInnovations.TodoManager.Domain.Models;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Services
{
    public interface IOpenAIService
    {
        Task<string?> InterpretTaskAsync(string message, IUserModel user);
    }
}
