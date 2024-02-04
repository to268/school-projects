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
    [Route("api/activiteenfantcarte")]
    [ApiController]
    public class ActiviteEnfantCartesController : ControllerBase
    {
        private readonly IDataRepository<ActiviteEnfantCarte> dataRepository;
        public ActiviteEnfantCartesController(IDataRepository<ActiviteEnfantCarte> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Get all the Activités Enfants Cartes.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200"></response>
        /// <response code="401"></response>
        // GET: api/ActiviteEnfantCartes
        [HttpGet]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        public async Task<ActionResult<IEnumerable<ActiviteEnfantCarte>>> GetActiviteEnfantCartes()
        {
            return await dataRepository.GetAllAsync();
        }

        /// <summary>
        /// Get a single Activité Enfant Carte.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the Activité Enfant Carte</param>
        /// <response code="200">When the Activité Enfant Carte id is found</response>
        /// <response code="401">When the Activité Enfant Carte id is unauthorized</response>
        /// <response code="404">When the Activité Enfant Carte id is not found</response>
        // GET: api/ActiviteEnfantCartes/GetById/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ActiviteEnfantCarte>> GetActiviteEnfantCarte(int id)
        {
            var activite = await dataRepository.GetByIdAsync(id);

            if (activite == null)
            {
                return NotFound();
            }

            return activite;
        }

        /// <summary>
        /// Get a single Activité Enfant Carte.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="title">The title of the Activité Enfant Carte</param>
        /// <response code="200">When the Activité Enfant Carte title is found</response>
        /// <response code="401">When the Activité Enfant Carte title is unauthorized</response>
        /// <response code="404">When the Activité Enfant Carte title is not found</response>
        // GET: api/ActiviteEnfantCartes/GetByTitle/Manege
        [HttpGet]
        [Route("[action]/{title}")]
        [ActionName("GetByTitle")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ActiviteEnfantCarte>> GetActiviteByTitle(string title)
        {
            var activite = await dataRepository.GetByStringAsync(title);

            if (activite == null)
            {
                return NotFound();
            }

            return activite;
        }

        /// <summary>
        /// Modify a single Activité Enfant Carte.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="204">When the Activité Enfant Carte and id is valid</response>
        /// <response code="401">When the Activité Enfant Carte and id is unauthorized</response>
        /// <response code="400">When the Activité Enfant Carte id is not the same as id</response>
        /// <response code="404">When id is not valid</response>
        // PUT: api/ActiviteEnfantCartes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutActiviteEnfantCarte(int id, ActiviteEnfantCarte activiteEnfantCarte)
        {
            if (id != activiteEnfantCarte.ActiviteEnfantCarteId)
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
                await dataRepository.UpdateAsync(activiteToUpdate.Value, activiteEnfantCarte);
                return NoContent();
            }
        }

        /// <summary>
        /// Add a single Activité Enfant Carte.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="201">When the Activité Enfant Carte is valid</response>
        /// <response code="401">When the Activité Enfant Carte is unauthorized</response>
        /// <response code="400">When the Activité Enfant Carte is not valid</response>
        // POST: api/ActiviteEnfantCartes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ActiviteEnfantCarte>> PostActiviteEnfantCarte(ActiviteEnfantCarte activiteEnfantCarte)
        {
            await dataRepository.AddAsync(activiteEnfantCarte);

            return CreatedAtAction("GetById", new { id = activiteEnfantCarte.ActiviteEnfantCarteId }, activiteEnfantCarte);
        }

        /// <summary>
        /// Delete a single Activité Enfant Carte.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">When the Activité Enfant Carte id is valid</response>
        /// <response code="401">When the Activité Enfant Carte id is unauthorized</response>
        /// <response code="404">When the Activité Enfant Carte id is not valid</response>
        // DELETE: api/ActiviteEnfantCartes/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteActiviteEnfantCarte(int id)
        {
            var activite = await dataRepository.GetByIdAsync(id);
            if (activite == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(activite.Value);

            return NoContent();
        }

        //private bool ActiviteEnfantCarteExists(int id)
        //{
        //    return (_context.ActiviteEnfantCartes?.Any(e => e.ActiviteEnfantCarteId == id)).GetValueOrDefault();
        //}
    }
}
