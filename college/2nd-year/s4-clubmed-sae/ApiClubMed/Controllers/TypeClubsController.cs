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
    [Route("api/typeclub")]
    [ApiController]
    public class TypeClubsController : ControllerBase
    {
        private readonly IDataRepository<TypeClub> dataRepository;
        public TypeClubsController(IDataRepository<TypeClub> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Get all the Types Clubs.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200"></response>
        /// <response code="401"></response>
        // GET: api/TypeClubs
        [HttpGet]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        public async Task<ActionResult<IEnumerable<TypeClub>>> GetTypeClubs()
        {
            return await dataRepository.GetAllAsync();
        }

        /// <summary>
        /// Get a single Type Club.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the Type Club</param>
        /// <response code="200">When the Type Club id is found</response>
        /// <response code="401">When the Type Club id is unauthorized</response>
        /// <response code="404">When the Type Club id is not found</response>
        // GET: api/TypeClubs/GetById/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TypeClub>> GetTypeClub(int id)
        {
            var typeClub = await dataRepository.GetByIdAsync(id);

            if (typeClub == null)
            {
                return NotFound();
            }

            return typeClub;
        }


        /// <summary>
        /// Get a single Type Club.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="libelle">The libelle of the Type Club</param>
        /// <response code="200">When the Type Club libelle is found</response>
        /// <response code="401">When the Type Club libelle is unauthorized</response>
        /// <response code="404">When the Type Club libelle is not found</response>
        // GET: api/ActiviteEnfantIncluses/GetByName/Manege
        // GET: api/SousLocalisations/GetLibelle/Hiver
        [HttpGet]
        [Route("[action]/{libelle}")]
        [ActionName("GetLibelle")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TypeClub>> GetTypeClubByLibelle(string libelle)
        {
            var typeClub = await dataRepository.GetByStringAsync(libelle);

            if (typeClub == null)
            {
                return NotFound();
            }

            return typeClub;
        }


        /// <summary>
        /// Modify a single Type Club.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="204">When the Type Club and id is valid</response>
        /// <response code="401">When the Type Club and id is unauthorized</response>
        /// <response code="400">When the Type Club id is not the same as id</response>
        /// <response code="404">When id is not valid</response>
        // PUT: api/TypeClubs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutTypeClub(int id, TypeClub typeClub)
        {
            if (id != typeClub.TypeClubId)
            {
                return BadRequest();
            }

            var typeClubToUpdate = await dataRepository.GetByIdAsync(id);
            if (typeClubToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(typeClubToUpdate.Value, typeClub);
                return NoContent();
            }
        }


        /// <summary>
        /// Add a single Type Club.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="201">When the Type Club is valid</response>
        /// <response code="401">When the Type Club is unauthorized</response>
        /// <response code="400">When the Type Club is not valid</response>
        // POST: api/TypeClubs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TypeClub>> PostTypeClub(TypeClub typeClub)
        {
            await dataRepository.AddAsync(typeClub);

            return CreatedAtAction("GetById", new { id = typeClub.TypeClubId }, typeClub);
        }

        /// <summary>
        /// Delete a single Type Club.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">When the Type Club id is valid</response>
        /// <response code="401">When the Type Club id is unauthorized</response>
        /// <response code="404">When the Type Club id is not valid</response>
        // DELETE: api/TypeClubs/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTypeClub(int id)
        {
            var typeClub = await dataRepository.GetByIdAsync(id);
            if (typeClub == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(typeClub.Value);

            return NoContent();
        }

        //private bool TypeClubExists(int id)
        //{
        //    return (_context.TypeClubs?.Any(e => e.TypeClubId == id)).GetValueOrDefault();
        //}
    }
}
