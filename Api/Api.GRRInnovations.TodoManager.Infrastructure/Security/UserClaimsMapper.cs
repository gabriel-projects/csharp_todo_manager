using Api.GRRInnovations.TodoManager.Domain.Entities;
using Api.GRRInnovations.TodoManager.Infrastructure.Interfaces;
using System.Security.Claims;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Security
{
    public class UserClaimsMapper : IUserClaimsMapper
    {
        public UserClaimsModel MapFromClaimsPrincipal(ClaimsPrincipal principal)
        {
            var identity = principal?.Identities?.FirstOrDefault();
            if (identity == null) return null;

            string firstName = identity.FindFirst(ClaimTypes.GivenName)?.Value;
            string lastName = identity.FindFirst(ClaimTypes.Surname)?.Value;
            string fullName = identity.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName))
            {
                if (!string.IsNullOrEmpty(fullName))
                {
                    var parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    firstName = parts.FirstOrDefault() ?? fullName;
                    lastName = parts.Length > 1 ? parts[^1] : "N/A";
                }
            }

            return new UserClaimsModel
            {
                Email = identity.FindFirst(ClaimTypes.Email)?.Value,
                FirstName = firstName,
                LastName = lastName,
                PrivateId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                Name = fullName
            };
        }
    }
}
