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
    public class TypeChambresController : ControllerBase
    {
        private readonly IDataRepository<TypeChambre> dataRepository;

        public TypeChambresController(IDataRepository<TypeChambre> dataRepo)
        {
            dataRepository = dataRepo;
        }


        /// <summary>
        /// Get all the typeChambre.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200"></response>
        /// <response code="401"></response>
        // GET: api/TypeChambres
        [HttpGet]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        public async Task<ActionResult<IEnumerable<TypeChambre>>> GetTypeChambres()
        {
            return await dataRepository.GetAllAsync();
        }

        /// <summary>
        /// Get a single typeChambre.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the typeChambre</param>
        /// <response code="200">When the typeChambre id is found</response>
        /// <response code="401">When the typeChambre id is unauthorized</response>
        /// <response code="404">When the typeChambre id is not found</response>
        // GET: api/TypeChambres/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TypeChambre>> GetTypeChambre(int id)
        {
            var typeChambre = await dataRepository.GetByIdAsync(id);

            if (typeChambre == null)
            {
                return NotFound();
            }

            return typeChambre;
        }

        /// <summary>
        /// Get a single typeChambre.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="name">The libelle of the typeChambre</param>
        /// <response code="200">When the typeChambre libelle is found</response>
        /// <response code="401">When the typeChambre libelle is unauthorized</response>
        /// <response code="404">When the typeChambre libelle is not found</response>
        // GET: api/GetTypeChambreByLibelle/deluxe
        [HttpGet]
        [Route("[action]/{libelle}")]
        [ActionName("GetByLibelle")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TypeChambre>> GetTypeChambreByLibelle(string libelle)
        {
            var typeChambre = await dataRepository.GetByStringAsync(libelle);

            if (typeChambre == null)
            {
                return NotFound();
            }

            return typeChambre;
        }

        //// GET: api/GetTypeChambreByPrixInf/deluxe
        //[HttpGet]
        //[Route("[action]/{libelle}")]
        //[ActionName("GetTypeChambreByPrixInf")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<TypeChambre>> GetTypeChambreByPrixInf(string libelle)
        //{
        //    var typeChambre = await dataRepository.get(libelle);

        //    if (typeChambre == null)
        //    {
        //        return NotFound();
        //    }

        //    return typeChambre;
        //}
        // GET: api/GetTypeChambreByPrixInf/deluxe

        //[HttpGet]
        //[Route("[action]/{libelle}")]
        //[ActionName("GetTypeChambreByPrixSup")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<TypeChambre>> GetTypeChambreByPrixSup(string libelle)
        //{
        //    var typeChambre = await dataRepository.GetTypeChambreByPrixSup(libelle);

        //    if (typeChambre == null)
        //    {
        //        return NotFound();
        //    }

        //    return typeChambre;
        //}

        /// <summary>
        /// Modify a single typeChambre.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="204">When the typeChambre and id is valid</response>
        /// <response code="401">When the typeChambre and id is unauthorized</response>
        /// <response code="400">When the typeChambre id is not the same as id</response>
        /// <response code="404">When id is not valid</response>
        // PUT: api/TypeChambres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutTypeChambre(int id, TypeChambre typeChambre)
        {
            if (id != typeChambre.TypeChambreId)
            {
                return BadRequest();
            }

            var typeChambreToUpdate = await dataRepository.GetByIdAsync(id);


            if (typeChambreToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(typeChambreToUpdate.Value, typeChambre);
                return NoContent();
            }
        }


        /// <summary>
        /// Add a single typeChambre.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="201">When the typeChambre is valid</response>
        /// <response code="401">When the typeChambre is unauthorized</response>
        /// <response code="400">When the typeChambre is not valid</response>
        // POST: api/TypeChambres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TypeChambre>> PostTypeChambre(TypeChambre typeChambre)
        {
            await dataRepository.AddAsync(typeChambre);

            return CreatedAtAction("GetById", new { id = typeChambre.TypeChambreId }, typeChambre);
        }

        /// <summary>
        /// Delete a single typeChambre.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">When the typeChambre id is valid</response>
        /// <response code="401">When the typeChambre id is unauthorized</response>
        /// <response code="404">When the typeChambre id is not valid</response>
        // DELETE: api/TypeChambres/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTypeChambre(int id)
        {
            var typeChambre = await dataRepository.GetByIdAsync(id);
            if (typeChambre == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(typeChambre.Value);

            return NoContent();
        }

        //private bool TypeChambreExists(int id)
        //{
        //    return (_context.TypeChambres?.Any(e => e.TypeChambreId == id)).GetValueOrDefault();
        //}
    }
}
