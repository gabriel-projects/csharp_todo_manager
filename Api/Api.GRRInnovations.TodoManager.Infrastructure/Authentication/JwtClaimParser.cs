using Api.GRRInnovations.TodoManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Authentication
{
    public class JwtClaimParser
    {
        private readonly List<Claim> _claims;

        public JwtClaimParser(List<Claim> claims)
        {
            _claims = claims ?? new List<Claim>();
        }

        public UserModel ParseToUserModel()
        {
            var user = new UserModel
            {
                UserDetail = new UserDetailModel()
            };

            foreach (var claim in _claims)
            {
                switch (claim.Type)
                {
                    case JwtModel.ClaimUserUid:
                        user.Uid = Guid.Parse(claim.Value);
                        break;
                    case ClaimTypes.Email:
                        user.UserDetail.Email = claim.Value;
                        break;
                    default:
                        break;
                }
            }

            return user;
        }
    }
}
