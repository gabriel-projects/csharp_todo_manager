using Api.GRRInnovations.TodoManager.Domain.Entities;
using Api.GRRInnovations.TodoManager.Domain.Models;
using System.Security.Claims;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Security.Authentication
{
    public static class JwtClaimHelper
    {
        public const string ClaimUserUid = "user_uid";
        private const string ClaimEmail = "email";

        public static List<Claim> GenerateClaims(IUserModel model)
        {
            var claims = new List<Claim>();

            if (model == null) return claims;

            if (!string.IsNullOrWhiteSpace(model.UserDetail?.Email))
            {
                claims.Add(new Claim(ClaimEmail, model.UserDetail.Email));
            }

            claims.Add(new Claim(ClaimUserUid, model.Uid.ToString()));

            return claims;
        }

        public static IUserModel ExtractUserFromClaims(List<Claim> claims)
        {
            if (claims == null || claims.Count == 0)
                return null;

            var userUidClaim = claims.FirstOrDefault(c => c.Type == ClaimUserUid)?.Value;
            var emailClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(userUidClaim))
                return null; // Retorna nulo se não encontrar o UID

            return new UserModel
            {
                Uid = Guid.Parse(userUidClaim),
                UserDetail = new UserDetailModel
                {
                    Email = emailClaim
                }
            };
        }
    }
}
