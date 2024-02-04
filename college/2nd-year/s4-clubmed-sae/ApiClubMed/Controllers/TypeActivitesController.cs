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
    [Route("api/typeactivite")]
    [ApiController]
    public class TypeActivitesController : ControllerBase
    {
        private readonly IDataRepository<TypeActivite> dataRepository;
        public TypeActivitesController(IDataRepository<TypeActivite> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Get all the Type Activites.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200"></response>
        /// <response code="401"></response>
        [HttpGet]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TypeActivite>>> GetTypeActivites()
        {
            return await dataRepository.GetAllAsync();
        }

        /// <summary>
        /// Get a single the Type Activite.
        /// </summary>
        /// <param name="id">The id of the Type Activite</param>
        /// <returns>Http response</returns>
        /// <response code="200">When the Type Activite id is found</response>
        /// <response code="401">When the Type Activite id is unauthorized</response>
        /// <response code="404">When the Type Activite id is not found</response>
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TypeActivite>> GetTypeActivite(int id)
        {
            var typeActivite = await dataRepository.GetByIdAsync(id);

            if (typeActivite == null)
            {
                return NotFound();
            }

            return typeActivite;
        }

        /// <summary>
        /// Get a single Type Activite by title.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="title">The name of the Type Activite</param>
        /// <response code="200">When the Type Activite title is found</response>
        /// <response code="401">When the Type Activite title is unauthorized</response>
        /// <response code="404">When the Type Activite title is not found</response>
        [HttpGet]
        [Route("[action]/{title}")]
        [ActionName("GetByTitle")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TypeActivite>> GetTypeActiviteByTitle(string title)
        {
            var typeActivite = await dataRepository.GetByStringAsync(title);

            if (typeActivite == null)
            {
                return NotFound();
            }

            return typeActivite;
        }

        /// <summary>
        /// Modify a single Type Activite.
        /// </summary>
        /// <param name="id">The id of the Type Activite</param>
        /// <returns>Http response</returns>
        /// <response code="204">When the Type Activite and id is valid</response>
        /// <response code="401">When the Type Activite and id is unauthorized</response>
        /// <response code="400">When the Type Activite id is not the same as id</response>
        /// <response code="404">When id is not valid</response>
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutTypeActivite(int id, TypeActivite typeActivite)
        {
            if (id != typeActivite.TypeActiviteId)
            {
                return BadRequest();
            }

            var typeActiviteToUpdate = await dataRepository.GetByIdAsync(id);
            if (typeActiviteToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(typeActiviteToUpdate.Value, typeActivite);
                return NoContent();
            }
        }

        /// <summary>
        /// Add a single Type Activite.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="201">When the Type Activite is valid</response>
        /// <response code="401">When the Type Activite is unauthorized</response>
        /// <response code="400">When the Type Activite is not valid</response>
        [HttpPost]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TypeActivite>> PostTypeActivite(TypeActivite typeActivite)
        {
            await dataRepository.AddAsync(typeActivite);

            return CreatedAtAction("GetById", new { id = typeActivite.TypeActiviteId }, typeActivite);
        }

        /// <summary>
        /// Delete a single Type Activite.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">When the Type Activite id is valid</response>
        /// <response code="401">When the Type Activite id is unauthorized</response>
        /// <response code="404">When the Type Activite id is not valid</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTypeActivite(int id)
        {
            var typeActivite = await dataRepository.GetByIdAsync(id);
            if (typeActivite == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(typeActivite.Value);

            return NoContent();
        }

        //private bool TypeActiviteExists(int id)
        //{
        //    return (_context.TypeActivites?.Any(e => e.TypeActiviteId == id)).GetValueOrDefault();
        //}
    }
}
