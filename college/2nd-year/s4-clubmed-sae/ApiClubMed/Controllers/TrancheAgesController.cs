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
    [Route("api/trancheage")]
    [ApiController]
    public class TrancheAgesController : ControllerBase
    {
        private readonly IDataRepository<TrancheAge> dataRepository;
        public TrancheAgesController(IDataRepository<TrancheAge> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Get all the Tranches Ages.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200"></response>
        /// <response code="401"></response>
        // GET: api/TrancheAges
        [HttpGet]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        public async Task<ActionResult<IEnumerable<TrancheAge>>> GetTrancheAges()
        {
            return await dataRepository.GetAllAsync();
        }

        /// <summary>
        /// Get a single Tranche Age.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the Tranche Age</param>
        /// <response code="200">When the Tranche Age id is found</response>
        /// <response code="401">When the Tranche Age id is unauthorized</response>
        /// <response code="404">When the Tranche Age id is not found</response>
        // GET: api/TrancheAges/GetById/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TrancheAge>> GetTrancheAge(int id)
        {
            var trancheAge = await dataRepository.GetByIdAsync(id);

            if (trancheAge == null)
            {
                return NotFound();
            }

            return trancheAge;
        }

        ///// <summary>
        ///// Get a single Tranche Age.
        ///// </summary>
        ///// <returns>Http response</returns>
        ///// <param name="agemin">The agemin of the Tranche Age</param>
        ///// <response code="200">When the Tranche Age agemin is found</response>
        ///// <response code="401">When the Tranche Age agemin is unauthorized</response>
        ///// <response code="404">When the Tranche Age agemin is not found</response>
        //// GET: api/TrancheAges/GetByAgeMin/15
        //[HttpGet]
        //[Route("[action]/{agemin}")]
        //[ActionName("GetByAgeMin")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<TrancheAge>> GetTrancheAgeByAgeMin(string agemin)
        //{
        //    var trancheAge = await dataRepository.GetByStringAsync(agemin);

        //    if (trancheAge == null)
        //    {
        //        return NotFound();
        //    }

        //    return trancheAge;
        //}

        /// <summary>
        /// Get a single Tranche Age.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="agemax">The agemax of the Tranche Age</param>
        /// <response code="200">When the Tranche Age agemax is found</response>
        /// <response code="401">When the Tranche Age agemax is unauthorized</response>
        /// <response code="404">When the Tranche Age agemax is not found</response>
        // GET: api/TrancheAges/GetByAgeMax/15
        [HttpGet]
        [Route("[action]/{agemax}")]
        [ActionName("GetByAgeMax")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TrancheAge>> GetTrancheAgeByAgeMax(string agemax)
        {
            var trancheAge = await dataRepository.GetByStringAsync(agemax);

            if (trancheAge == null)
            {
                return NotFound();
            }

            return trancheAge;
        }

        /// <summary>
        /// Modify a single Tranche Age.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="204">When the Tranche Age and id is valid</response>
        /// <response code="401">When the Tranche Age and id is unauthorized</response>
        /// <response code="400">When the Tranche Age id is not the same as id</response>
        /// <response code="404">When id is not valid</response>
        // PUT: api/TrancheAges/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutTrancheAge(int id, TrancheAge trancheAge)
        {
            if (id != trancheAge.TrancheAgeId)
            {
                return BadRequest();
            }

            var trancheAgeToUpdate = await dataRepository.GetByIdAsync(id);
            if (trancheAgeToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(trancheAgeToUpdate.Value, trancheAge);
                return NoContent();
            }
        }

        /// <summary>
        /// Add a single Tranche Age.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="201">When the Tranche Age is valid</response>
        /// <response code="401">When the Tranche Age is unauthorized</response>
        /// <response code="400">When the Tranche Age is not valid</response>
        // POST: api/TrancheAges
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TrancheAge>> PostTrancheAge(TrancheAge trancheAge)
        {
            await dataRepository.AddAsync(trancheAge);

            return CreatedAtAction("GetById", new { id = trancheAge.TrancheAgeId }, trancheAge);
        }

        /// <summary>
        /// Delete a single Tranche Age.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">When the Tranche Age id is valid</response>
        /// <response code="404">When the Tranche Age id is not valid</response>
        // DELETE: api/TrancheAges/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTrancheAge(int id)
        {
            var trancheAge = await dataRepository.GetByIdAsync(id);
            if (trancheAge == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(trancheAge.Value);

            return NoContent();
        }

        //private bool TrancheAgeExists(int id)
        //{
        //    return (_context.TrancheAges?.Any(e => e.TrancheAgeId == id)).GetValueOrDefault();
        //}
    }
}
