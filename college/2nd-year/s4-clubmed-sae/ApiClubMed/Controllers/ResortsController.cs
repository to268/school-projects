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
    [Route("api/resort")]
    [ApiController]
    public class ResortsController : ControllerBase
    {

        private readonly IDataRepository<Resort> dataRepository;
        public ResortsController(IDataRepository<Resort> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Get all the Resorts.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200"></response>
        /// <response code="401"></response>
        // GET: api/Resorts
        [HttpGet]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        public async Task<ActionResult<IEnumerable<Resort>>> GetResorts([FromQuery] uint page = 1)
        {
            Console.WriteLine("page number requested: " + page);
            return await dataRepository.GetPage(page, 12);
        }

        /// <summary>
        /// Get a single Resort.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the Resort</param>
        /// <response code="200">When the Resort id is found</response>
        /// <response code="401">When the Resort id is unauthorized</response>
        /// <response code="404">When the Resort id is not found</response>
        // GET: api/Resorts/GetById/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Resort>> GetResort(int id)
        {
            var resort = await dataRepository.GetByIdAsync(id);

            if (resort == null)
            {
                return NotFound();
            }

            return resort;
        }

        /// <summary>
        /// Get a single Resort.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="name">The name of the Resort</param>
        /// <response code="200">When the Resort name is found</response>
        /// <response code="401">When the Resort name is unauthorized</response>
        /// <response code="404">When the Resort name is not found</response>
        // GET: api/Resorts/GetByName/Rosiere
        [HttpGet]
        [Route("[action]/{name}")]
        [ActionName("GetByName")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Resort>> GetResortByName(string name)
        {
            var resort = await dataRepository.GetByStringAsync(name);

            if (resort == null)
            {
                return NotFound();
            }

            return resort;
        }


        /// <summary>
        /// Modify a single Resort.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="204">When the Resort and id is valid</response>
        /// <response code="401">When the Resort and id is unauthorized</response>
        /// <response code="400">When the Resort id is not the same as id</response>
        /// <response code="404">When id is not valid</response>
        // PUT: api/Resorts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutResort(int id, Resort resort)
        {
            if (id != resort.ResortId)
            {
                return BadRequest();
            }

            var resortToUpdate = await dataRepository.GetByIdAsync(id);
            if (resortToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(resortToUpdate.Value, resort);
                return NoContent();
            }
        }


        /// <summary>
        /// Add a single Resort.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="201">When the Resort is valid</response>
        /// <response code="401">When the Resort is unauthorized</response>
        /// <response code="400">When the Resort is not valid</response>
        // POST: api/Resorts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Resort>> PostResort(Resort resort)
        {
            await dataRepository.AddAsync(resort);

            return CreatedAtAction("GetById", new { id = resort.ResortId }, resort);
        }

        /// <summary>
        /// Delete a single Resort.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">When the Resort id is valid</response>
        /// <response code="401">When the Resort id is unauthorized</response>
        /// <response code="404">When the Resort id is not valid</response>
        // DELETE: api/Resorts/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteResort(int id)
        {
            var resort = await dataRepository.GetByIdAsync(id);
            if (resort == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(resort.Value);

            return NoContent();
        }

        //private bool ResortExists(int id)
        //{
        //    return _context.Resorts.Any(e => e.ResortId == id);
        //}
    }
}
