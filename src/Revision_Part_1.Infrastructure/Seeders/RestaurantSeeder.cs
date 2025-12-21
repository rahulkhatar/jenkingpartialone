using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Revision_Part_1.Domain.Entities;
using Revision_Part_1.Infrastructure.Persistence;

namespace Revision_Part_1.Infrastructure.Seeders;

public class RestaurantSeeder(RestaurentsDbContext dbContext) : IRestaurantSeeder
{
    public async Task Seed()
    {
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            await dbContext.Database.MigrateAsync();
        }
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!await dbContext.Restaurants.AnyAsync())
            {
                var restaurants = GetRestaurants();
                dbContext.Restaurants.AddRange(restaurants);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Roles.Any())
            {
                var roles = GetRoles();
                dbContext.Roles.AddRange(roles);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<Restaurant> GetRestaurants()
    {
        User owner = new User()
        {
            Email = "seed-user@test.com"
        };
        List<Restaurant> restaurants = [
            new Restaurant(){
                Owner = owner,
                Name = "KFC",
                Category = "Fast Food",
                Description = "Famous for its fried chicken.",
                ContactEmail = "smartyspy07@yahoo.com",
                HasDelivery = true,
                Dishes =
                [
                    new(){
                      Name = "Fried Chicken Bucket",
                      Description = "A bucket of variety of fried chicken",
                      Price = 19.99M,
                    },
                ],
                Address = new()
                {
                    City = "New York",
                    Street = "123 Main St",
                    PostalCode = "10001",
                }
            }
        ];

        return restaurants;
    }

    private IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles = [
            new IdentityRole()
            {
                Name = "User",
                NormalizedName = "USER"
            },
            new IdentityRole()
            {
                Name = "Owner",
                NormalizedName = "OWNER"
            },
            new IdentityRole()
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            }
        ];

        return roles;
    }
}
