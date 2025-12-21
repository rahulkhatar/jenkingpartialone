namespace Revision_Part_1.Application.User;

public record Current(string Id, string Email, IEnumerable<string> Roles, string? Nationality, DateOnly? DateOfBirth)
{
    public bool IsInRole(string role) => Roles.Contains(role);
}
