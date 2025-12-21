using Microsoft.EntityFrameworkCore;
using Revision_Part_1.Domain.Entities;
using Revision_Part_1.Domain.Interfaces;
using Revision_Part_1.Infrastructure.Persistence;

namespace Revision_Part_1.Infrastructure.Repositories;

public class RestaurantServiceRepository(RestaurentsDbContext dbContext) : IRestaurantService
{
    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await dbContext.Restaurants.ToListAsync();
        return restaurants;
    }

    public async Task<Restaurant?> GetRestaurantByIdAsync(int id)
    {
        var restaurant = await dbContext.Restaurants.Include(x => x.Dishes).FirstOrDefaultAsync(x => x.Id == id);
        return restaurant;
    }
    public async Task<int> CreateRestaurant(Restaurant restaurant)
    {
        dbContext.Restaurants.Add(restaurant);
        await dbContext.SaveChangesAsync();
        return restaurant.Id;
    }

    public async Task<bool> DeleteRestaurant(int id)
    {
        var restaurant = await dbContext.Restaurants.FirstOrDefaultAsync(x => x.Id == id);
        if (restaurant is null)
            return false;

        dbContext.Restaurants.Remove(restaurant);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public Task SaveChanges()
     => dbContext.SaveChangesAsync();
}
