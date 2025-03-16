namespace Api.GRRInnovations.TodoManager.Infrastructure.Services
{
    public interface IOpenAIService
    {
        Task<string?> InterpretTaskAsync(string message, string user);
    }
}
