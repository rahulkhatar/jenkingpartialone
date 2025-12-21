using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Revision_Part_1.Application.CQRS.Commands;
using Revision_Part_1.Application.CQRS.Queries;
using Revision_Part_1.Application.DTOs;
using Revision_Part_1.Infrastructure.Constants;

namespace Revision_Part_1.Controllers;

[Route("api/[controller]")]
[ApiController]
/*public class RestaurantsController(RestaurantServiceAdditionalLayer _additionalRestaurantService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var restaurants = await _additionalRestaurantService.GetAllRestaurants();
        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var restaurant = await _additionalRestaurantService.GetRestaurantById(id);
        if (restaurant is null)
            return NotFound();

        return Ok(restaurant);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantDto createRestaurantDto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        int id = await _additionalRestaurantService.CreateRestaurant(createRestaurantDto);
        return CreatedAtAction(nameof(GetById), new { id = id }, null);
    }
    Comment 1
}*/
public class RestaurantsController(IMediator _additionalRestaurantService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RestaurantDto>))]
    //[Authorize(Roles = "User")]

    //[Authorize(Policy = PolicyNames.Atleast20)]
    public async Task<IActionResult> GetAll()
    {
        //var userId = User.Claims.FirstOrDefault(c => c.Type == "user_id")!.Value;
        var restaurants = await _additionalRestaurantService.Send(new GetAllRestaurantQuery());
        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    //[Authorize(Policy = "HasNationality")]
    public async Task<IActionResult> GetById(int id)
    {
        var restaurant = await _additionalRestaurantService.Send(new GetRestaurantByIdQuery() { Id = id });
        if (restaurant is null)
            return NotFound();

        return Ok(restaurant);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantDto createRestaurantDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        int id = await _additionalRestaurantService.Send(new CreateRestaurantCommand() { CreateRestaurantDto = createRestaurantDto });
        return CreatedAtAction(nameof(GetById), new { id = id }, null);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
    {
        var restaurant = await _additionalRestaurantService.Send(new DeleteRestaurantCommand() { Id = id });
        if (!restaurant)
            return NotFound();

        return Ok(restaurant);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRestaurant([FromRoute] int id, UpdateRestaurantCommand command)
    {
        command.Id = id;
        await _additionalRestaurantService.Send(command);

        //if (isUpdated)
        return NoContent();

        //return NotFound();
    }

    //BlobStorage
    [HttpPost("{id}/logo")]
    public async Task<IActionResult> UploadLogo([FromRoute] int id, IFormFile file)
    {
        using var stream = file.OpenReadStream();
        
        var command = new UploadRestaurantLogoCommand()
        {
            RestaurantId = id,
            FileName = file.FileName,
            File = stream
        };

        await _additionalRestaurantService.Send(command);
        return NoContent();
    }
}