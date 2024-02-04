using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using ApiClubMed.Models;

namespace ApiClubMed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IDataRepository<Restaurant> dataRepository;

        public RestaurantsController(IDataRepository<Restaurant> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Get all the restaurant.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200"></response>
        /// <response code="401"></response>
        // GET: api/Restaurants
        [HttpGet]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
        {
            return await dataRepository.GetAllAsync();
        }

        /// <summary>
        /// Get a single restaurant.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the restaurant</param>
        /// <response code="200">When the restaurant id is found</response>
        /// <response code="401">When the restaurant id is unauthorized</response>
        /// <response code="404">When the restaurant id is not found</response>
        // GET: api/Restaurants/5
        [HttpGet]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
        {
            var restaurant = await dataRepository.GetByIdAsync(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return restaurant;
        }


        //// GET: api/Restaurants/4
        //[HttpGet]
        //[Route("[action]/{libelle}")]
        //[ActionName("GetRestaurantsByResortId")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<Restaurant>> GetRestaurantsByResortId(int id)
        //{
        //    var restaurant = await dataRepository.GetRestaurantByResortId;

        //    if (restaurant == null)
        //    {
        //        return NotFound();
        //    }

        //    return restaurant ;
        //}

        /// <summary>
        /// Get a single restaurant.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="name">The name of the restaurant</param>
        /// <response code="200">When the restaurant name is found</response>
        /// <response code="401">When the restaurant name is unauthorized</response>
        /// <response code="404">When the restaurant name is not found</response>
        // GET: api/Restaurants/GetByName/Unkai
        [HttpGet]
        [Route("[action]/{name}")]
        [ActionName("GetByName")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Restaurant>> GetRestaurantsByName(string name)
        {
            var restaurant = await dataRepository.GetByStringAsync(name);

            if (restaurant == null)
            {
                return NotFound();
            }

            return restaurant;
        }

        /// <summary>
        /// Modify a single restaurant.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="204">When the restaurant and id is valid</response>
        /// <response code="401">When the restaurant and id is unauthorized</response>
        /// <response code="400">When the restaurant id is not the same as id</response>
        /// <response code="404">When id is not valid</response>
        // PUT: api/Restaurants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutRestaurant(int id, Restaurant restaurant)
        {

            if (id != restaurant.RestaurantId)
            {
                return BadRequest();
            }

            var restaurantToUpdate = await dataRepository.GetByIdAsync(id);


            if (restaurantToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(restaurantToUpdate.Value, restaurant);
                return NoContent();
            }
        }

        /// <summary>
        /// Add a single restaurant.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="201">When the restaurant is valid</response>
        /// <response code="401">When the restaurant is unauthorized</response>
        /// <response code="400">When the restaurant is not valid</response>
        // POST: api/Restaurants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Restaurant>> PostRestaurant(Restaurant restaurant)
        {
            await dataRepository.AddAsync(restaurant);

            return CreatedAtAction("GetById", new { id = restaurant.RestaurantId }, restaurant);
        }

        /// <summary>
        /// Delete a single restaurant.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">When the restaurant id is valid</response>
        /// <response code="401">When the restaurant id is unauthorized</response>
        /// <response code="404">When the restaurant id is not valid</response>
        // DELETE: api/Restaurants/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var restaurant = await dataRepository.GetByIdAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(restaurant.Value);

            return NoContent();
        }

        //private bool RestaurantExists(int id)
        //{
        //    return (_context.Restaurants?.Any(e => e.RestaurantId == id)).GetValueOrDefault();
        //}
    }
}
