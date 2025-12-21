using MediatR;
using Microsoft.Extensions.Logging;
using Revision_Part_1.Domain.Entities;
using Revision_Part_1.Domain.Exceptions;
using Revision_Part_1.Domain.Interfaces;

namespace Revision_Part_1.Application.CQRS.Commands;

public class UploadRestaurantLogoCommand : IRequest
{
    public int RestaurantId { get; set; }
    public string FileName { get; set; } = default!;
    public Stream File { get; set; } = default!;
}

public class UploadRestaurantLogoCommandHandler(ILogger<UploadRestaurantLogoCommandHandler> _logger, IRestaurantService _restaurantService, 
    //IRestaurantAuthorizationService _restaurantAuthorizationService
    IBlobStorageService _blobStorageService
    ) : IRequestHandler<UploadRestaurantLogoCommand>
{
    public async Task Handle(UploadRestaurantLogoCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Uploading restaurant logo for Id: {RestaurantId}", request.RestaurantId);

        var restaurant = await _restaurantService.GetRestaurantByIdAsync(request.RestaurantId);

        if (restaurant is null)
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        //if(!_restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
        //    throw new ForbiddenAccessException();

        var logoUrl = await _blobStorageService.UploadToBlobAsync(request.File, request.FileName);
        restaurant.LogoUrl = logoUrl;

        await _restaurantService.SaveChanges();
    }
}
