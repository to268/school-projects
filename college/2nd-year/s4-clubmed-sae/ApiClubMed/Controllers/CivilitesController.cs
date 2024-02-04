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
    public class CivilitesController : ControllerBase
    {
        private readonly IDataRepository<Civilite> dataRepository;

        public CivilitesController(IDataRepository<Civilite> dataRepo)
        {
            dataRepository = dataRepo;
        }


        /// <summary>
        /// Get all the civilites.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200"></response>
        /// <response code="401"></response>
        // GET: api/Civilites
        [HttpGet]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        public async Task<ActionResult<IEnumerable<Civilite>>> GetCivilites()
        {
            return await dataRepository.GetAllAsync();

        }

        /// <summary>
        /// Get a single civilite.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the civilite</param>
        /// <response code="200">When the civilite id is found</response>
        /// <response code="401">When the civilite id is unauthorized</response>
        /// <response code="404">When the civilite id is not found</response>
        // GET: api/Civilites/GetById/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Civilite>> GetCivilite(int id)
        {
            var civilite = await dataRepository.GetByIdAsync(id);

            if (civilite == null)
            {
                return NotFound();
            }

            return civilite;
        }


        /// <summary>
        /// Get a single civilite.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="libelle">The libelle of the civilite</param>
        /// <response code="200">When the civilite libelle is found</response>
        /// <response code="401">When the civilite libelle is unauthorized</response>
        /// <response code="404">When the civilite libelle is not found</response>
        [HttpGet]
        [Route("[action]/{libelle}")]
        [ActionName("GetByLibelle")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Civilite>> GetCiviliteByLibelle(string libelle)
        {
            var civilite = await dataRepository.GetByStringAsync(libelle);

            if (civilite == null)
            {
                return NotFound();
            }

            return civilite;
        }



        /// <summary>
        /// Modify a single civilite.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="400">You cannot put a civilite</response>
        /// <response code="401">You cannot put a unauthorized</response>
        [HttpPut]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutCivilite(int id, Civilite civilite)
        {
            return BadRequest();
        }

        /// <summary>
        /// Add a single civilite.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="400">You cannot post a civilite</response>
        /// <response code="401">You cannot post a unauthorized</response>
        [HttpPost]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Civilite>> PostCivilite(Civilite civilite)
        {
            return BadRequest();
        }

        /// <summary>
        /// Delete a single civilite.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="400">You cannot delete a civilite</response>
        /// <response code="401">You cannot delete a unauthorized</response>
        [HttpDelete]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteCivilite(int id)
        {
            return BadRequest();
        }

        //private bool CiviliteExists(int id)
        //{
        //    return (_context.Civilites?.Any(e => e.CiviliteId == id)).GetValueOrDefault();
        //}
    }
}
