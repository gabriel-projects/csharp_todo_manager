using Api.GRRInnovations.TodoManager.Application.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TodoManager.Application.Services
{
    public class AuthManager : IAuthManager
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthManager> _logger;

        public AuthManager(IAuthService authService, ILogger<AuthManager> logger)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IActionResult HandleLogin(string provider, string responseAction)
        {
            try
            {
                var redirectUrl = _authService.GenerateRedirectUrl(responseAction);
                _logger.LogInformation("Redirecting user to {Provider} sign-in.", provider);
                return new ChallengeResult(provider, new AuthenticationProperties { RedirectUri = redirectUrl });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while processing {Provider} login.", provider);
                return new StatusCodeResult(500);
            }
        }
    }
}
