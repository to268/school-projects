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
    [Route("api/souslocalisation")]
    [ApiController]
    public class SousLocalisationsController : ControllerBase
    {
        private readonly IDataRepository<SousLocalisation> dataRepository;
        public SousLocalisationsController(IDataRepository<SousLocalisation> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Get all the Sous Localisation.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200"></response>
        /// <response code="401"></response>
        // GET: api/SousLocalisations
        [HttpGet]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        public async Task<ActionResult<IEnumerable<SousLocalisation>>> GetSousLocalisations()
        {
            return await dataRepository.GetAllAsync();
        }

        /// <summary>
        /// Get a single Sous Localisation.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the Sous Localisation</param>
        /// <response code="200">When the Sous Localisation id is found</response>
        /// <response code="401">When the Sous Localisation id is unauthorized</response>
        /// <response code="404">When the Sous Localisation id is not found</response>
        // GET: api/SousLocalisations/GetById/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SousLocalisation>> GetSousLocalisation(int id)
        {
            var sousLocalisation = await dataRepository.GetByIdAsync(id);

            if (sousLocalisation == null)
            {
                return NotFound();
            }

            return sousLocalisation;
        }

        /// <summary>
        /// Get a single Sous Localisation.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="libelle">The libelle of the Sous Localisation</param>
        /// <response code="200">When the Sous Localisation libelle is found</response>
        /// <response code="401">When the Sous Localisation libelle is unauthorized</response>
        /// <response code="404">When the Sous Localisation libelle is not found</response>
        // GET: api/SousLocalisations/GetByLibelle/Annecy
        [HttpGet]
        [Route("[action]/{libelle}")]
        [ActionName("GetByLibelle")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SousLocalisation>> GetSousLocalisationByName(string name)
        {
            var sousLocalisation = await dataRepository.GetByStringAsync(name);

            if (sousLocalisation == null)
            {
                return NotFound();
            }

            return sousLocalisation;
        }

        /// <summary>
        /// Modify a single Sous Localisation.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="204">When the Sous Localisation and id is valid</response>
        /// <response code="401">When the Sous Localisation and id is unauthorized</response>
        /// <response code="400">When the Sous Localisation id is not the same as id</response>
        /// <response code="404">When id is not valid</response>
        // PUT: api/SousLocalisations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutSousLocalisation(int id, SousLocalisation sousLocalisation)
        {
            if (id != sousLocalisation.SousLocalisationId)
            {
                return BadRequest();
            }

            var sousLocalisationToUpdate = await dataRepository.GetByIdAsync(id);
            if (sousLocalisationToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(sousLocalisationToUpdate.Value, sousLocalisation);
                return NoContent();
            }
        }


        /// <summary>
        /// Add a single Sous Localisation.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="201">When the Sous Localisation is valid</response>
        /// <response code="401">When the Sous Localisation is unauthorized</response>
        /// <response code="400">When the Sous Localisation is not valid</response>
        // POST: api/SousLocalisations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SousLocalisation>> PostSousLocalisation(SousLocalisation sousLocalisation)
        {
            await dataRepository.AddAsync(sousLocalisation);

            return CreatedAtAction("GetById", new { id = sousLocalisation.SousLocalisationId }, sousLocalisation);
        }

        /// <summary>
        /// Delete a single Sous Localisation.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">When the Sous Localisation id is valid</response>
        /// <response code="401">When the Sous Localisation id is unauthorized</response>
        /// <response code="404">When the Sous Localisation id is not valid</response>
        // DELETE: api/SousLocalisations/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSousLocalisation(int id)
        {
            var sousLocalisation = await dataRepository.GetByIdAsync(id);
            if (sousLocalisation == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(sousLocalisation.Value);

            return NoContent();
        }

        //private bool SousLocalisationExists(int id)
        //{
        //    return (_context.SousLocalisations?.Any(e => e.SousLocalisationId == id)).GetValueOrDefault();
        //}
    }
}
