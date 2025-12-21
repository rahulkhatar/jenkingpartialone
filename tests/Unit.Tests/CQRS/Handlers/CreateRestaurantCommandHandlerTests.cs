using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Revision_Part_1.Application.CQRS.Commands;
using Revision_Part_1.Application.Interfaces;
using Revision_Part_1.Application.User;
using Revision_Part_1.Domain.Entities;
using Revision_Part_1.Domain.Interfaces;

namespace Unit.Tests.CQRS.Handlers;

public class CreateRestaurantCommandHandlerTests
{
    [Fact()]
    public async Task Handle_ForValidCommand_ReturnsCreateRestaurant()
    {
        //Arrange Constructor
        var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();
        var mapperMock = new Mock<IMapper>();
        var restaurantRepositoryMock = new Mock<IRestaurantService>();
        var userContextMock = new Mock<IUserContext>();

        restaurantRepositoryMock.Setup(repo => repo.CreateRestaurant(It.IsAny<Restaurant>()))
            .ReturnsAsync(1);

        var currentUser = new Current("owner-id", "test@test.com", [], null, null);
        userContextMock.Setup(user => user.GetCurrent()).Returns(currentUser);

        var commandHandler = new CreateRestaurantCommandHandler(loggerMock.Object, restaurantRepositoryMock.Object, mapperMock.Object, userContextMock.Object);

        var command = new CreateRestaurantCommand();
        var restaurant = new Restaurant();
        mapperMock.Setup(m => m.Map<Restaurant>(command.CreateRestaurantDto)).Returns(restaurant);

        //Act
        var result = await commandHandler.Handle(command, CancellationToken.None);

        //Assert
        result.Should().Be(1);
        restaurant.OwnerId.Should().Be("owner-id");
        restaurantRepositoryMock.Verify(repo => repo.CreateRestaurant(restaurant), Times.Once);
    }
}
