using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Revision_Part_1.Application.DTOs;
using Revision_Part_1.Domain.Entities;
using Revision_Part_1.Domain.Exceptions;
using Revision_Part_1.Domain.Interfaces;

namespace Revision_Part_1.Application.CQRS.Queries;

public class GetRestaurantByIdQuery : IRequest<RestaurantDto>
{
    public int Id { get; set; }
}

public class GetRestaurantByIdQueryHandler(ILogger<GetRestaurantByIdQueryHandler> _logger, IRestaurantService _restaurantService, IMapper _mapper
  , IBlobStorageService _blobStorageService  
    ) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
{
    public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellation)
    {
        _logger.LogInformation("Deleting restaurant with Id: {RestaurantId}", request.Id);
        var restaurant = await _restaurantService.GetRestaurantByIdAsync(request.Id);
        if(restaurant is null)
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

        var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

        if (restaurantDto.LogoSasUrl != null)
            restaurantDto.LogoSasUrl = _blobStorageService.GetBlobSasUrl(restaurant.LogoUrl);

        return restaurantDto;
    }
}
