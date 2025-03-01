using Api.GRRInnovations.TodoManager.Interfaces.Models;
using System.Security.Claims;

namespace Api.GRRInnovations.TodoManager.Interfaces.Authentication
{
    public interface IJwtService
    {
        string GenerateToken(IUserModel user);
        ClaimsPrincipal? ValidateToken(string token);
    }
}
