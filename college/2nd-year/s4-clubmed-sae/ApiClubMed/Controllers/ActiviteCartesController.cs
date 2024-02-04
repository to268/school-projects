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
    [Route("api/activitecarte")]
    [ApiController]
    public class ActiviteCartesController : ControllerBase
    {
        private readonly IDataRepository<ActiviteCarte> dataRepository;
        public ActiviteCartesController(IDataRepository<ActiviteCarte> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Get all the Activite Cartes.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200"></response>
        /// <response code="401"></response>
        [HttpGet]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        public async Task<ActionResult<IEnumerable<ActiviteCarte>>> GetActiviteCarte()
        {
            return await dataRepository.GetAllAsync();
        }

        /// <summary>
        /// Get a single the Activite Carte.
        /// </summary>
        /// <param name="id">The id of the Activite Carte</param>
        /// <returns>Http response</returns>
        /// <response code="200">When the Activite Carte id is found</response>
        /// <response code="401">When the Activite Carte id is unauthorized</response>
        /// <response code="404">When the Activite Carte id is not found</response>
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ActiviteCarte>> GetActiviteCarte(int id)
        {
            var activiteCarte = await dataRepository.GetByIdAsync(id);

            if (activiteCarte == null)
            {
                return NotFound();
            }

            return activiteCarte;
        }

        /// <summary>
        /// Get a single Activite Carte by title.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="title">The title of the Activite Carte</param>
        /// <response code="200">When the Activite Carte title is found</response>
        /// <response code="404">When the Activite Carte title is unauthorized</response>
        /// <response code="404">When the Activite Carte title is not found</response>
        [HttpGet]
        [Route("[action]/{title}")]
        [ActionName("GetByTitle")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ActiviteCarte>> GetActiviteCarteByTitle(string title)
        {
            var activiteCarte = await dataRepository.GetByStringAsync(title);

            if (activiteCarte == null)
            {
                return NotFound();
            }

            return activiteCarte;
        }

        /// <summary>
        /// Modify a single Activite Carte.
        /// </summary>
        /// <param name="id">The id of the Activite Carte</param>
        /// <returns>Http response</returns>
        /// <response code="204">When the Activite Carte and id is valid</response>
        /// <response code="401">When the Activite Carte and id is unauthorized</response>
        /// <response code="400">When the Activite Carte id is not the same as id</response>
        /// <response code="404">When id is not valid</response>
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutActiviteCarte(int id, ActiviteCarte activiteCarte)
        {
            if (id != activiteCarte.ActiviteCarteId)
            {
                return BadRequest();
            }

            var activiteCarteToUpdate = await dataRepository.GetByIdAsync(id);
            if (activiteCarteToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(activiteCarteToUpdate.Value, activiteCarte);
                return NoContent();
            }
        }

        /// <summary>
        /// Add a single Activite Carte.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="201">When the Activite Carte is valid</response>
        /// <response code="401">When the Activite Carte is unauthorized</response>
        /// <response code="400">When the Activite Carte is not valid</response>
        [HttpPost]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ActiviteCarte>> PostActiviteCarte(ActiviteCarte activiteCarte)
        {
            await dataRepository.AddAsync(activiteCarte);

            return CreatedAtAction("GetById", new { id = activiteCarte.ActiviteCarteId }, activiteCarte);
        }

        /// <summary>
        /// Delete a single Activite Carte.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">When the Activite Carte id is valid</response>
        /// <response code="401">When the Activite Carte id is unauthorized</response>
        /// <response code="404">When the Activite Carte id is not valid</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteActiviteCarte(int id)
        {
            var activiteCarte = await dataRepository.GetByIdAsync(id);
            if (activiteCarte == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(activiteCarte.Value);

            return NoContent();
        }

        //private bool ActiviteCarteExists(int id)
        //{
        //    return (_context.ActiviteCarte?.Any(e => e.ActiviteCarteId == id)).GetValueOrDefault();
        //}
    }
}
