using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Revision_Part_1.Domain.Entities;
using Revision_Part_1.Domain.Interfaces;
using Revision_Part_1.Infrastructure.Authorization;
using Revision_Part_1.Infrastructure.Constants;
using Revision_Part_1.Infrastructure.Helper;
using Revision_Part_1.Infrastructure.Persistence;
using Revision_Part_1.Infrastructure.Repositories;
using Revision_Part_1.Infrastructure.Seeders;

namespace Revision_Part_1.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RestaurentsDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("RestaurantsDb")).EnableSensitiveDataLogging());

        services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
        services.AddScoped<IRestaurantService, RestaurantServiceRepository>();
        services.AddScoped<IDishService, DishServiceRepository>();

        //Identity
        services.AddIdentityApiEndpoints<User>().AddRoles<IdentityRole>().AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>().AddEntityFrameworkStores<RestaurentsDbContext>();

        //Policy
        services.AddAuthorizationBuilder()
            .AddPolicy(PolicyNames.HasNationality, builder => builder.RequireClaim(AppClaimTypes.Nationality, "German", "Polish"))
            .AddPolicy(PolicyNames.Atleast20, builder => builder.AddRequirements(new MinimumAgeRequirement(20)))
            .AddPolicy(PolicyNames.CreatedAtleast2Restaurants, builder => builder.AddRequirements(new CreatedMultipleRestaurantsRequirement(2)));

        services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
        services.AddScoped<IAuthorizationHandler, CreatedMultipleRestaurantsRequirementHandler>();

        services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();

        //BlobStorage Configuration Settings from appsettings.json
        services.Configure<BlobStorageSettings>(configuration.GetSection("BlobStorage"));
        services.AddScoped<IBlobStorageService, BlobStorageService>();
    }
}
