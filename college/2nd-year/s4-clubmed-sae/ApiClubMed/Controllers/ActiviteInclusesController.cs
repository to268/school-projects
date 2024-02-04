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
    [Route("api/activiteincluse")]
    [ApiController]
    public class ActiviteInclusesController : ControllerBase
    {
        private readonly IDataRepository<ActiviteIncluse> dataRepository;
        public ActiviteInclusesController(IDataRepository<ActiviteIncluse> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Get all the Activite Incluses.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200"></response>
        /// <response code="401"></response>
        [HttpGet]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        public async Task<ActionResult<IEnumerable<ActiviteIncluse>>> GetActiviteIncluses()
        {
            return await dataRepository.GetAllAsync();
        }

        /// <summary>
        /// Get a single the Activite Incluse.
        /// </summary>
        /// <param name="id">The id of the Activite Incluse</param>
        /// <returns>Http response</returns>
        /// <response code="200">When the Activite Incluse id is found</response>
        /// <response code="401">When the Activite Incluse id is unauthorized</response>
        /// <response code="404">When the Activite Incluse id is not found</response>
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ActiviteIncluse>> GetActiviteIncluse(int id)
        {
            var activiteIncluse = await dataRepository.GetByIdAsync(id);

            if (activiteIncluse == null)
            {
                return NotFound();
            }

            return activiteIncluse;
        }

        /// <summary>
        /// Get a single Activite Incluse by title.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="title">The title of the Activite Incluse</param>
        /// <response code="200">When the Activite Incluse title is found</response>
        /// <response code="401">When the Activite Incluse title is unauthorized</response>
        /// <response code="404">When the Activite Incluse title is not found</response>
        [HttpGet]
        [Route("[action]/{title}")]
        [ActionName("GetByTitle")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ActiviteIncluse>> GetActiviteIncluseByTitle(string title)
        {
            var activiteIncluse = await dataRepository.GetByStringAsync(title);

            if (activiteIncluse == null)
            {
                return NotFound();
            }

            return activiteIncluse;
        }

        /// <summary>
        /// Modify a single Activite Incluse.
        /// </summary>
        /// <param name="id">The id of the Activite Incluse</param>
        /// <returns>Http response</returns>
        /// <response code="204">When the Activite Incluse and id is valid</response>
        /// <response code="401">When the Activite Incluse and id is unauthorized</response>
        /// <response code="400">When the Activite Incluse id is not the same as id</response>
        /// <response code="404">When id is not valid</response>
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutActiviteIncluse(int id, ActiviteIncluse activiteIncluse)
        {
            if (id != activiteIncluse.ActiviteIncluseId)
            {
                return BadRequest();
            }

            var activiteIncluseToUpdate = await dataRepository.GetByIdAsync(id);
            if (activiteIncluseToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(activiteIncluseToUpdate.Value, activiteIncluse);
                return NoContent();
            }
        }

        /// <summary>
        /// Add a single Activite Incluse.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="201">When the Activite Incluse is valid</response>
        /// <response code="401">When the Activite Incluse is unauthorized</response>
        /// <response code="400">When the Activite Incluse is not valid</response>
        [HttpPost]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ActiviteIncluse>> PostActiviteIncluse(ActiviteIncluse activiteIncluse)
        {
            await dataRepository.AddAsync(activiteIncluse);

            return CreatedAtAction("GetById", new { id = activiteIncluse.ActiviteIncluseId }, activiteIncluse);
        }

        /// <summary>
        /// Delete a single Activite Incluse.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">When the Activite Incluse id is valid</response>
        /// <response code="401">When the Activite Incluse id is unauthorized</response>
        /// <response code="404">When the Activite Incluse id is not valid</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteActiviteIncluse(int id)
        {
            var activiteIncluse = await dataRepository.GetByIdAsync(id);
            if (activiteIncluse == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(activiteIncluse.Value);

            return NoContent();
        }

        //private bool ActiviteIncluseExists(int id)
        //{
        //    return (_context.ActiviteIncluses?.Any(e => e.ActiviteIncluseId == id)).GetValueOrDefault();
        //}
    }
}
