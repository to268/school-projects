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
    public class AvisController : ControllerBase
    {
        private readonly IDataRepository<Avis> dataRepository;

        public AvisController(IDataRepository<Avis> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Get all the avis.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200"></response>
        /// <response code="401"></response>
        // GET: api/Avis
        [HttpGet]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        public async Task<ActionResult<IEnumerable<Avis>>> GetAvis()
        {
            return await dataRepository.GetAllAsync();
        }


        /// <summary>
        /// Get a single avis.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the avis</param>
        /// <response code="200">When the avis id is found</response>
        /// <response code="401">When the avis id is unauthorized</response>
        /// <response code="404">When the avis id is not found</response>
        // GET: api/Avis/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Avis>> GetAvis(int id)
        {
            var avis = await dataRepository.GetByIdAsync(id);

            if (avis == null)
            {
                return NotFound();
            }

            return avis;
        }

        //// GET: api/GetAvisByResortId/deluxe
        //[HttpGet]
        //[Route("[action]/{libelle}")]
        //[ActionName("GetAvisByResortId")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<avis>> GetAvisByResortId(string libelle)
        //{
        //    var avis = await dataRepository.GetAvisByResortId(libelle);

        //    if (avis == null)
        //    {
        //        return NotFound();
        //    }

        //    return avis;
        //}

        // GET: api/GetAvisByClientId/deluxe
        //[HttpGet]
        //[Route("[action]/{libelle}")]
        //[ActionName("GetAvisByClientId")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<avis>> GetAvisByClientId(string libelle)
        //{
        //    var avis = await dataRepository.GetAvisByClientId(libelle);

        //    if (avis == null)
        //    {
        //        return NotFound();
        //    }

        //    return avis;
        //}

        // GET: api/GetAvisByNote/deluxe
        //[HttpGet]
        //[Route("[action]/{libelle}")]
        //[ActionName("GetAvisByNote")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<avis>> GetAvisByNote(string libelle)
        //{
        //    var avis = await dataRepository.GetAvisByNote(libelle);

        //    if (avis == null)
        //    {
        //        return NotFound();
        //    }

        //    return avis;
        //}

        ///// <summary>
        ///// Get a single avis.
        ///// </summary>
        ///// <returns>Http response</returns>
        ///// <param name="name">The name of the avis</param>
        ///// <response code="200">When the avis name is found</response>
        ///// <response code="401">When the avis name is unauthorized</response>
        ///// <response code="404">When the avis name is not found</response>
        //// GET: api/GetAvisByLibelle/notimplement
        //[HttpGet]
        //[Route("[action]/{libelle}")]
        //[ActionName("GetAvisByLibelle")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<Avis>> GetAvisByLibelle(string libelle)
        //{
        //    var avis = await dataRepository.GetByStringAsync(libelle);

        //    if (avis == null)
        //    {
        //        return NotFound();
        //    }

        //    return avis;
        //}



        /// <summary>
        /// Modify a single avis.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="204">When the avis and id is valid</response>
        /// <response code="401">When the avis and id is unauthorized</response>
        /// <response code="400">When the avis id is not the same as id</response>
        /// <response code="404">When id is not valid</response>
        // PUT: api/Avis/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutAvis(int id, Avis avis)
        {
            if (id != avis.AvisId)
            {
                return BadRequest();
            }

            var avisToUpdate = await dataRepository.GetByIdAsync(id);


            if (avisToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(avisToUpdate.Value, avis);
                return NoContent();
            }
        }

        /// <summary>
        /// Add a single avis.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="201">When the avis is valid</response>
        /// <response code="401">When the avis is unauthorized</response>
        /// <response code="400">When the avis is not valid</response>
        // POST: api/Avis
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Avis>> PostAvis(Avis avis)
        {
            await dataRepository.AddAsync(avis);

            return CreatedAtAction("GetById", new { id = avis.AvisId }, avis);
        }

        /// <summary>
        /// Delete a single avis.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">When the avis id is valid</response>
        /// <response code="401">When the avis id is unauthorized</response>
        /// <response code="404">When the avis id is not valid</response>
        // DELETE: api/Avis/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAvis(int id)
        {
            var avis = await dataRepository.GetByIdAsync(id);
            if (avis == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(avis.Value);

            return NoContent();
        }

        //    private bool AvisExists(int id)
        //    {
        //        return (_context.Avis?.Any(e => e.AvisId == id)).GetValueOrDefault();
        //    }
    }
}
