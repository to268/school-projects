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
    public class PointFortsController : ControllerBase
    {
        private readonly IDataRepository<PointFort> dataRepository;

        public PointFortsController(IDataRepository<PointFort> dataRepo)
        {
            dataRepository = dataRepo;
        }


        /// <summary>
        /// Get all the pointFort.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200"></response>
        /// <response code="401"></response>
        // GET: api/PointForts
        [HttpGet]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        public async Task<ActionResult<IEnumerable<PointFort>>> GetPointForts()
        {
            return await dataRepository.GetAllAsync();
        }


        /// <summary>
        /// Get a single pointFort.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the pointFort</param>
        /// <response code="200">When the pointFort id is found</response>
        /// <response code="401">When the pointFort id is unauthorized</response>
        /// <response code="404">When the RepointFortsort id is not found</response>
        // GET: api/PointForts/GetById/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PointFort>> GetPointFort(int id)
        {
            var pointFort = await dataRepository.GetByIdAsync(id);

            if (pointFort == null)
            {
                return NotFound();
            }

            return pointFort;
        }

        /// <summary>
        /// Get a single pointFort.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="name">The libelle of the pointFort</param>
        /// <response code="200">When the pointFort libelle is found</response>
        /// <response code="401">When the pointFort libelle is unauthorized</response>
        /// <response code="404">When the pointFort libelle is not found</response>
        // GET: api/PointForts/Chambre
        [HttpGet]
        [Route("[action]/{libelle}")]
        [ActionName("GetByLibelle")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PointFort>> GetPointFortByLibelle(string libelle)
        {
            var pointFort = await dataRepository.GetByStringAsync(libelle);

            if (pointFort == null)
            {
                return NotFound();
            }

            return pointFort;
        }

        /// <summary>
        /// Modify a single pointFort.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="204">When the pointFort and id is valid</response>
        /// <response code="401">When the pointFort and id is unauthorized</response>
        /// <response code="400">When the pointFort id is not the same as id</response>
        /// <response code="404">When id is not valid</response>
        // PUT: api/PointForts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutPointFort(int id, PointFort pointFort)
        {
            if (id != pointFort.PointFortId)
            {
                return BadRequest();
            }

            var pointFortToUpdate = await dataRepository.GetByIdAsync(id);


            if (pointFortToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(pointFortToUpdate.Value, pointFort);
                return NoContent();
            }
        }


        /// <summary>
        /// Add a single pointFort.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="201">When the pointFort is valid</response>
        /// <response code="401">When the pointFort is unauthorized</response>
        /// <response code="400">When the pointFort is not valid</response>
        // POST: api/PointForts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PointFort>> PostPointFort(PointFort pointFort)
        {
            await dataRepository.AddAsync(pointFort);

            return CreatedAtAction("GetById", new { id = pointFort.PointFortId }, pointFort);
        }


        /// <summary>
        /// Delete a single pointFort.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">When the pointFort id is valid</response>
        /// <response code="401">When the pointFort id is unauthorized</response>
        /// <response code="404">When the pointFort id is not valid</response>
        // DELETE: api/PointForts/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePointFort(int id)
        {
            var pointFort = await dataRepository.GetByIdAsync(id);
            if (pointFort == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(pointFort.Value);

            return NoContent();
        }

        //private bool PointFortExists(int id)
        //{
        //    return (_context.PointForts?.Any(e => e.PointFortId == id)).GetValueOrDefault();
        //}
    }
}
