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
    [Route("api/activiteenfantincluse")]
    [ApiController]
    public class ActiviteEnfantInclusesController : ControllerBase
    {
        private readonly IDataRepository<ActiviteEnfantIncluse> dataRepository;
        public ActiviteEnfantInclusesController(IDataRepository<ActiviteEnfantIncluse> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Get all the Activités Enfants Incluses.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200"></response>
        /// <response code="401"></response>
        // GET: api/ActiviteEnfantIncluses
        [HttpGet]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        public async Task<ActionResult<IEnumerable<ActiviteEnfantIncluse>>> GetActiviteEnfantIncluses()
        {
            return await dataRepository.GetAllAsync();
        }

        /// <summary>
        /// Get a single Activité Enfant Incluse.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the Activité Enfant Incluse</param>
        /// <response code="200">When the Activité Enfant Incluse id is found</response>
        /// <response code="401">When the Activité Enfant Incluse id is unauthorized</response>
        /// <response code="404">When the Activité Enfant Incluse id is not found</response>
        // GET: api/ActiviteEnfantIncluses/GetById/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ActiviteEnfantIncluse>> GetActiviteEnfantIncluse(int id)
        {
            var activite = await dataRepository.GetByIdAsync(id);

            if (activite == null)
            {
                return NotFound();
            }

            return activite;
        }

        /// <summary>
        /// Get a single Activité Enfant Incluse.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="title">The title of the Activité Enfant Incluse</param>
        /// <response code="200">When the Activité Enfant Incluse title is found</response>
        /// <response code="401">When the Activité Enfant Incluse title is unauthorized</response>
        /// <response code="404">When the Activité Enfant Incluse title is not found</response>
        // GET: api/ActiviteEnfantIncluses/GetByTitle/Manege
        [HttpGet]
        [Route("[action]/{title}")]
        [ActionName("GetByTitle")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ActiviteEnfantIncluse>> GetActiviteByTitle(string title)
        {
            var activite = await dataRepository.GetByStringAsync(title);

            if (activite == null)
            {
                return NotFound();
            }

            return activite;
        }

        /// <summary>
        /// Modify a single Activité Enfant Incluse.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="204">When the Activité Enfant Incluse and id is valid</response>
        /// <response code="401">When the Activité Enfant Incluse and id is unauthorized</response>
        /// <response code="400">When the Activité Enfant Incluse id is not the same as id</response>
        /// <response code="404">When id is not valid</response>
        // PUT: api/ActiviteEnfantIncluses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutActiviteEnfantIncluse(int id, ActiviteEnfantIncluse activiteEnfantIncluse)
        {
            if (id != activiteEnfantIncluse.ActiviteEnfantIncluseId)
            {
                return BadRequest();
            }

            var activiteToUpdate = await dataRepository.GetByIdAsync(id);
            if (activiteToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(activiteToUpdate.Value, activiteEnfantIncluse);
                return NoContent();
            }
        }

        /// <summary>
        /// Add a single Activité Enfant Incluse.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="201">When the Activité Enfant Incluse is valid</response>
        /// <response code="401">When the Activité Enfant Incluse is unauthorized</response>
        /// <response code="400">When the Activité Enfant Incluse is not valid</response>
        // POST: api/ActiviteEnfantIncluses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ActiviteEnfantIncluse>> PostActiviteEnfantIncluse(ActiviteEnfantIncluse activiteEnfantIncluse)
        {
            await dataRepository.AddAsync(activiteEnfantIncluse);

            return CreatedAtAction("GetById", new { id = activiteEnfantIncluse.ActiviteEnfantIncluseId }, activiteEnfantIncluse);
        }

        /// <summary>
        /// Delete a single Activité Enfant Incluse.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">When the Activité Enfant Incluse id is valid</response>
        /// <response code="401">When the Activité Enfant Incluse id is unauthorized</response>
        /// <response code="404">When the Activité Enfant Incluse id is not valid</response>
        // DELETE: api/ActiviteEnfantIncluses/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteActiviteEnfantIncluse(int id)
        {
            var activite = await dataRepository.GetByIdAsync(id);
            if (activite == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(activite.Value);

            return NoContent();
        }

        //private bool ActiviteEnfantIncluseExists(int id)
        //{
        //    return (_context.ActiviteEnfantIncluses?.Any(e => e.ActiviteEnfantIncluseId == id)).GetValueOrDefault();
        //}
    }
}
