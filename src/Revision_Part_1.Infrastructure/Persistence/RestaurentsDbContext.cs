using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Revision_Part_1.Domain.Entities;

namespace Revision_Part_1.Infrastructure.Persistence;

/*public class RestaurentsDbContext(DbContextOptions<RestaurentsDbContext> options) : DbContext(options)
{
    public DbSet<Restaurant> Restaurants => Set<Restaurant>();
    public DbSet<Dish> Dishes => Set<Dish>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Restaurant>()
        .OwnsOne(r => r.Address, a =>
        {
            a.Property(p => p.City).IsRequired(false);
            a.Property(p => p.Street).IsRequired(false);
            a.Property(p => p.PostalCode).IsRequired(false);

            // Add 1 required field so EF always creates Address object
            a.Property<string>("AddressMarker")
                .HasColumnName("AddressMarker")
                .HasDefaultValue("X");
        });
        modelBuilder.Entity<Restaurant>().HasMany(r => r.Dishes).WithOne().HasForeignKey(r => r.RestaurantId);
        modelBuilder.Entity<Dish>().Property(d => d.Price).HasPrecision(10, 2);
    }

}*/

public class RestaurentsDbContext(DbContextOptions<RestaurentsDbContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<Restaurant> Restaurants => Set<Restaurant>();
    public DbSet<Dish> Dishes => Set<Dish>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Restaurant>()
        .OwnsOne(r => r.Address, a =>
        {
            a.Property(p => p.City).IsRequired(false);
            a.Property(p => p.Street).IsRequired(false);
            a.Property(p => p.PostalCode).IsRequired(false);

            // Add 1 required field so EF always creates Address object
            a.Property<string>("AddressMarker")
                .HasColumnName("AddressMarker")
                .HasDefaultValue("X");
        });
        modelBuilder.Entity<Restaurant>().HasMany(r => r.Dishes).WithOne().HasForeignKey(r => r.RestaurantId);
        modelBuilder.Entity<Dish>().Property(d => d.Price).HasPrecision(10, 2);

        //Identity
        modelBuilder.Entity<User>().HasMany(o => o.OwnedRestaurant).WithOne(o => o.Owner).HasForeignKey(o => o.OwnerId);
    }

}
