using MediatR;
using Microsoft.Extensions.Logging;
using Revision_Part_1.Domain.Entities;
using Revision_Part_1.Domain.Exceptions;
using Revision_Part_1.Domain.Interfaces;

namespace Revision_Part_1.Application.CQRS.Commands;

public class DeleteDishesCommand(int restaurantId) : IRequest
{
    public int RestaurantId { get; } = restaurantId;
}

public class DeleteDishesCommandHandler(ILogger<DeleteDishesCommandHandler> logger,
    IRestaurantService restaurantsRepository,
    IDishService dishesRepository) : IRequestHandler<DeleteDishesCommand>
{
    public async Task Handle(DeleteDishesCommand request, CancellationToken cancellationToken)
    {
        logger.LogWarning("Removing all dishes from restaurant: {RestaurantId}", request.RestaurantId);

        var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(request.RestaurantId);
        if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        await dishesRepository.Delete(restaurant.Dishes);
    }
}
