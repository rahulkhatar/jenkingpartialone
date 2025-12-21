using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Moq;
using Revision_Part_1.Application.Interfaces;
using Revision_Part_1.Application.User;
using Revision_Part_1.Domain.Entities;
using Revision_Part_1.Domain.Interfaces;
using Revision_Part_1.Infrastructure.Authorization;

namespace Unit.Tests.Middlewares;

public class CreateMultipleRestaurantRequirementHandlerTests
{
    [Fact()]
    public async Task HandleRequirementAsync_UserHasCreatedMultipleRestaurants_ShouldSucceed()
    {
        //Arrange
        var userContextMock = new Mock<IUserContext>();

        var currentUser = new Current("1", "test@example.com", [], null, null);
        userContextMock.Setup(uc => uc.GetCurrent()).Returns(currentUser);

        var restaurants = new List<Restaurant>
        {
            new()
            {
                OwnerId = currentUser.Id,
            },
            new()
            {
                OwnerId = currentUser.Id,
            },
            new()
            {
                OwnerId = "2",
            }
        };

        var restaurantServiceMock = new Mock<IRestaurantService>();
        restaurantServiceMock.Setup(r => r.GetAllAsync()).ReturnsAsync(restaurants);

        var requirement = new CreatedMultipleRestaurantsRequirement(2);
        var handler = new CreatedMultipleRestaurantsRequirementHandler(restaurantServiceMock.Object, userContextMock.Object);
        var context = new AuthorizationHandlerContext([requirement], null, null);
        
        //Act
        await handler.HandleAsync(context);

        //Assert
        context.HasSucceeded.Should().BeTrue();
    }
}
