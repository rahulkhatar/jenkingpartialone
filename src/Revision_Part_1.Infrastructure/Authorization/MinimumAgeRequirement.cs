using Microsoft.AspNetCore.Authorization;

namespace Revision_Part_1.Infrastructure.Authorization;

public class MinimumAgeRequirement(int minimumAge) : IAuthorizationRequirement
{
    public int MinimumAge { get; } = minimumAge;
}
