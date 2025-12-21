using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Revision_Part_1.Application.DTOs;
using Revision_Part_1.Domain.Entities;
using Revision_Part_1.Domain.Interfaces;
using System.Net;
using System.Net.Http.Json;

namespace Integration.Tests;

public class RestaurantControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly Mock<IRestaurantService> _restaurantService = new();
    public RestaurantControllerTests(WebApplicationFactory<Program> factory) 
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                services.Replace(ServiceDescriptor.Scoped(typeof(IRestaurantService), _ => _restaurantService.Object));
            });
        });
    }
    
    [Fact]
    public async Task GetAll_ForValidRequest_ReturnsOK()
    {
        //Arrange
        var client = _factory.CreateClient();

        //Act
        var result = await client.GetAsync("api/Restaurants");

        //Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetAll_ForInvalidRequest_ReturnsBadRequest()
    {
        //Arrange
        var client = _factory.CreateClient();

        //Act
        var result = await client.GetAsync("api/restaurants/vdsv");

        //Assert
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }


    //Policy
    [Fact]
    public async Task GetById_ForNonExistingId_ShouldReturn404NotFound()
    {
        //Arrange
        int id = 123;
        _restaurantService.Setup(m => m.GetRestaurantByIdAsync(id)).ReturnsAsync((Restaurant?)null);

        var client = _factory.CreateClient();

        //Act
        var result = await client.GetAsync($"/api/restaurants/{id}");

        //Assert
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetById_ForExistingId_ShouldReturnOK()
    {
        //Arrange
        int id = 123;
        var restaurant = new Restaurant()
        {
            Id = id,
            Name = "Test",
            Description = "Test Description"
        };

        _restaurantService.Setup(m => m.GetRestaurantByIdAsync(id)).ReturnsAsync(restaurant);

        var client = _factory.CreateClient();

        //Act
        var result = await client.GetAsync($"/api/restaurants/{id}");
        var restaurantDto = await result.Content.ReadFromJsonAsync<RestaurantDto>();

        //Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        restaurantDto.Should().NotBeNull();
        restaurantDto.Name.Should().Be("Test");
        restaurantDto.Description.Should().Be("Test Description");

    }
}