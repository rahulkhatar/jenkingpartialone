using FluentAssertions;
using Revision_Part_1.Application.User;
using Xunit;

namespace Unit.Tests.Users;

public class CurrentUserTests
{
    //[Fact()]
    [Theory()]
    [InlineData("Admin")]
    [InlineData("User")]
    public void IsInRole_WithMatchingRole_ShouldReturnTrue(string roleName)
    {
        //Arrange
        var currentUser = new Current("1", "test@test.com", ["Admin", "User"], null, null);

        //Act
        var isInRole = currentUser.IsInRole(roleName);

        //Assert
        isInRole.Should().BeTrue();
    }

    [Fact()]
    public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
    {
        //Arrange
        var currentUser = new Current("1", "test@test.com", ["Admin", "User"], null, null);

        //Act
        var isInRole = currentUser.IsInRole("Owner");

        //Assert
        isInRole.Should().BeFalse();
    }

    [Fact()]
    public void IsInRole_WithNoMatchingRoleCase_ShouldReturnFalse()
    {
        //Arrange
        var currentUser = new Current("1", "test@test.com", ["Admin", "User"], null, null);

        //Act
        var isInRole = currentUser.IsInRole("owner");

        //Assert
        isInRole.Should().BeFalse();
    }
}
