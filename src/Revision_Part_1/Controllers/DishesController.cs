using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Revision_Part_1.Application.CQRS.Commands;
using Revision_Part_1.Application.CQRS.Queries;
using Revision_Part_1.Application.DTOs;

namespace Revision_Part_1.Controllers
{
    [Route("api/restaurants/{restaurantId}/dishes")]
    [ApiController]
    public class DishesController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, CreateDishCommand command)
        {
            command.RestaurantId = restaurantId;

            var dishId = await mediator.Send(command);
            return CreatedAtAction(nameof(GetByIdForRestaurant), new { restaurantId, dishId }, null);
        }

        [HttpGet]
        //[Authorize(Policy = PolicyNames.AtLeast20)]
        public async Task<ActionResult<IEnumerable<DishDto>>> GetAllForRestaurant([FromRoute] int restaurantId)
        {
            var dishes = await mediator.Send(new GetAllDishQuery(restaurantId));
            return Ok(dishes);
        }

        [HttpGet("{dishId}")]
        public async Task<ActionResult<DishDto>> GetByIdForRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            var dish = await mediator.Send(new GetDishByIdQuery(restaurantId, dishId));
            return Ok(dish);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDishesForRestaurant([FromRoute] int restaurantId)
        {
            await mediator.Send(new DeleteDishesCommand(restaurantId));
            return NoContent();
        }
    }
}
