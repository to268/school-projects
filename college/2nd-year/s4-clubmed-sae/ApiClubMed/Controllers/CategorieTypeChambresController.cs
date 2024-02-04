using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Authorization;
using ApiClubMed.Models;

namespace ApiClubMed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorieTypeChambresController : ControllerBase
    {
        private readonly IDataRepository<CategorieTypeChambre> dataRepository;

        public CategorieTypeChambresController(IDataRepository<CategorieTypeChambre> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Get all the categorieTypeChambre.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200"></response>
        /// <response code="401"></response>
        // GET: api/CategorieTypeChambres
        [HttpGet]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        public async Task<ActionResult<IEnumerable<CategorieTypeChambre>>> GetCategorieTypeChambres()
        {
            return await dataRepository.GetAllAsync();
        }


        /// <summary>
        /// Get a single categorieTypeChambre.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the categorieTypeChambre</param>
        /// <response code="200">When the categorieTypeChambre id is found</response>
        /// <response code="401">When the categorieTypeChambre id is unauthorized</response>
        /// <response code="404">When the categorieTypeChambre id is not found</response>
        // GET: api/CategorieTypeChambres/GetById/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategorieTypeChambre>> GetCategorieTypeChambre(int id)
        {
            var categorieTypeChambre = await dataRepository.GetByIdAsync(id);

            if (categorieTypeChambre == null)
            {
                return NotFound();
            }

            return categorieTypeChambre;
        }

        /// <summary>
        /// Get a single categorieTypeChambre.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="libelle">The libelle of the categorieTypeChambre</param>
        /// <response code="200">When the categorieTypeChambre libelle is found</response>
        /// <response code="401">When the categorieTypeChambre libelle is unauthorized</response>
        /// <response code="404">When the categorieTypeChambre libelle is not found</response>
        // GET: api/CategorieTypeChambres/GetByLibelle/deluxe
        [HttpGet]
        [Route("[action]/{libelle}")]
        [ActionName("GetByLibelle")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategorieTypeChambre>> GetCategorieTypeChambreByLibelle(string libelle)
        {
            var categorieTypeChambre = await dataRepository.GetByStringAsync(libelle);

            if (categorieTypeChambre == null)
            {
                return NotFound();
            }

            return categorieTypeChambre;
        }

        /// <summary>
        /// Modify a single categorieTypeChambre.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="204">When the categorieTypeChambre and id is valid</response>
        /// <response code="401">When the categorieTypeChambre and id is unauthorized</response>
        /// <response code="400">When the categorieTypeChambre id is not the same as id</response>
        /// <response code="404">When id is not valid</response>
        // PUT: api/CategorieTypeChambres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutCategorieTypeChambre(int id, CategorieTypeChambre categorieTypeChambre)
        {
            if (id != categorieTypeChambre.CategorieTypeChambreId)
            {
                return BadRequest();
            }

            var categorieTypeChambreToUpdate = await dataRepository.GetByIdAsync(id);


            if (categorieTypeChambreToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(categorieTypeChambreToUpdate.Value, categorieTypeChambre);
                return NoContent();
            }
        }

        /// <summary>
        /// Add a single categorieTypeChambre.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="201">When the categorieTypeChambre is valid</response>
        /// <response code="401">When the categorieTypeChambre is unauthorized</response>
        /// <response code="400">When the categorieTypeChambre is not valid</response>
        // POST: api/CategorieTypeChambres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategorieTypeChambre>> PostCategorieTypeChambre(CategorieTypeChambre categorieTypeChambre)
        {
            await dataRepository.AddAsync(categorieTypeChambre);

            return CreatedAtAction("GetById", new { id = categorieTypeChambre.CategorieTypeChambreId }, categorieTypeChambre);
        }

        /// <summary>
        /// Delete a single categorieTypeChambre.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">When the categorieTypeChambre id is valid</response>
        /// <response code="401">When the categorieTypeChambre id is unauthorized</response>
        /// <response code="404">When the categorieTypeChambre id is not valid</response>
        // DELETE: api/CategorieTypeChambres/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCategorieTypeChambre(int id)
        {
            var categorieTypeChambre = await dataRepository.GetByIdAsync(id);
            if (categorieTypeChambre == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(categorieTypeChambre.Value);

            return NoContent();
        }
        //private bool CategorieTypeChambreExists(int id)
        //{
        //    return (_context.CategorieTypeChambres?.Any(e => e.CategorieTypeChambreId == id)).GetValueOrDefault();
        //}
    }
}
