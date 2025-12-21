using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Revision_Part_1.Domain.Entities;
using Revision_Part_1.Infrastructure.Constants;
using System.Security.Claims;

namespace Revision_Part_1.Infrastructure.Authorization;

public class RestaurantsUserClaimsPrincipalFactory(UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<IdentityOptions> options)
            : UserClaimsPrincipalFactory<User, IdentityRole>(userManager, roleManager, options)
{
    public override async Task<ClaimsPrincipal> CreateAsync(User user)
    {
        var id = await GenerateClaimsAsync(user);

        if (user.Nationality != null)
        {
            id.AddClaim(new Claim(AppClaimTypes.Nationality, user.Nationality));
        }

        if (user.DateOfBirth != null)
        {
            id.AddClaim(new Claim(AppClaimTypes.DateOfBirth, user.DateOfBirth.Value.ToString("yyyy-MM-dd")));
        }

        return new ClaimsPrincipal(id);

    }
}
