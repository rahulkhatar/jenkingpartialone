using Microsoft.Extensions.DependencyInjection;
using Revision_Part_1.Application.Services;
using FluentValidation.AspNetCore;
using FluentValidation;
using Revision_Part_1.Application.Interfaces;
using Revision_Part_1.Application.User;

namespace Revision_Part_1.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtension).Assembly;
        //services.AddScoped<RestaurantServiceAdditionalLayer>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(applicationAssembly));
        services.AddAutoMapper(applicationAssembly);
        services.AddValidatorsFromAssembly(applicationAssembly).AddFluentValidationAutoValidation();

        services.AddScoped<IUserContext, UserContext>();
        services.AddHttpContextAccessor();
    }
}

