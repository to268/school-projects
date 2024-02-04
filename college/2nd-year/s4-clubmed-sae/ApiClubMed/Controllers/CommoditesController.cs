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
    public class CommoditesController : ControllerBase
    {
        private readonly IDataRepository<Commodite> dataRepository;

        public CommoditesController(IDataRepository<Commodite> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Get all the commodite.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200"></response>
        /// <response code="401"></response>
        // GET: api/Commodites
        [HttpGet]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        public async Task<ActionResult<IEnumerable<Commodite>>> GetCommodites()
        {
            return await dataRepository.GetAllAsync();
        }

        /// <summary>
        /// Get a single commodite.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the commodite</param>
        /// <response code="200">When the commodite id is found</response>
        /// <response code="401">When the commodite id is unauthorized</response>
        /// <response code="404">When the commodite id is not found</response>
        // GET: api/Commodites/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Commodite>> GetCommodite(int id)
        {
            var commodite = await dataRepository.GetByIdAsync(id);

            if (commodite == null)
            {
                return NotFound();
            }

            return commodite;
        }

        /// <summary>
        /// Get a single commodite.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="name">The name of the commodite</param>
        /// <response code="200">When the commodite name is found</response>
        /// <response code="401">When the commodite name is unauthorized</response>
        /// <response code="404">When the commodite name is not found</response>
        // GET: api/Commodites/TV
        [HttpGet]
        [Route("[action]/{name}")]
        [ActionName("GetByName")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Commodite>> GetCommoditeByName(string name)
        {
            var commodite = await dataRepository.GetByStringAsync(name);

            if (commodite == null)
            {
                return NotFound();
            }

            return commodite;
        }

        /// <summary>
        /// Modify a single commodite.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="204">When the commodite and id is valid</response>
        /// <response code="401">When the commodite and id is unauthorized</response>
        /// <response code="400">When the commodite id is not the same as id</response>
        /// <response code="404">When id is not valid</response>
        // PUT: api/Commodites/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutCommodite(int id, Commodite commodite)
        {
            if (id != commodite.CommoditeId)
            {
                return BadRequest();
            }

            var commoditeToUpdate = await dataRepository.GetByIdAsync(id);

            if (commoditeToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(commoditeToUpdate.Value, commodite);
                return NoContent();
            }
        }

        /// <summary>
        /// Add a single commodite.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="201">When the commodite is valid</response>
        /// <response code="401">When the commodite is unauthorized</response>
        /// <response code="400">When the commodite is not valid</response>
        // POST: api/Commodites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Commodite>> PostCommodite(Commodite commodite)
        {
            await dataRepository.AddAsync(commodite);

            return CreatedAtAction("GetById", new { id = commodite.CommoditeId }, commodite);
        }



        /// <summary>
        /// Delete a single commodite.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">When the commodite id is valid</response>
        /// <response code="401">When the commodite id is unauthorized</response>
        /// <response code="404">When the commodite id is not valid</response>
        // DELETE: api/Commodites/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCommodite(int id)
        {
            var commodite = await dataRepository.GetByIdAsync(id);
            if (commodite == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(commodite.Value);

            return NoContent();
        }

        //private bool CommoditeExists(int id)
        //{
        //    return (_context.Commodites?.Any(e => e.CommoditeId == id)).GetValueOrDefault();
        //}
    }
}
