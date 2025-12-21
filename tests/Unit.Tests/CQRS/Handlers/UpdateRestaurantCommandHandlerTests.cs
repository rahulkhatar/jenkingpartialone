using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Moq;
using Revision_Part_1.Application.CQRS.Commands;
using Revision_Part_1.Domain.Entities;
using Revision_Part_1.Domain.Exceptions;
using Revision_Part_1.Domain.Interfaces;
using System.Threading.Tasks;

namespace Unit.Tests.CQRS.Handlers;

public class UpdateRestaurantCommandHandlerTests
{
    private readonly Mock<IRestaurantService> _restaurantServiceMock;
    private readonly Mock<AutoMapper.IMapper> _mapperMock;
    
    private readonly UpdateRestaurantCommandHandler _handler;

    public UpdateRestaurantCommandHandlerTests()
    {
        var _loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();
        _restaurantServiceMock = new Mock<IRestaurantService>();
        _mapperMock = new Mock<AutoMapper.IMapper>();
        
        _handler = new UpdateRestaurantCommandHandler(
            _loggerMock.Object,
            _restaurantServiceMock.Object,
            _mapperMock.Object);
    }

    [Fact()]
    public async Task Handle_WithValidRequest_ShouldUpdateRestaurant()
    {
        var restaurantId = 1;
        var command = new UpdateRestaurantCommand
        {
            Id = restaurantId,
            Name = "Updated Name",
            Description = "Updated Description",
            HasDelivery = true
        };

        var restaurant = new Restaurant()
        {
            Id = restaurantId,
            Name = "Test",
            Description = "Test"
        };

        _restaurantServiceMock.Setup(r => r.GetRestaurantByIdAsync(restaurantId)).ReturnsAsync(restaurant);        

        //Act
        await _handler.Handle(command, CancellationToken.None);

        //Assert
        _restaurantServiceMock.Verify(r => r.SaveChanges(), Times.Once);
        _mapperMock.Verify(r => r.Map(command, restaurant), Times.Once);
    }

    [Fact()]
    public async Task Handle_WithNonExistingRestaurant_ShouldThrowNotFoundException()
    {
        var restaurantId = 2;
        var command = new UpdateRestaurantCommand
        {
            Id = restaurantId,
        };

        _restaurantServiceMock.Setup(r => r.GetRestaurantByIdAsync(restaurantId)).ReturnsAsync((Restaurant?)null);

        //Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        //Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Restaurant with id: {restaurantId} doesn't exists.....");
    }
}
