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
    public class BarsController : ControllerBase
    {
        private readonly IDataRepository<Bar> dataRepository;

        public BarsController(IDataRepository<Bar> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Get all the Bars.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200"></response>
        /// <response code="401"></response>
        // GET: api/Bars
        [HttpGet]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        public async Task<ActionResult<IEnumerable<Bar>>> GetBars()
        {
            return await dataRepository.GetAllAsync();
        }


        /// <summary>
        /// Get a single Bar.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the Bar</param>
        /// <response code="200">When the Bar id is found</response>
        /// <response code="401">When the Bar id is unauthorized</response>
        /// <response code="404">When the Bar id is not found</response>
        // GET: api/Bars/GetById/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [Authorize(Policy = Policies.Client)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Bar>> GetBar(int id)
        {
            var bar = await dataRepository.GetByIdAsync(id);

            if (bar == null)
            {
                return NotFound();
            }

            return bar;
        }


        //// GET: api/Bars/4
        //[HttpGet]
        //[Route("[action]/{libelle}")]
        //[ActionName("GetBarByResortId")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<CategorieTypeChambre>> GetBarByResortId(int id)
        //{
        //    var bar = await (Bar)dataRepository.GetBarByResortId;

        //    if (bar == null)
        //    {
        //        return NotFound();
        //    }

        //    return bar;
        //}



        /// <summary>
        /// Get a single bar.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="name">The name of the bar</param>
        /// <response code="200">When the bar name is found</response>
        /// <response code="401">When the bar name is unauthorized</response>
        /// <response code="404">When the bar name is not found</response>
        // GET: api/Resorts/GetByName/Unkai
        [HttpGet]
        [Route("[action]/{name}")]
        [ActionName("GetByName")]
        [Authorize(Policy = Policies.Client)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Bar>> GetBarByName(string name)
        {
            var bar = await dataRepository.GetByStringAsync(name);

            if (bar == null)
            {
                return NotFound();
            }

            return bar;
        }

        /// <summary>
        /// Modify a single bar.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="204">When the bar and id is valid</response>
        /// <response code="401">When the bar and id is unauthorized</response>
        /// <response code="400">When the bar id is not the same as id</response>
        /// <response code="404">When id is not valid</response>
        // PUT: api/Bars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.Client)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutBar(int id, Bar bar)
        {
            if (id != bar.BarId)
            {
                return BadRequest();
            }

            var barToUpdate = await dataRepository.GetByIdAsync(id);


            if (barToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(barToUpdate.Value, bar);
                return NoContent();
            }
        }

        /// <summary>
        /// Add a single bar.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="201">When the bar is valid</response>
        /// <response code="401">When the bar is unauthorized</response>
        /// <response code="400">When the bar is not valid</response>
        // POST: api/Bars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.Client)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Bar>> PostBar(Bar bar)
        {
            await dataRepository.AddAsync(bar);

            return CreatedAtAction("GetById", new { id = bar.BarId }, bar);
        }

        /// <summary>
        /// Delete a single bar.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">When the bar id is valid</response>
        /// <response code="401">When the bar id is unauthorized</response>
        /// <response code="404">When the bar id is not valid</response>
        // DELETE: api/Bars/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.Client)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBar(int id)
        {
            var bar = await dataRepository.GetByIdAsync(id);
            if (bar == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(bar.Value);

            return NoContent();
        }

        //private bool BarExists(int id)
        //{
        //    return (_context.Bars?.Any(e => e.BarId == id)).GetValueOrDefault();
        //}
    }
}
