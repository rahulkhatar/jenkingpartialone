using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Revision_Part_1.Application.DTOs;
using Revision_Part_1.Application.Interfaces;
using Revision_Part_1.Domain.Interfaces;

namespace Revision_Part_1.Application.CQRS.Commands;

public class CreateRestaurantCommand : IRequest<int>
{
    public CreateRestaurantDto? CreateRestaurantDto { get; set; }
}

public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> _logger, IRestaurantService _restaurantService, IMapper _mapper, IUserContext userContext) : IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellation)
    {
        var currentUser = userContext.GetCurrent();

        _logger.LogInformation("{UserEmail} [{UserId}] is creating a new restaurant {@Restaurant}", currentUser!.Email, currentUser.Id, request);

        var restaurant = _mapper.Map<Domain.Entities.Restaurant>(request.CreateRestaurantDto);
        restaurant.OwnerId = currentUser.Id;

        int restaurantId = await _restaurantService.CreateRestaurant(restaurant);
        return restaurantId;
    }
}
