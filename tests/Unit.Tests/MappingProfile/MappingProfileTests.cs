using AutoMapper;
using Revision_Part_1.Domain.Entities;
using Revision_Part_1.Application.DTOs;
using FluentAssertions;
using Revision_Part_1.Application.CQRS.Commands;

namespace Unit.Tests.MappingProfile;

public class MappingProfileTests
{
    private readonly IMapper _mapper;
    public MappingProfileTests()
    {
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<Revision_Part_1.Application.MappingProfile>();
        }).CreateMapper();
    }

    [Fact()]
    public void CreateMap_ForRestaurantToDto_Maps()
    {
        //Arrange        
        var restaurant = new Restaurant
        {
            Id = 1,
            Name = "Test Restaurant",
            Description = "A test restaurant",
            Category = "Italian",
            HasDelivery = true,
            ContactEmail = "test@example.com",
            Address = new Address
            {
                Street = "123 Test St",
                City = "Testville",
                PostalCode = "12345"
            },
        };

        //Act
        var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

        //Assert
        restaurantDto.Should().NotBeNull();
        restaurantDto.Id.Should().Be(restaurant.Id);
        restaurantDto.Name.Should().Be(restaurant.Name);
        restaurantDto.Description.Should().Be(restaurant.Description);
        restaurantDto.Category.Should().Be(restaurant.Category);
        restaurantDto.HasDelivery.Should().Be(restaurant.HasDelivery);
        restaurantDto.Street.Should().Be(restaurant.Address?.Street);
        restaurantDto.City.Should().Be(restaurant.Address?.City);
        restaurantDto.PostalCode.Should().Be(restaurant.Address?.PostalCode);
    }

    [Fact()]
    public void CreateMap_ForRestaurantCommandToRestaurant_Maps()
    {
        //Arrange
        var command = new CreateRestaurantCommand()
        {
            CreateRestaurantDto = new CreateRestaurantDto
            {
                Name = "Test Restaurant",
                Description = "Desc",
                Category = "Italian",
                HasDelivery = true,
                ContactEmail = "test@test.com",
                ContactNumber = "123456789",
                City = "Test City",
                Street = "Test Street",
                PostalCode = "12-345"                
            }
        };

        //Act
        var restaurant = _mapper.Map<Restaurant>(command.CreateRestaurantDto);

        //Assert
        restaurant.Should().NotBeNull();
        restaurant.Name.Should().Be(command.CreateRestaurantDto.Name);
        restaurant.Description.Should().Be(command.CreateRestaurantDto.Description);
        restaurant.Category.Should().Be(command.CreateRestaurantDto.Category);
        restaurant.HasDelivery.Should().Be(command.CreateRestaurantDto.HasDelivery);
        restaurant.ContactEmail.Should().Be(command.CreateRestaurantDto.ContactEmail);        
    }
}
