using Api.GRRInnovations.TodoManager.Domain.Entities;
using Api.GRRInnovations.TodoManager.Domain.Extensions;
using Api.GRRInnovations.TodoManager.Domain.Models;
using Api.GRRInnovations.TodoManager.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Api.GRRInnovations.TodoManager.Infrastructure.Security.Authentication.JwtClaimHelper;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Security.Authentication
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _jwtSettings;

        public JwtService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public IJwtResultModel GenerateToken(IUserModel user)
        {
            var expireDiff = TimeSpan.FromDays(10);

            var jwtData = new JwtModel(user);

            if (jwtData.NotBefore == null) jwtData.NotBefore = DateTime.Now;
            jwtData.ExpireAt = jwtData.NotBefore + expireDiff;


            var jwtKey = Encoding.UTF8.GetBytes(_jwtSettings.Secret);
            var securityKey = new SymmetricSecurityKey(jwtKey);
            var claims = GenerateClaims(user);
            var identity = new ClaimsIdentity(claims, "OAuth");
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature),
                IssuedAt = jwtData.NotBefore,
                Subject = identity,
                NotBefore = jwtData.NotBefore,
                Expires = jwtData.ExpireAt
            });

            var token = handler.WriteToken(securityToken);

            var result = new JwtResultModel
            {
                AccessToken = token,
                Expire = jwtData.ExpireAt.Value.TimeStamp(),
                Type = "Bearer"
            };

            return result;
        }

        public IUserModel? FromJwt(string token)
        {
            var info = Validate(token);
            if (info == null) return null;

            var result = new JwtModel(info.Claims.ToList())
            {
                Jwt = token,
                JwtToken = info,
                NotBefore = info.ValidFrom,
                ExpireAt = info.ValidTo
            };

            return result.Model;
        }

        private JwtSecurityToken Validate(string token)
        {
            JwtSecurityToken result = null;
            try
            {
                var jwtKey = Encoding.UTF8.GetBytes(_jwtSettings.Secret);
                var tokenHandler = new JwtSecurityTokenHandler();
                var paramsValidation = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(jwtKey),
                    ValidAudience = _jwtSettings.Audience,
                    ValidIssuer = _jwtSettings.Issuer
                };

                SecurityToken securityToken;
                tokenHandler.ValidateToken(token, paramsValidation, out securityToken);

                result = tokenHandler.ReadJwtToken(token);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

            return result;
        }
    }
}
