using Microsoft.AspNetCore.Http;

namespace Api.GRRInnovations.TodoManager.Application.Interfaces
{
    public interface IAuthService
    {
        string GenerateRedirectUrl(string action);
    }
}
