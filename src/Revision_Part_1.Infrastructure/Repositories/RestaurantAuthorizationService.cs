using Microsoft.Extensions.Logging;
using Revision_Part_1.Application.Interfaces;
using Revision_Part_1.Domain.Entities;
using Revision_Part_1.Domain.Interfaces;

namespace Revision_Part_1.Infrastructure.Repositories;

public class RestaurantAuthorizationService(ILogger<RestaurantAuthorizationService> logger,
    IUserContext userContext) : IRestaurantAuthorizationService
{
    public bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation)
    {
        var user = userContext.GetCurrent();

        logger.LogInformation("Authorizing user {UserEmail}, to {Operation} for restaurant {RestaurantName}",
            user?.Email,
            resourceOperation,
            restaurant.Name);

        if (resourceOperation == ResourceOperation.Read || resourceOperation == ResourceOperation.Create)
        {
            logger.LogInformation("Create/read operation - successful authorization");
            return true;
        }

        if (resourceOperation == ResourceOperation.Delete && user!.IsInRole("Admin"))
        {
            logger.LogInformation("Admin user, delete operation - successful authorization");
            return true;
        }

        if ((resourceOperation == ResourceOperation.Delete || resourceOperation == ResourceOperation.Update)
            && user?.Id == restaurant.OwnerId)
        {
            logger.LogInformation("Restaurant owner - successful authorization");
            return true;
        }

        return false;
    }
}
