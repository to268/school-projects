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
    [Route("api/pays")]
    [ApiController]
    public class PaysController : ControllerBase
    {
        private readonly IDataRepository<Pays> dataRepository;
        public PaysController(IDataRepository<Pays> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Get all the Pays.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200"></response>
        /// <response code="401"></response>
        [HttpGet]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        public async Task<ActionResult<IEnumerable<Pays>>> GetPays()
        {
            return await dataRepository.GetAllAsync();
        }

        /// <summary>
        /// Get a single the Pays.
        /// </summary>
        /// <param name="id">The id of the Pays</param>
        /// <returns>Http response</returns>
        /// <response code="200">When the Pays id is found</response>
        /// <response code="401">When the Pays id is unauthorized</response>
        /// <response code="404">When the Pays id is not found</response>
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pays>> GetPays(int id)
        {
            var pays = await dataRepository.GetByIdAsync(id);

            if (pays == null)
            {
                return NotFound();
            }

            return pays;
        }

        /// <summary>
        /// Get a single Pays by title.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="title">The name of the Pays</param>
        /// <response code="200">When the Pays title is found</response>
        /// <response code="401">When the Pays title is unauthorized</response>
        /// <response code="404">When the Pays title is not found</response>
        [HttpGet]
        [Route("[action]/{title}")]
        [ActionName("GetByTitle")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pays>> GeyPaysByNom(string title)
        {
            var pays = await dataRepository.GetByStringAsync(title);

            if (pays == null)
            {
                return NotFound();
            }

            return pays;
        }

        /// <summary>
        /// Modify a single Pays.
        /// </summary>
        /// <param name="id">The id of the Pays</param>
        /// <returns>Http response</returns>
        /// <response code="204">When the Pays and id is valid</response>
        /// <response code="401">When the Pays and id is unauthorized</response>
        /// <response code="400">When the Pays id is not the same as id</response>
        /// <response code="404">When id is not valid</response>
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutPays(int id, Pays pays)
        {
            if (id != pays.PaysId)
            {
                return BadRequest();
            }

            var paysToUpdate = await dataRepository.GetByIdAsync(id);
            if (paysToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(paysToUpdate.Value, pays);
                return NoContent();
            }
        }

        /// <summary>
        /// Add a single Pays.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="201">When the Pays is valid</response>
        /// <response code="401">When the Pays is unauthorized</response>
        /// <response code="400">When the Pays is not valid</response>
        [HttpPost]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pays>> PostPays(Pays pays)
        {
            await dataRepository.AddAsync(pays);

            return CreatedAtAction("GetById", new { id = pays.PaysId }, pays);
        }

        /// <summary>
        /// Delete a single Pays.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">When the Pays id is valid</response>
        /// <response code="401">When the Pays id is unauthorized</response>
        /// <response code="404">When the Pays id is not valid</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePays(int id)
        {
            var pays = await dataRepository.GetByIdAsync(id);
            if (pays == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(pays.Value);

            return NoContent();
        }

        //private bool PaysExists(int id)
        //{
        //    return (_context.Pays?.Any(e => e.PaysId == id)).GetValueOrDefault();
        //}
    }
}
