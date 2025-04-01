using Api.GRRInnovations.TodoManager.Infrastructure.Security;
using System.Security.Claims;

namespace Api.GRRInnovations.TodoManager.Tests.Infrastructure.Tests.Security.Tests
{
    public class UserClaimsMapperTests
    {
        [Fact]
        public void Should_Map_Claims_With_GivenName_And_Surname()
        {
            //arrange
            var userClaimsMapper = new UserClaimsMapper();

            //act
            var claims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.GivenName, "John"),
                new Claim(ClaimTypes.Surname, "Doe"),
                new Claim(ClaimTypes.Name, "John Doe"),
            }));
            var userClaimsModel = userClaimsMapper.MapFromClaimsPrincipal(claims);

            //assert
            Assert.NotNull(userClaimsModel);
            Assert.Equal("John", userClaimsModel.FirstName);
            Assert.Equal("Doe", userClaimsModel.LastName);
            Assert.Equal("John Doe", userClaimsModel.Name);
        }
    }
}
