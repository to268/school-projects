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
    [Route("api/reservation")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IDataRepository<Reservation> dataRepository;
        public ReservationsController(IDataRepository<Reservation> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Get all the Reservations.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200"></response>
        /// <response code="401"></response>
        [HttpGet]
        [Authorize(Policy = Policies.Admin)]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            return await dataRepository.GetAllAsync();
        }

        /// <summary>
        /// Get a single the Reservation.
        /// </summary>
        /// <param name="id">The id of the Reservation</param>
        /// <returns>Http response</returns>
        /// <response code="200">When the Reservation id is found</response>
        /// <response code="401">When the Reservation id is unauthorized</response>
        /// <response code="404">When the Reservation id is not found</response>
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            var activite = await dataRepository.GetByIdAsync(id);

            if (activite == null)
            {
                return NotFound();
            }

            return activite;
        }

        /// <summary>
        /// Get a single Reservation by Resort id.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The name of the Reservation</param>
        /// <response code="200">When the Reservation Resort id is found</response>
        /// <response code="401">When the Reservation Resort id is unauthorized</response>
        /// <response code="404">When the Reservation Resort id is not found</response>
        //[HttpGet("GetReservationByResortId/{id}")]
        //public async Task<ActionResult<Reservation>> GetReservationByResortId(int id)
        //{
        //    var reservation = await dataRepository.GetByResortIdAsync(id);

        //    if (reservation == null)
        //    {
        //        return NotFound();
        //    }

        //    return reservation;
        //}

        /// <summary>
        /// Get a single Reservation by Client id.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The name of the Reservation</param>
        /// <response code="200">When the Reservation Client id is found</response>
        /// <response code="404">When the Reservation Client id is not found</response>
        //[HttpGet("GetReservationByClientId/{id}")]
        //public async Task<ActionResult<Reservation>> GetReservationByClientId(int id)
        //{
        //    var reservation = await dataRepository.GetByClientIdAsync(id);

        //    if (reservation == null)
        //    {
        //        return NotFound();
        //    }

        //    return reservation;
        //}

        /// <summary>
        /// Modify a single Reservation.
        /// </summary>
        /// <param name="id">The id of the Reservation</param>
        /// <returns>Http response</returns>
        /// <response code="204">When the Reservation and id is valid</response>
        /// <response code="400">When the Reservation id is not the same as id</response>
        /// <response code="404">When id is not valid</response>
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutReservation(int id, Reservation reservation)
        {
            if (id != reservation.ReservationId)
            {
                return BadRequest();
            }

            var reservationToUpdate = await dataRepository.GetByIdAsync(id);
            if (reservationToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(reservationToUpdate.Value, reservation);
                return NoContent();
            }
        }

        /// <summary>
        /// Add a single Reservation.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="201">When the Reservation is valid</response>
        /// <response code="401">When the Reservation is unauthorized</response>
        /// <response code="400">When the Reservation is not valid</response>
        [HttpPost]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            await dataRepository.AddAsync(reservation);

            return CreatedAtAction("GetById", new { id = reservation.ReservationId }, reservation);
        }

        /// <summary>
        /// Delete a single Reservation.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">When the Reservation id is valid</response>
        /// <response code="401">When the Reservation id is unauthorized</response>
        /// <response code="404">When the Reservation id is not valid</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await dataRepository.GetByIdAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(reservation.Value);

            return NoContent();
        }

        //private bool ReservationExists(int id)
        //{
        //    return (_context.Reservations?.Any(e => e.ReservationId == id)).GetValueOrDefault();
        //}
    }
}
