using Api.GRRInnovations.TodoManager.Application.Interfaces;
using Api.GRRInnovations.TodoManager.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TodoManager.Tests.Application.Tests.Services.Tests
{
    public class AuthManagerTests
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly Mock<ILogger<AuthManager>> _loggerMock;
        private readonly AuthManager _authManager;

        public AuthManagerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _loggerMock = new Mock<ILogger<AuthManager>>();
            _authManager = new AuthManager(_authServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void HandleLogin_ShouldReturnChallengeResult_WhenValidProviderAndAction()
        {
            // Arrange
            var provider = "Google";
            var responseAction = "/home";
            var expectedRedirectUrl = "https://redirect.com";

            _authServiceMock.Setup(a => a.GenerateRedirectUrl(responseAction))
                        .Returns(expectedRedirectUrl);

            // Act
            var result = _authManager.HandleLogin(provider, responseAction);

            // Assert
            Assert.IsType<ChallengeResult>(result);

            var challengeResult = result as ChallengeResult;
            Assert.NotNull(challengeResult);
            Assert.NotNull(challengeResult.Properties);
            Assert.Equal(expectedRedirectUrl, challengeResult.Properties.RedirectUri);
        }

        [Fact]
        public void HandleLogin_ShouldReturn500Status_WhenExceptionOccurs()
        {
            // Arrange
            var provider = "Google";
            var responseAction = "/home";

            _authServiceMock.Setup(a => a.GenerateRedirectUrl(responseAction))
                            .Throws(new Exception("Test Exception"));

            // Act
            var result = _authManager.HandleLogin(provider, responseAction);

            // Assert
            Assert.IsType<StatusCodeResult>(result);

            var statusCodeResult = result as StatusCodeResult;
            Assert.NotNull(statusCodeResult);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public void HandleLogin_ShouldLogInformation_WhenValidRequest()
        {
            // Arrange
            var provider = "Google";
            var responseAction = "/home";
            var expectedRedirectUrl = "https://redirect.com";

            _authServiceMock.Setup(a => a.GenerateRedirectUrl(responseAction))
                            .Returns(expectedRedirectUrl);

            // Act
            _authManager.HandleLogin(provider, responseAction);

            // Assert (Verifica se LogInformation foi chamado)
            _loggerMock.Verify(
                logger => logger.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((state, _) => state.ToString().Contains("Redirecting user to Google sign-in.")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public void HandleLogin_ShouldLogError_WhenExceptionOccurs()
        {
            // Arrange
            var provider = "Google";
            var responseAction = "/home";

            var exception = new Exception("Test Exception");
            _authServiceMock.Setup(a => a.GenerateRedirectUrl(responseAction))
                            .Throws(exception);

            // Act
            _authManager.HandleLogin(provider, responseAction);

            // Assert (Verifica se LogError foi chamado)
            _loggerMock.Verify(
                logger => logger.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((state, _) => state.ToString().Contains("Unexpected error while processing Google login.")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }
    }
}
