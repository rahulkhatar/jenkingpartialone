using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Revision_Part_1.Application.DTOs;
using Revision_Part_1.Domain.Entities;
using Revision_Part_1.Domain.Exceptions;
using Revision_Part_1.Domain.Interfaces;

namespace Revision_Part_1.Application.CQRS.Queries;

public class GetAllDishQuery(int restaurantId) : IRequest<IEnumerable<DishDto>>
{
    public int RestaurantId { get; } = restaurantId;
}

public class GetAllDishQueryHandler(ILogger<GetAllDishQueryHandler> logger,
    IRestaurantService restaurantsRepository,
    IMapper mapper) : IRequestHandler<GetAllDishQuery, IEnumerable<DishDto>>
{
    public async Task<IEnumerable<DishDto>> Handle(GetAllDishQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving dishes for restaurant with id: {RestaurantId}", request.RestaurantId);
        var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(request.RestaurantId);

        if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var results = mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);

        return results;
    }
}
