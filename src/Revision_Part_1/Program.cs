using Revision_Part_1.Infrastructure.Seeders;
using Revision_Part_1.Infrastructure.Extensions;
using Revision_Part_1.Application.Extensions;
using Serilog;
using Revision_Part_1.Middlewares;
using Revision_Part_1.Domain.Entities;
using Revision_Part_1.Extensions;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.AddPresentation();

    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddApplication();

    var app = builder.Build();

    var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
    await seeder.Seed();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseSerilogRequestLogging();
    app.UseMiddleware<ExceptionHandling>();
    app.UseMiddleware<RequestTimeLogging>();

    app.UseHttpsRedirection();

    //Identity
    app.MapGroup("api/identity").WithTags("Identity").MapIdentityApi<User>();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program { }
