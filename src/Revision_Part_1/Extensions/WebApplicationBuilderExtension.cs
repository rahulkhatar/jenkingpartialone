using Revision_Part_1.Middlewares;
using Serilog;

namespace Revision_Part_1.Extensions;

public static class WebApplicationBuilderExtension
{
    public static void AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("bearerAuth", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "bearerAuth"
                }
            },
            []
        }
    });
        });

        //Add Middleware
        builder.Services.AddScoped<ExceptionHandling>();
        builder.Services.AddScoped<RequestTimeLogging>();

        //Serilog
        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration
            .ReadFrom.Configuration(context.Configuration);
            /*.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", Serilog.Events.LogEventLevel.Information)
            .WriteTo.File("Logs/Restaurant-API-.log", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
            .WriteTo.Console(outputTemplate: "[{Timestamp:dd-MM-yyyy HH:mm:ss} {Level:u3}] |{SourceContext}|  {Message:lj}{NewLine}{Exception}");*/
        });

    }
}
