using Api.GRRInnovations.TodoManager.Domain.Entities;
using Api.GRRInnovations.TodoManager.Infrastructure.Authentication;
using Microsoft.AspNetCore.Http;
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

        public static async Task<JwtModel> JwtInfo(this HttpContext context, bool validateLifeTime = true)
        {
            var user = context.User;
            if (user == null) return null;

            var claimIdentifier = user.Claims.FirstOrDefault(c => c.Type == JwtModel.ClaimUserUid)?.Value;
            if (string.IsNullOrEmpty(claimIdentifier)) return null;

            var userUid = Guid.Parse(claimIdentifier);
            if (userUid == Guid.Empty) return null;

            try
            {
                var token = await context.Request.Jwt();

                var response = await JwtService.FromJwt(token, validateLifeTime).ConfigureAwait(false);
                if (response == null) return null;

                return response;

            }
            catch (Exception ex)
            {
                //to do: tratar
                return null;
            }
        }

        public static async Task<string> Jwt(this HttpRequest request)
        {
            var result = await request.FindKey(AuthorizationKey, AuthorizationQueryKey);

            if (!string.IsNullOrEmpty(result))
            {
                result = result.Replace("Bearer ", "");
            }

            return result;
        }

        public static Task<string> FindKey(this HttpRequest request, string headerKey, string queryKey)
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

            return Task.FromResult(result);
        }
    }
}
