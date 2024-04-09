using Api.GRRInnovations.TodoManager.Domain.Entities;
using Api.GRRInnovations.TodoManager.Domain.Extensions;
using Api.GRRInnovations.TodoManager.Domain.Models;
using Api.GRRInnovations.TodoManager.Domain.Wrappers.Out;
using Api.GRRInnovations.TodoManager.Interfaces.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Helpers
{
    public static class JwtHelper
    {
        private static string Key
        {
            get
            {
                var jwtKey = Environment.GetEnvironmentVariable("JwtKey");

                if (string.IsNullOrEmpty(jwtKey))
                {
                    //todo mover para appsetings
                    jwtKey = "6040E308-8AF2-4639-B7D7-57B405A4DA42-C33BF5B8-B498-4464-9769-FD09388DA06D";
                }

                return jwtKey;
            }
        }

        private const string Issuer = "https://api.todo.manager.com.br";

        public static string Audience = "DefaultAudience";

        public static async Task<WrapperOutJwtResult> JwtResult(IUserModel user)
        {
            var jwtResult = await CreateJwt(user).ConfigureAwait(false);

            var wrapper = new WrapperOutJwtResult();
            await wrapper.Populate(jwtResult).ConfigureAwait(false);

            return wrapper;
        }

        private static async Task<JwtResultModel> CreateJwt(IUserModel user)
        {
            var jwtData = new JwtModel(user);
            var expireDiff = TimeSpan.FromHours(1);

            var jwtResult = await ToJwt(jwtData, expireDiff);
            return jwtResult;
        }

        private static Task<JwtResultModel> ToJwt(JwtModel data, TimeSpan expireDiff)
        {
            if (data.NotBefore == null) data.NotBefore = DateTime.Now;

            data.ExpireAt = data.NotBefore + expireDiff;

            return ToJwt(data, data.NotBefore.Value, data.ExpireAt.Value);
        }

        private static Task<JwtResultModel> ToJwt(JwtModel data, DateTime createdAt, DateTime expireAt)
        {
            return InternalToJwt(data, createdAt, expireAt);
        }

        private static async Task<JwtResultModel> InternalToJwt(JwtModel data, DateTime createdAt, DateTime expireAt)
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

            var result = new JwtResultModel
            {
                AccessToken = handler.WriteToken(securityToken),
                Expire = expireAt.TimeStamp(),
                Type = "Bearer"
            };

            return result;
        }
    }
}
