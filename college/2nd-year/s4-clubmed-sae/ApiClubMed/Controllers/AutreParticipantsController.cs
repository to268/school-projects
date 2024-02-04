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
    [Route("api/[controller]")]
    [ApiController]
    public class AutreParticipantsController : ControllerBase
    {
        private readonly IDataRepository<AutreParticipant> dataRepository;

        public AutreParticipantsController(IDataRepository<AutreParticipant> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Get all the autreParticipant.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200"></response>
        /// <response code="401"></response>
        // GET: api/AutreParticipants
        [HttpGet]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        public async Task<ActionResult<IEnumerable<AutreParticipant>>> GetAutreParticipants()
        {
            return await dataRepository.GetAllAsync();
        }

        /// <summary>
        /// Get a single autreParticipant.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the autreParticipant</param>
        /// <response code="200">When the autreParticipant id is found</response>
        /// <response code="401">When the autreParticipant id is unauthorized</response>
        /// <response code="404">When the autreParticipant id is not found</response>
        // GET: api/AutreParticipants/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AutreParticipant>> GetAutreParticipant(int id)
        {
            var autreParticipant = await dataRepository.GetByIdAsync(id);

            if (autreParticipant == null)
            {
                return NotFound();
            }

            return autreParticipant;
        }

        /// <summary>
        /// Get a single autreParticipant.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="name">The name of the autreParticipant</param>
        /// <response code="200">When the autreParticipant name is found</response>
        /// <response code="401">When the autreParticipant name is unauthorized</response>
        /// <response code="404">When the autreParticipant name is not found</response>
        [HttpGet]
        [Route("[action]/{name}")]
        [ActionName("GetAutreParticipantByName")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AutreParticipant>> GetAutreParticipantByName(string name)
        {
            var autreParticipant = await dataRepository.GetByStringAsync(name);

            if (autreParticipant == null)
            {
                return NotFound();
            }

            return autreParticipant;
        }

        /// <summary>
        /// Modify a single autreParticipant.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="204">When the ResautreParticipantort and id is valid</response>
        /// <response code="401">When the ResautreParticipantort and id is unauthorized</response>
        /// <response code="400">When the autreParticipant id is not the same as id</response>
        /// <response code="404">When id is not valid</response>
        // PUT: api/AutreParticipants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutAutreParticipant(int id, AutreParticipant autreParticipant)
        {
            if (id != autreParticipant.AutreParticipantId)
            {
                return BadRequest();
            }

            var autreParticipantToUpdate = await dataRepository.GetByIdAsync(id);


            if (autreParticipantToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(autreParticipantToUpdate.Value, autreParticipant);
                return NoContent();
            }
        }

        /// <summary>
        /// Add a single autreParticipant.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="201">When the autreParticipant is valid</response>
        /// <response code="401">When the autreParticipant is unauthorized</response>
        /// <response code="400">When the autreParticipant is not valid</response>
        // POST: api/AutreParticipants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AutreParticipant>> PostAutreParticipant(AutreParticipant autreParticipant)
        {
            await dataRepository.AddAsync(autreParticipant);

            return CreatedAtAction("GetById", new { id = autreParticipant.AutreParticipantId }, autreParticipant);
        }

        /// <summary>
        /// Delete a single autreParticipant.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">When the autreParticipant id is valid</response>
        /// <response code="401">When the autreParticipant id is unauthorized</response>
        /// <response code="404">When the autreParticipant id is not valid</response>
        // DELETE: api/AutreParticipants/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAutreParticipant(int id)
        {
            var autreParticipant = await dataRepository.GetByIdAsync(id);
            if (autreParticipant == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(autreParticipant.Value);

            return NoContent();
        }

        //private bool AutreParticipantExists(int id)
        //{
        //    return (_context.AutreParticipants?.Any(e => e.AutreParticipantId == id)).GetValueOrDefault();
        //}
    }
}
