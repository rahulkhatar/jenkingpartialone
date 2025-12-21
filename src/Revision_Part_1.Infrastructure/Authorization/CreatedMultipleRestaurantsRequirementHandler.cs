using Microsoft.AspNetCore.Authorization;
using Revision_Part_1.Application.Interfaces;
using Revision_Part_1.Domain.Interfaces;

namespace Revision_Part_1.Infrastructure.Authorization;

public class CreatedMultipleRestaurantsRequirementHandler(IRestaurantService _restaurantService, IUserContext _userContext) : AuthorizationHandler<CreatedMultipleRestaurantsRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedMultipleRestaurantsRequirement requirement)
    {
        var currentUser = _userContext.GetCurrent();
        
        var restaurants = await _restaurantService.GetAllAsync();
        
        var userRestaurantCreated = restaurants.Count(r => r.OwnerId == currentUser!.Id);
        if (userRestaurantCreated >= requirement.MinimumRestaurantCreated)
            context.Succeed(requirement);
        else
            context.Fail();
    }
}
