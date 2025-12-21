using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Revision_Part_1.Domain.Entities;
using Revision_Part_1.Domain.Exceptions;
using Revision_Part_1.Domain.Interfaces;

namespace Revision_Part_1.Application.CQRS.Commands;

public class CreateDishCommand : IRequest<int>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }

    public int? KiloCalories { get; set; }
    public int RestaurantId { get; set; }
}

public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger,
    IRestaurantService restaurantsRepository,
    IDishService dishesRepository,
    IMapper mapper) : IRequestHandler<CreateDishCommand, int>
{
    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new dish: {@DishRequest}", request);
        var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(request.RestaurantId);
        if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var dish = mapper.Map<Dish>(request);

        return await dishesRepository.Create(dish);

    }
}

public class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
{
    public CreateDishCommandValidator()
    {
        RuleFor(dish => dish.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price must be a non-negative number.");


        RuleFor(dish => dish.KiloCalories)
            .GreaterThanOrEqualTo(0)
            .WithMessage("KiloCalories must be a non-negative number.");
    }
}
