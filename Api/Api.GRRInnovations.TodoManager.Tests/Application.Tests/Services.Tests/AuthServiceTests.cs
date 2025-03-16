using Api.GRRInnovations.TodoManager.Application.Services;
using Api.GRRInnovations.TodoManager.Interfaces.Repositories;
using Api.GRRInnovations.TodoManager.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TodoManager.Tests.Application.Tests.Services.Tests
{
    public class AuthServiceTests
    {
        private readonly Mock<IUrlHelperFactory> _mockUrlHelperFactory;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly Mock<IActionContextAccessor> _mockActionContextAccessor;
        private readonly Mock<ILogger<AuthService>> _mockLogger;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _mockUrlHelperFactory = new Mock<IUrlHelperFactory>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockActionContextAccessor = new Mock<IActionContextAccessor>();
            _mockLogger = new Mock<ILogger<AuthService>>();

            _authService = new AuthService(
                _mockUrlHelperFactory.Object,
                _mockHttpContextAccessor.Object,
                _mockActionContextAccessor.Object,
                _mockLogger.Object);
        }

        [Fact]
        public void GenerateRedirectUrl_ShouldThrowException_WhenActionIsNullOrEmpty()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _authService.GenerateRedirectUrl(null));
            Assert.Throws<ArgumentException>(() => _authService.GenerateRedirectUrl(""));
        }

        [Fact]
        public void GenerateRedirectUrl_ShouldThrowException_WhenHttpContextIsNull()
        {
            // Arrange
            _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns((HttpContext)null);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => _authService.GenerateRedirectUrl("Index"));
            Assert.Equal("HttpContext is not available.", exception.Message);
        }

        [Fact]
        public void GenerateRedirectUrl_ShouldThrowException_WhenActionContextIsNull()
        {
            // Arrange
            var mockHttpContext = new DefaultHttpContext();
            _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(mockHttpContext);
            _mockActionContextAccessor.Setup(x => x.ActionContext).Returns((ActionContext)null);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => _authService.GenerateRedirectUrl("Index"));
            Assert.Equal("ActionContext is not available.", exception.Message);
        }

        [Fact]
        public void GenerateRedirectUrl_ShouldThrowException_WhenControllerNameIsNotDetermined()
        {
            // Arrange
            var mockHttpContext = new DefaultHttpContext();
            _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(mockHttpContext);
            var actionContext = new ActionContext { RouteData = new Microsoft.AspNetCore.Routing.RouteData() };
            _mockActionContextAccessor.Setup(x => x.ActionContext).Returns(actionContext);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => _authService.GenerateRedirectUrl("Index"));
            Assert.Equal("Controller name could not be determined.", exception.Message);
        }

        [Fact]
        public void GenerateRedirectUrl_ShouldReturnValidUrl()
        {
            // Arrange
            var mockHttpContext = new DefaultHttpContext();
            mockHttpContext.Request.Scheme = "https";
            mockHttpContext.Request.Host = new HostString("example.com");
            _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(mockHttpContext);

            var actionContext = new ActionContext { RouteData = new Microsoft.AspNetCore.Routing.RouteData() };
            actionContext.RouteData.Values["controller"] = "Home";
            _mockActionContextAccessor.Setup(x => x.ActionContext).Returns(actionContext);

            var mockUrlHelper = new Mock<IUrlHelper>();
            mockUrlHelper
                .Setup(x => x.Action(It.IsAny<UrlActionContext>()))
                .Returns("https://example.com/Home/Index");
            _mockUrlHelperFactory.Setup(x => x.GetUrlHelper(actionContext)).Returns(mockUrlHelper.Object);

            // Act
            var result = _authService.GenerateRedirectUrl("Index");

            // Assert
            Assert.Equal("https://example.com/Home/Index", result);
        }
    }
}
