using Api.GRRInnovations.TodoManager.Domain.Models;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Interfaces
{
    public interface IJwtService
    {
        IJwtResultModel GenerateToken(IUserModel user);
        IUserModel? FromJwt(string token);
    }
}
