using Api.GRRInnovations.TodoManager.Domain.Entities;
using Api.GRRInnovations.TodoManager.Interfaces.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Security.Authentication
{
    public class JwtModel
    {
        public JwtModel(IUserModel model)
        {
            Model = model;
            AdditionalClaims = JwtClaimHelper.GenerateClaims(model);
        }

        public JwtModel(List<Claim> claims)
        {
            AdditionalClaims = claims ?? new List<Claim>();
            Model = JwtClaimHelper.ExtractUserFromClaims(claims);
        }

        public JwtSecurityToken JwtToken { get; set; }

        public List<Claim> AdditionalClaims { get; set; } = new List<Claim>();

        public IUserModel Model { get; set; }

        public string Jwt { get; set; }

        public DateTime? NotBefore { get; set; }

        public DateTime? ExpireAt { get; set; }
    }
}
