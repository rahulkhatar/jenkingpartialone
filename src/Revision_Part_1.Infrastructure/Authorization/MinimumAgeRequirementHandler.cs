using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Revision_Part_1.Application.Interfaces;

namespace Revision_Part_1.Infrastructure.Authorization;

public class MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger, IUserContext userContext) : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        var currentUser = userContext.GetCurrent();
        var dob = context.User.Claims.FirstOrDefault(c => c.Type == "DateOfBirth")?.Value;
        logger.LogInformation("User: {Email}, DateOfBirth:{DOB} - Handling MinimumAgeRequirement", currentUser?.Email, currentUser?.DateOfBirth);

        if(currentUser?.DateOfBirth == null)
        {
            logger.LogInformation("User's DateOfBirth is null...");
            context.Fail();
            return Task.CompletedTask;
        }

        if(currentUser.DateOfBirth.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today))
        {
            logger.LogInformation("Authorization Succeeded.....");
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }

            context.Fail();
        return Task.CompletedTask;
    }
}
