using Api.GRRInnovations.TodoManager.Interfaces.Models;
using System.Security.Claims;

namespace Api.GRRInnovations.TodoManager.Interfaces.Authentication
{
    public interface IJwtService
    {
        IJwtResultModel GenerateToken(IUserModel user);
        IUserModel? FromJwt(string token);
    }
}
