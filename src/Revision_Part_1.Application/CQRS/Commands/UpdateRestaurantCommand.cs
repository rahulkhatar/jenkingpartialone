using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Revision_Part_1.Domain.Entities;
using Revision_Part_1.Domain.Exceptions;
using Revision_Part_1.Domain.Interfaces;

namespace Revision_Part_1.Application.CQRS.Commands;

/*public class UpdateRestaurantCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool HasDelivery { get; set; }
}

public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> _logger, IRestaurantService restaurantsRepository,
    IMapper mapper) : IRequestHandler<UpdateRestaurantCommand, bool>
{
    public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating restaurant with Id: {RestaurantId} and {@UpdateRestaurant}", request.Id, request);
        var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(request.Id);
        if (restaurant is null)
            throw new NotFoundException($"Restaurant with {request.Id} does not exists.....");

        mapper.Map(request, restaurant);
        //restaurant.Name = request.Name;
        //restaurant.Description = request.Description;
        //restaurant.HasDelivery = request.HasDelivery;

        await restaurantsRepository.SaveChanges();
        return true;
    }
}*/

public class UpdateRestaurantCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool HasDelivery { get; set; }
}

public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> _logger, IRestaurantService restaurantsRepository,
    IMapper mapper) : IRequestHandler<UpdateRestaurantCommand>
{
    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating restaurant with Id: {RestaurantId} and {@UpdateRestaurant}", request.Id, request);
        var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(request.Id);
        if (restaurant is null)
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

        mapper.Map(request, restaurant);
        //restaurant.Name = request.Name;
        //restaurant.Description = request.Description;
        //restaurant.HasDelivery = request.HasDelivery;

        await restaurantsRepository.SaveChanges();
    }
}