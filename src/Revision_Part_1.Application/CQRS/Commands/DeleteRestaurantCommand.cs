using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Revision_Part_1.Domain.Entities;
using Revision_Part_1.Domain.Exceptions;
using Revision_Part_1.Domain.Interfaces;

namespace Revision_Part_1.Application.CQRS.Commands;

public class DeleteRestaurantCommand : IRequest<bool>
{
    public int Id { get; set; }
}

public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> _logger, IRestaurantService _restaurantService, IRestaurantAuthorizationService _restaurantAuthorizationService) : IRequestHandler<DeleteRestaurantCommand, bool>
{
    public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellation)
    {
        _logger.LogInformation("Deleting restaurant with Id: {RestaurantId}", request.Id);
        var restaurant = await _restaurantService.GetRestaurantByIdAsync(request.Id);
        if (restaurant is null)
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

        if(!_restaurantAuthorizationService.Authorize(restaurant, resourceOperation: ResourceOperation.Delete))
            throw new ForbiddenAccessException();

        bool isDeleted = await _restaurantService.DeleteRestaurant(request.Id);
        return isDeleted;
    }
}
