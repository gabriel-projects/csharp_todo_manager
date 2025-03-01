using Api.GRRInnovations.TodoManager.Domain.Entities;
using Api.GRRInnovations.TodoManager.Interfaces.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Authentication
{
    public class JwtModel
    {
        public const string ClaimUserUid = "user_uid";
        private const string ClaimEmail = "email";

        public JwtModel(IUserModel model)
        {
            Model = model as UserModel;
        }

        public JwtModel(List<Claim> claims)
        {
            AdditionalClaims = claims ?? new List<Claim>();
        }
        public JwtSecurityToken JwtToken { get; set; }

        public List<Claim> AdditionalClaims { get; set; } = new List<Claim>();

        public UserModel Model { get; set; }

        public string Jwt { get; set; }

        public DateTime? NotBefore { get; set; }

        public DateTime? ExpireAt { get; set; }

        public async Task<List<Claim>> AllClaims()
        {
            var claims = new List<Claim>();

            if (Model == null) return claims;

            claims.Add(new Claim(ClaimTypes.Email, Model.UserDetail?.Email ?? ""));
            claims.Add(new Claim(ClaimUserUid, Model.Uid.ToString()));

            return claims;
        }
    }
}
