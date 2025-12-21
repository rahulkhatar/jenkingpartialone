using Revision_Part_1.Domain.Entities;

namespace Revision_Part_1.Domain.Interfaces;

public interface IRestaurantAuthorizationService
{
    bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation);
}
