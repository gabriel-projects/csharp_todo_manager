namespace Api.GRRInnovations.TodoManager.Interfaces.Services
{
    public interface IOpenAIService
    {
        Task<string?> InterpretTaskAsync(string message, string user);
    }
}
