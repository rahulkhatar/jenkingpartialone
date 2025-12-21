using Microsoft.AspNetCore.Identity;

namespace Revision_Part_1.Domain.Entities;

public class User : IdentityUser
{
    public DateOnly? DateOfBirth { get; set; }
    public string Nationality { get; set; } = string.Empty;

    //Identity
    public List<Restaurant> OwnedRestaurant { get; set; } = [];
}
