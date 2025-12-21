namespace Revision_Part_1.Domain.Entities;

public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public bool HasDelivery { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactNumber { get; set; }
    public Address? Address { get; set; }
    public List<Dish> Dishes { get; set; } = new();

    //Identity
    public User Owner { get; set; } = default!;
    public string OwnerId { get; set; } = default!;
    public string? LogoUrl { get; set; }
}

public class Address
{
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
}

public class Dish
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public int? KiloCalories { get; set; }
    public int RestaurantId { get; set; }
}
