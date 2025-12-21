using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Revision_Part_1.Application.DTOs;
using Revision_Part_1.Domain.Entities;
using Revision_Part_1.Domain.Exceptions;
using Revision_Part_1.Domain.Interfaces;

namespace Revision_Part_1.Application.CQRS.Queries;

public class GetDishByIdQuery(int restaurantId, int dishId) : IRequest<DishDto>
{
    public int RestaurantId { get; } = restaurantId;
    public int DishId { get; } = dishId;
}

public class GetDishByIdQueryHandler(
    ILogger<GetDishByIdQueryHandler> logger,
    IRestaurantService restaurantsRepository,
    IMapper mapper) : IRequestHandler<GetDishByIdQuery, DishDto>
{
    public async Task<DishDto> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving dish: {DishId}, for restaurant with id: {RestaurantId}",
            request.DishId,
            request.RestaurantId);

        var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(request.RestaurantId);

        if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId);
        if (dish == null) throw new NotFoundException(nameof(Dish), request.DishId.ToString());

        var result = mapper.Map<DishDto>(dish);
        return result;
    }
}
