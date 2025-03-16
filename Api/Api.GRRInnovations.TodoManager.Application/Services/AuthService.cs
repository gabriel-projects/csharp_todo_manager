using Api.GRRInnovations.TodoManager.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TodoManager.Application.Services
{
    public class AuthService : IAuthService
    {
        private const string ROUTE_DATA_CONTROLLER_KEY = "controller";
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IActionContextAccessor _actionAccessor;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUrlHelperFactory urlHelperFactory, IHttpContextAccessor httpContextAccessor, IActionContextAccessor actionAccessor, ILogger<AuthService> logger)
        {
            _urlHelperFactory = urlHelperFactory ?? throw new ArgumentNullException(nameof(urlHelperFactory));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _actionAccessor = actionAccessor ?? throw new ArgumentNullException(nameof(actionAccessor));
            _logger = logger;
        }

        public string GenerateRedirectUrl(string action)
        {
            if(string.IsNullOrEmpty(action))
            {
                _logger.LogError("Action name is null or empty.");
                throw new ArgumentException("Action name cannot be null or empty.", nameof(action));
            }

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                _logger.LogError("HttpContext is not available.");
                throw new InvalidOperationException("HttpContext is not available.");
            }

            if (_actionAccessor.ActionContext == null)
            {
                _logger.LogError("ActionContext is not available.");
                throw new InvalidOperationException("ActionContext is not available.");
            }

            if (!_actionAccessor.ActionContext.RouteData.Values.TryGetValue("controller", out var controllerObj) || string.IsNullOrEmpty(controllerObj?.ToString()))
            {
                _logger.LogError("Controller name could not be determined.");
                throw new InvalidOperationException("Controller name could not be determined.");
            }

            var urlHelper = _urlHelperFactory.GetUrlHelper(_actionAccessor.ActionContext);
            var redirectUrl = urlHelper.Action(new UrlActionContext
            {
                Action = action,
                Controller = controllerObj.ToString(),
                Protocol = httpContext.Request.Scheme,
                Host = httpContext.Request.Host.ToUriComponent()
            });

            if (string.IsNullOrEmpty(redirectUrl))
            {
                _logger.LogError("Failed to generate redirect URL.");
                throw new InvalidOperationException("Failed to generate the redirect URL.");
            }

            _logger.LogInformation("Successfully generated redirect URL: {RedirectUrl}", redirectUrl);
            return redirectUrl;
        }
    }
}
