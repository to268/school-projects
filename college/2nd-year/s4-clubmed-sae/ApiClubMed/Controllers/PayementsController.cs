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
    [Route("api/payement")]
    [ApiController]
    public class PayementsController : ControllerBase
    {
        private readonly IDataRepository<Payement> dataRepository;
        public PayementsController(IDataRepository<Payement> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Get all the Payements.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200"></response>
        /// <response code="401unauthorizedresponse>
        /// <response code="401"></response>
        [HttpGet]
        [Authorize(Policy = Policies.Admin)]
        public async Task<ActionResult<IEnumerable<Payement>>> GetPayements()
        {
            return await dataRepository.GetAllAsync();
        }

        /// <summary>
        /// Get a single the Payement.
        /// </summary>
        /// <param name="id">The id of the Payement</param>
        /// <returns>Http response</returns>
        /// <response code="200">When the Payement id is found</response>
        /// <response code="401">When the Payement id is unauthorized</response>
        /// <response code="404">When the Payement id is not found</response>
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Payement>> GetPayement(int id)
        {
            var payement = await dataRepository.GetByIdAsync(id);

            if (payement == null)
            {
                return NotFound();
            }

            return payement;
        }

        /// <summary>
        /// Get a single Activite Carte by agemin.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">When the Activite Carte agemin is found</response>
        /// <response code="401">When the Activite Carte agemin is unauthorized</response>
        /// <response code="404">When the Activite Carte agemin is not found</response>
        //[HttpGet("GetPayementByReservationId/{id}")]
        //public async Task<ActionResult<Payement>> GetPayementByReservationId(int id)
        //{
        //    var payement = await dataRepository.GetByReservationIdAsync(id);

        //    if (payement == null)
        //    {
        //        return NotFound();
        //    }

        //    return payement;
        //}

        /// <summary>
        /// Modify a single Payement.
        /// </summary>
        /// <param name="id">The id of the Payement</param>
        /// <returns>Http response</returns>
        /// <response code="204">When the Payement and id is valid</response>
        /// <response code="400">When the Payement id is not the same as id</response>
        /// <response code="404">When id is not valid</response>
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutPayement(int id, Payement payement)
        {
            if (id != payement.PayementId)
            {
                return BadRequest();
            }

            var payementToUpdate = await dataRepository.GetByIdAsync(id);
            if (payementToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(payementToUpdate.Value, payement);
                return NoContent();
            }
        }

        /// <summary>
        /// Add a single Payement.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="201">When the Payement is valid</response>
        /// <response code="401">When the Payement is unauthorized</response>
        /// <response code="400">When the Payement is not valid</response>
        [HttpPost]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Payement>> PostPayement(Payement payement)
        {
            await dataRepository.AddAsync(payement);

            return CreatedAtAction("GetById", new { id = payement.PayementId }, payement);
        }

        /// <summary>
        /// Delete a single Payement.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">When the Payement id is valid</response>
        /// <response code="401">When the Payement id is unauthorized</response>
        /// <response code="404">When the Payement id is not valid</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePayement(int id)
        {
            var payement = await dataRepository.GetByIdAsync(id);
            if (payement == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(payement.Value);

            return NoContent();
        }

        //private bool PayementExists(int id)
        //{
        //    return (_context.Payements?.Any(e => e.PayementId == id)).GetValueOrDefault();
        //}
    }
}
