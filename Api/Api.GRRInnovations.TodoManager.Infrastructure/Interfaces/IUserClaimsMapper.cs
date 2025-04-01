using Api.GRRInnovations.TodoManager.Domain.Entities;
using System.Security.Claims;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Interfaces
{
    public interface IUserClaimsMapper
    {
        UserClaimsModel MapFromClaimsPrincipal(ClaimsPrincipal principal);
    }
}
