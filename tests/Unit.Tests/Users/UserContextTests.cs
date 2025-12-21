using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Revision_Part_1.Application.User;
using System.Security.Claims;

namespace Unit.Tests.Users;

public class UserContextTests
{
    [Fact()]
    public void GetCurrentUserTest_WithAuthenticatedUser_ShouldReturnCurrentUser()
    {
        //Arrange
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

        var dateOfBirth = new DateOnly(1990, 1, 1);
        var claims = new List<Claim>()
        {
            new (ClaimTypes.NameIdentifier, "1"),
            new (ClaimTypes.Email, "test@test.com"),
            new (ClaimTypes.Role, "Admin"),
            new (ClaimTypes.Role, "User"),
            new ("Nationality", "German"),
            new ("DateOfBirth", dateOfBirth.ToString("yyyy-MM-dd"))
        };

        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));
        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
        {
            User = user
        });

        var userContext = new UserContext(httpContextAccessorMock.Object);

        //Act
        var currentUser = userContext.GetCurrent();

        //Assert
        currentUser.Should().NotBeNull();
        currentUser!.Id.Should().Be("1");
        currentUser.Email.Should().Be("test@test.com");
        currentUser.Roles.Should().ContainInOrder( "Admin", "User");
        currentUser.Nationality.Should().Be("German");
        currentUser.DateOfBirth.Should().Be(dateOfBirth);
    }

    [Fact()]
    public void GetCurrentUserTest_WithUserContextNotPresent_ThrowInvalidOperationException()
    {
        //Arrange
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null);

        var userContext = new UserContext(httpContextAccessorMock.Object);

        //Act
        Action action = () => userContext.GetCurrent();

        //Assert
        action.Should().Throw<InvalidOperationException>().WithMessage("User context is not present.");
    }
}
