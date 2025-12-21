using Revision_Part_1.Domain.Entities;

namespace Revision_Part_1.Domain.Interfaces;

public interface IRestaurantService
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<Restaurant?> GetRestaurantByIdAsync(int id);
    Task<int> CreateRestaurant(Restaurant restaurant);
    Task<bool> DeleteRestaurant(int id);
    Task SaveChanges();
}
