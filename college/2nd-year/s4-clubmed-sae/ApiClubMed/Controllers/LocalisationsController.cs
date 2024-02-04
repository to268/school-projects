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
    [Route("api/localisation")]
    [ApiController]
    public class LocalisationsController : ControllerBase
    {
        private readonly IDataRepository<Localisation> dataRepository;
        public LocalisationsController(IDataRepository<Localisation> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Get all the Localisations.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200"></response>
        /// <response code="401"></response>
        // GET: api/Localisations
        [HttpGet]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        public async Task<ActionResult<IEnumerable<Localisation>>> GetLocalisations()
        {
            return await dataRepository.GetAllAsync();
        }

        /// <summary>
        /// Get a single Localisation.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the Localisation</param>
        /// <response code="200">When the Localisation id is found</response>
        /// <response code="401">When the Localisation id is unauthorized</response>
        /// <response code="404">When the Localisation id is not found</response>
        // GET: api/Localisations/GetById/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Localisation>> GetLocalisation(int id)
        {
            var localisation = await dataRepository.GetByIdAsync(id);

            if (localisation == null)
            {
                return NotFound();
            }

            return localisation;
        }

        /// <summary>
        /// Get a single Localisation.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="name">The name of the Localisation</param>
        /// <response code="200">When the Localisation name is found</response>
        /// <response code="401">When the Localisation name is unauthorized</response>
        /// <response code="404">When the Localisation name is not found</response>
        // GET: api/Localisations/GetByName/Chamonix
        [HttpGet]
        [Route("[action]/{name}")]
        [ActionName("GetByName")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Localisation>> GetLocalisationByName(string name)
        {
            var localisation = await dataRepository.GetByStringAsync(name);

            if (localisation == null)
            {
                return NotFound();
            }

            return localisation;
        }


        /// <summary>
        /// Modify a single Localisation.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="204">When the Localisation and id is valid</response>
        /// <response code="401">When the Localisation and id is unauthorized</response>
        /// <response code="400">When the Localisation id is not the same as id</response>
        /// <response code="404">When id is not valid</response>
        // PUT: api/Localisations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutLocalisation(int id, Localisation localisation)
        {
            if (id != localisation.LocalisationId)
            {
                return BadRequest();
            }

            var localisationToUpdate = await dataRepository.GetByIdAsync(id);
            if (localisationToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(localisationToUpdate.Value, localisation);
                return NoContent();
            }
        }

        /// <summary>
        /// Add a single Localisation.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="201">When the Localisation is valid</response>
        /// <response code="401">When the Localisation is unauthorized</response>
        /// <response code="400">When the Localisation is not valid</response>
        // POST: api/Localisations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Localisation>> PostLocalisation(Localisation localisation)
        {
            await dataRepository.AddAsync(localisation);

            return CreatedAtAction("GetById", new { id = localisation.LocalisationId }, localisation);
        }

        /// <summary>
        /// Delete a single Localisation.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">When the Localisation id is valid</response>
        /// <response code="401">When the Localisation id is unauthorized</response>
        /// <response code="404">When the Localisation id is not valid</response>
        // DELETE: api/Localisations/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteLocalisation(int id)
        {
            var localisation = await dataRepository.GetByIdAsync(id);
            if (localisation == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(localisation.Value);

            return NoContent();
        }

        //private bool LocalisationExists(int id)
        //{
        //    return (_context.Localisations?.Any(e => e.LocalisationId == id)).GetValueOrDefault();
        //}
    }
}
