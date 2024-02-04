using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using ApiClubMed.Models;
using Microsoft.AspNetCore.Authorization;

namespace ApiClubMed.Controllers
{
    [Route("api/domaineskiable")]
    [ApiController]
    public class DomaineSkiablesController : ControllerBase
    {
        private readonly IDataRepository<DomaineSkiable> dataRepository;
        public DomaineSkiablesController(IDataRepository<DomaineSkiable> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Get all the Domaines Skiables.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200"></response>
        /// <response code="401"></response>
        // GET: api/DomaineSkiables
        [HttpGet]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DomaineSkiable>>> GetDomaineSkiables()
        {
            return await dataRepository.GetAllAsync();
        }


        /// <summary>
        /// Get a single Domaine Skiable.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the Domaine Skiable</param>
        /// <response code="200">When the Domaine Skiable id is found</response>
        /// <response code="401">When the Domaine Skiable id is unauthorized</response>
        /// <response code="404">When the Domaine Skiable id is not found</response>
        // GET: api/DomaineSkiables/GetById/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DomaineSkiable>> GetDomaineSkiable(int id)
        {
            var domaine = await dataRepository.GetByIdAsync(id);

            if (domaine == null)
            {
                return NotFound();
            }

            return domaine;
        }


        /// <summary>
        /// Get a single Domaine Skiable.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="name">The name of the Domaine Skiable</param>
        /// <response code="200">When the Domaine Skiable name is found</response>
        /// <response code="401">When the Domaine Skiable name is unauthorized</response>
        /// <response code="404">When the Domaine Skiable name is not found</response>
        // GET: api/ActiviteEnfantIncluses/GetByName/Manege
        [HttpGet]
        [Route("[action]/{name}")]
        [ActionName("GetByName")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DomaineSkiable>> GetDomaineByName(string name)
        {
            var domaineSkiable = await dataRepository.GetByStringAsync(name);

            if (domaineSkiable == null)
            {
                return NotFound();
            }

            return domaineSkiable;
        }

        /// <summary>
        /// Modify a single Domaine Skiable.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="204">When the Domaine Skiable and id is valid</response>
        /// <response code="401">When the Domaine Skiable and id is unauthorized</response>
        /// <response code="400">When the Domaine Skiable id is not the same as id</response>
        /// <response code="404">When id is not valid</response>
        // PUT: api/DomaineSkiables/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutDomaineSkiable(int id, DomaineSkiable domaineSkiable)
        {
            if (id != domaineSkiable.DomaineSkiableId)
            {
                return BadRequest();
            }

            var domaineToUpdate = await dataRepository.GetByIdAsync(id);
            if (domaineToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(domaineToUpdate.Value, domaineSkiable);
                return NoContent();
            }
        }

        /// <summary>
        /// Add a single Domaine Skiable.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="201">When the Domaine Skiable is valid</response>
        /// <response code="401">When the Domaine Skiable is unauthorized</response>
        /// <response code="400">When the Domaine Skiable is not valid</response>
        // POST: api/DomaineSkiables
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DomaineSkiable>> PostDomaineSkiable(DomaineSkiable domaineSkiable)
        {
            await dataRepository.AddAsync(domaineSkiable);

            return CreatedAtAction("GetById", new { id = domaineSkiable.DomaineSkiableId }, domaineSkiable);
        }


        /// <summary>
        /// Delete a single Domaine Skiable.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">When the Domaine Skiable id is valid</response>
        /// <response code="401">When the Domaine Skiable id is unauthorized</response>
        /// <response code="404">When the Domaine Skiable id is not valid</response>
        // DELETE: api/DomaineSkiables/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDomaineSkiable(int id)
        {
            var domaine = await dataRepository.GetByIdAsync(id);
            if (domaine == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(domaine.Value);

            return NoContent();
        }

        //private bool DomaineSkiableExists(int id)
        //{
        //    return (_context.DomaineSkiables?.Any(e => e.DomaineSkiableId == id)).GetValueOrDefault();
        //}
    }
}
