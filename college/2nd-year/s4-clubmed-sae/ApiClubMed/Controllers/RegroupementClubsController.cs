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
    [Route("api/regroupementclub")]
    [ApiController]
    public class RegroupementClubsController : ControllerBase
    {
        private readonly IDataRepository<RegroupementClub> dataRepository;
        public RegroupementClubsController(IDataRepository<RegroupementClub> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Get all the Regroupements Clubs.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200"></response>
        /// <response code="401"></response>
        // GET: api/RegroupementClubs
        [HttpGet]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        public async Task<ActionResult<IEnumerable<RegroupementClub>>> GetRegroupementClubs()
        {
            return await dataRepository.GetAllAsync();
        }

        /// <summary>
        /// Get a single Regroupement Club.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the Regroupement Club</param>
        /// <response code="200">When the Regroupement Club id is found</response>
        /// <response code="401">When the Regroupement Club id is unauthorized</response>
        /// <response code="404">When the Regroupement Club id is not found</response>
        // GET: api/RegroupementClubs/GetById/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RegroupementClub>> GetRegroupementClub(int id)
        {
            var regroupementClub = await dataRepository.GetByIdAsync(id);

            if (regroupementClub == null)
            {
                return NotFound();
            }

            return regroupementClub;
        }

        /// <summary>
        /// Get a single Regroupement Club.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="libelle">The libelle of the Regroupement Club</param>
        /// <response code="200">When the Regroupement Club libelle is found</response>
        /// <response code="401">When the Regroupement Club libelle is unauthorized</response>
        /// <response code="404">When the Regroupement Club libelle is not found</response>
        // GET: api/RegroupementClubs/GetByLibelle/Exterieur
        [HttpGet]
        [Route("[action]/{libelle}")]
        [ActionName("GetByLibelle")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RegroupementClub>> GetRegroupementClubByLibelle(string libelle)
        {
            var regroupementClub = await dataRepository.GetByStringAsync(libelle);

            if (regroupementClub == null)
            {
                return NotFound();
            }

            return regroupementClub;
        }


        /// <summary>
        /// Modify a single Regroupement Club.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="204">When the Regroupement Club and id is valid</response>
        /// <response code="401">When the Regroupement Club and id is unauthorized</response>
        /// <response code="400">When the Regroupement Club id is not the same as id</response>
        /// <response code="404">When id is not valid</response>
        // PUT: api/RegroupementClubs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutRegroupementClub(int id, RegroupementClub regroupementClub)
        {
            if (id != regroupementClub.RegroupementClubId)
            {
                return BadRequest();
            }

            var regroupementClubToUpdate = await dataRepository.GetByIdAsync(id);
            if (regroupementClubToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(regroupementClubToUpdate.Value, regroupementClub);
                return NoContent();
            }
        }


        /// <summary>
        /// Add a single Regroupement Club.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="201">When the Regroupement Club is valid</response>
        /// <response code="401">When the Regroupement Club is unauthorized</response>
        /// <response code="400">When the Regroupement Club is not valid</response>
        // POST: api/RegroupementClubs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RegroupementClub>> PostRegroupementClub(RegroupementClub regroupementClub)
        {
            await dataRepository.AddAsync(regroupementClub);

            return CreatedAtAction("GetById", new { id = regroupementClub.RegroupementClubId }, regroupementClub);
        }


        /// <summary>
        /// Delete a single Regroupement Club.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">When the Regroupement Club id is valid</response>
        /// <response code="401">When the Regroupement Club id is unauthorized</response>
        /// <response code="404">When the Regroupement Club id is not valid</response>
        // DELETE: api/RegroupementClubs/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRegroupementClub(int id)
        {
            var regroupentClub = await dataRepository.GetByIdAsync(id);
            if (regroupentClub == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(regroupentClub.Value);

            return NoContent();
        }

        //private bool RegroupementClubExists(int id)
        //{
        //    return (_context.RegroupementClubs?.Any(e => e.RegroupementClubId == id)).GetValueOrDefault();
        //}
    }
}
