using Api.GRRInnovations.TodoManager.Domain.Entities;
using Api.GRRInnovations.TodoManager.Domain.Extensions;
using Api.GRRInnovations.TodoManager.Domain.Wrappers.Out;
using Api.GRRInnovations.TodoManager.Interfaces.Authentication;
using Api.GRRInnovations.TodoManager.Interfaces.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Authentication
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _jwtSettings;

        public JwtService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public static async Task<JwtModel> FromJwt(string jwt, bool validateLifetime = true)
        {
            var info = Validate(jwt, validateLifetime);

            if (info == null) return null;

            var result = new JwtModel(info.Claims.ToList())
            {
                Jwt = jwt,
                JwtToken = info,
                NotBefore = info.ValidFrom,
                ExpireAt = info.ValidTo
            };

            await result.CreateUserModel();

            return result;
        }

        private static JwtSecurityToken Validate(string token, bool validateLifetime)
        {
            JwtSecurityToken reuslt = null;
            try
            {
                var jwtKey = Encoding.UTF8.GetBytes(Key);
                var tokenHandler = new JwtSecurityTokenHandler();
                var paramsValidation = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = validateLifetime,
                    IssuerSigningKey = new SymmetricSecurityKey(jwtKey),
                    ValidAudience = Audience,
                    ValidIssuer = Issuer
                };

                SecurityToken securityToken;
                tokenHandler.ValidateToken(token, paramsValidation, out securityToken);

                reuslt = tokenHandler.ReadJwtToken(token);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

            return reuslt;
        }

        public static async Task<WrapperOutJwtResult> JwtResult(IUserModel user)
        {
            var jwtResult = await CreateJwt(user).ConfigureAwait(false);

            return jwtResult;
        }

        private static async Task<WrapperOutJwtResult> CreateJwt(IUserModel user)
        {
            var jwtData = new JwtModel(user);
            var expireDiff = TimeSpan.FromDays(10);

            var jwtResult = await ToJwt(jwtData, expireDiff);
            return jwtResult;
        }

        private static Task<WrapperOutJwtResult> ToJwt(JwtModel data, TimeSpan expireDiff)
        {
            if (data.NotBefore == null) data.NotBefore = DateTime.Now;

            data.ExpireAt = data.NotBefore + expireDiff;

            return ToJwt(data, data.NotBefore.Value, data.ExpireAt.Value);
        }

        private static Task<WrapperOutJwtResult> ToJwt(JwtModel data, DateTime createdAt, DateTime expireAt)
        {
            return InternalToJwt(data, createdAt, expireAt);
        }

        private static async Task<WrapperOutJwtResult> InternalToJwt(JwtModel data, DateTime createdAt, DateTime expireAt)
        {
            var jwtKey = Encoding.UTF8.GetBytes(Key);
            var securityKey = new SymmetricSecurityKey(jwtKey);
            var claims = await data.AllClaims();
            var identity = new ClaimsIdentity(claims, "OAuth");
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = Issuer,
                Audience = Audience,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature),
                IssuedAt = createdAt,
                Subject = identity,
                NotBefore = createdAt,
                Expires = expireAt
            });

            var result = new WrapperOutJwtResult
            {
                AccessToken = handler.WriteToken(securityToken),
                Expire = expireAt.TimeStamp(),
                Type = "Bearer"
            };

            return result;
        }

        public string GenerateToken(IUserModel user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Uid.ToString()),
                new Claim(ClaimTypes.Email, user.UserDetail.Email)
            };

            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal? ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Secret);

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidAudience = _jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }, out _);

                return principal;
            }
            catch
            {
                return null;
            }
        }
    }
}
