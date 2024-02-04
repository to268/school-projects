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
    [Route("api/transport")]
    [ApiController]
    public class TransportsController : ControllerBase
    {
        private readonly IDataRepository<Transport> dataRepository;
        public TransportsController(IDataRepository<Transport> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Get all the Transports.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200"></response>
        /// <response code="401"></response>
        /// <response code="401unauthorizedresponse>
        [HttpGet]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        public async Task<ActionResult<IEnumerable<Transport>>> GetTransports()
        {
            return await dataRepository.GetAllAsync();
        }

        /// <summary>
        /// Get a single the Transport.
        /// </summary>
        /// <param name="id">The id of the Transport</param>
        /// <returns>Http response</returns>
        /// <response code="200">When the Transport id is found</response>
        /// <response code="401">When the Transport id is unauthorized</response>
        /// <response code="404">When the Transport id is not found</response>
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Transport>> GetTransport(int id)
        {
            var activite = await dataRepository.GetByIdAsync(id);

            if (activite == null)
            {
                return NotFound();
            }

            return activite;
        }

        /// <summary>
        /// Get a single Transport by libelle.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="libelle">The name of the Transport</param>
        /// <response code="200">When the Transport libelle is found</response>
        /// <response code="401">When the Transport libelle is unauthorized</response>
        /// <response code="404">When the Transport libelle is not found</response>
        [HttpGet]
        [Route("[action]/{libelle}")]
        [ActionName("GetByLibelle")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Transport>> GetTransportByLibelle(string libelle)
        {
            var transport = await dataRepository.GetByStringAsync(libelle);

            if (transport == null)
            {
                return NotFound();
            }

            return transport;
        }

        /// <summary>
        /// Modify a single Transport.
        /// </summary>
        /// <param name="id">The id of the Transport</param>
        /// <returns>Http response</returns>
        /// <response code="204">When the Transport and id is valid</response>
        /// <response code="401">When the Transport and id is unauthorized</response>
        /// <response code="400">When the Transport id is not the same as id</response>
        /// <response code="404">When id is not valid</response>
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutTransport(int id, Transport transport)
        {
            if (id != transport.TransportId)
            {
                return BadRequest();
            }

            var transportToUpdate = await dataRepository.GetByIdAsync(id);
            if (transportToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(transportToUpdate.Value, transport);
                return NoContent();
            }
        }

        /// <summary>
        /// Add a single Transport.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="201">When the Transport is valid</response>
        /// <response code="401">When the Transport is unauthorized</response>
        /// <response code="400">When the Transport is not valid</response>
        [HttpPost]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Transport>> PostTransport(Transport transport)
        {
            await dataRepository.AddAsync(transport);

            return CreatedAtAction("GetById", new { id = transport.TransportId }, transport);
        }

        /// <summary>
        /// Delete a single Transport.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">When the Transport id is valid</response>
        /// <response code="401">When the Transport id is unauthorized</response>
        /// <response code="404">When the Transport id is not valid</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTransport(int id)
        {
            var transport = await dataRepository.GetByIdAsync(id);
            if (transport == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(transport.Value);

            return NoContent();
        }

        //private bool TransportExists(int id)
        //{
        //    return (_context.Transports?.Any(e => e.TransportId == id)).GetValueOrDefault();
        //}
    }
}
