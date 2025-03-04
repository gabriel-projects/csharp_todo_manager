using Api.GRRInnovations.TodoManager.Interfaces.Models;
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
    }
}
