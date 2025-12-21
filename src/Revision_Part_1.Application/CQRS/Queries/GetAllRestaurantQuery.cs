using AutoMapper;
using MediatR;
using Revision_Part_1.Application.CQRS.Commands;
using Revision_Part_1.Application.DTOs;
using Revision_Part_1.Domain.Interfaces;

namespace Revision_Part_1.Application.CQRS.Queries;

public class GetAllRestaurantQuery : IRequest<IEnumerable<RestaurantDto>>
{
}

public class GetAllRestaurantQueryHandler(IRestaurantService _restaurantService, IMapper _mapper) : IRequestHandler<GetAllRestaurantQuery, IEnumerable<RestaurantDto>>
{
    public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantQuery request, CancellationToken cancellation)
    {
        var restaurants = await _restaurantService.GetAllAsync();

        var restaurantsDto = _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
        return restaurantsDto!;
    }
}
