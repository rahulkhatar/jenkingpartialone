using Microsoft.AspNetCore.Authorization;

namespace Revision_Part_1.Infrastructure.Authorization;

public class CreatedMultipleRestaurantsRequirement(int minimumRestaurantCreated) : IAuthorizationRequirement
{
    public int MinimumRestaurantCreated { get; } = minimumRestaurantCreated;
}
