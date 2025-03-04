using Api.GRRInnovations.TodoManager.Domain.Entities;
using Api.GRRInnovations.TodoManager.Interfaces.Authentication;
using Api.GRRInnovations.TodoManager.Interfaces.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Extensions
{
    public static class HttpRequestExtension
    {
        public const string AuthorizationKey = "Authorization";
        public const string AuthorizationQueryKey = "access_token";

        public static Task<IUserModel> JwtInfo(this HttpContext context, bool validateLifeTime = true)
        {
            var user = context.User;
            if (user == null) return null;

            var claimIdentifier = user.Claims.FirstOrDefault(c => c.Type == JwtModel.ClaimUserUid)?.Value;
            if (string.IsNullOrEmpty(claimIdentifier)) return null;

            var userUid = Guid.Parse(claimIdentifier);
            if (userUid == Guid.Empty) return null;

            try
            {
                var token = context.Request.Jwt();

                var jwtService = context.RequestServices.GetService<IJwtService>();
                if (jwtService == null) return null;

                IUserModel? response = jwtService.FromJwt(token);
                if (response == null) return null;

                return Task.FromResult(response);

            }
            catch (Exception ex)
            {
                //to do: tratar
                return null;
            }
        }

        public static string Jwt(this HttpRequest request)
        {
            var result = request.FindKey(AuthorizationKey, AuthorizationQueryKey);

            if (!string.IsNullOrEmpty(result))
            {
                result = result.Replace("Bearer ", "");
            }

            return result;
        }

        public static string FindKey(this HttpRequest request, string headerKey, string queryKey)
        {
            string result = null;

            try
            {
                if (!string.IsNullOrEmpty(headerKey))
                {
                    result = request.Headers.Where(p => p.Key.ToLower() == headerKey.ToLower())?.Select(p => p.Value)?.FirstOrDefault();
                }

                if (string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(queryKey))
                {
                    result = request.Query.Where(p => p.Key.ToLower() == queryKey.ToLower())?.Select(p => p.Value)?.FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return result;
        }
    }
}
