using AutoMapper;
using Revision_Part_1.Application.DTOs;
using Revision_Part_1.Domain.Interfaces;

namespace Revision_Part_1.Application.Services;
/*
public class RestaurantServiceAdditionalLayer(IRestaurantService _restaurantService, IMapper _mapper)
{
    public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants() 
    {
        var restaurants = await _restaurantService.GetAllAsync();

        //var restaurantsDto = restaurants.Select(RestaurantDto.FromEntity);
        var restaurantsDto = _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
        return restaurantsDto!;
    }

    public async Task<RestaurantDto?> GetRestaurantById(int id)
    {
        var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);

        //var restaurantDto = RestaurantDto.FromEntity(restaurant);
        var restaurantDto = _mapper.Map<RestaurantDto?>(restaurant);
        return restaurantDto;
    }

    public async Task<int> CreateRestaurant(CreateRestaurantDto createRestaurantDto)
    {
        var restaurant = _mapper.Map<Domain.Entities.Restaurant>(createRestaurantDto);
        var restaurantId = await _restaurantService.CreateRestaurant(restaurant);
        return restaurantId;
    }
}
*/