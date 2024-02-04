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
    [Route("api/cartebancaire")]
    [ApiController]
    public class CarteBanquairesController : ControllerBase
    {
        private readonly IDataRepository<CarteBanquaire> dataRepository;
        public CarteBanquairesController(IDataRepository<CarteBanquaire> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Get all the Carte Banquaires.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200"></response>
        /// <response code="401"></response>
        [HttpGet]
        [Authorize(Policy = Policies.Admin)]
        public async Task<ActionResult<IEnumerable<CarteBanquaire>>> GetCarteBanquaires()
        {
            return await dataRepository.GetAllAsync();
        }

        /// <summary>
        /// Get a single the Carte Banquaire.
        /// </summary>
        /// <param name="id">The id of the Carte Banquaire</param>
        /// <returns>Http response</returns>
        /// <response code="200">When the Carte Banquaire id is found</response>
        /// <response code="401">When the Carte Banquaire id is unauthorized</response>
        /// <response code="404">When the Carte Banquaire id is not found</response>
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CarteBanquaire>> GetCarteBanquaire(int id)
        {
            var carteBancaire = await dataRepository.GetByIdAsync(id);

            if (carteBancaire == null)
            {
                return NotFound();
            }

            return carteBancaire;
        }

        /// <summary>
        /// Get a single Carte Banquaire by numero.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="numero">The name of the Carte Banquaire</param>
        /// <response code="200">When the Carte Banquaire numero is found</response>
        /// <response code="401">When the Carte Banquaire numero is unauthorized</response>
        /// <response code="404">When the Carte Banquaire numero is not found</response>
        [HttpGet]
        [Route("[action]/{numero}")]
        [ActionName("GetByNumero")]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CarteBanquaire>> GetCarteBancaireByNumeroCarte(string numero)
        {
            var carteBancaire = await dataRepository.GetByStringAsync(numero);

            if (carteBancaire == null)
            {
                return NotFound();
            }

            return carteBancaire;
        }

        /// <summary>
        /// Modify a single Carte Banquaire.
        /// </summary>
        /// <param name="id">The id of the Carte Banquaire</param>
        /// <returns>Http response</returns>
        /// <response code="204">When the Carte Banquaire and id is valid</response>
        /// <response code="401">When the Carte Banquaire and id is unauthorized</response>
        /// <response code="400">When the Carte Banquaire id is not the same as id</response>
        /// <response code="404">When id is not valid</response>
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutCarteBanquaire(int id, CarteBanquaire carteBanquaire)
        {
            if (id != carteBanquaire.CarteBanquaireId)
            {
                return BadRequest();
            }

            var carteBancaireToUpdate = await dataRepository.GetByIdAsync(id);
            if (carteBancaireToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(carteBancaireToUpdate.Value, carteBanquaire);
                return NoContent();
            }
        }

        /// <summary>
        /// Add a single Carte Banquaire.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="201">When the Carte Banquaire is valid</response>
        /// <response code="401">When the Carte Banquaire is unauthorized</response>
        /// <response code="400">When the Carte Banquaire is not valid</response>
        [HttpPost]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CarteBanquaire>> PostCarteBanquaire(CarteBanquaire carteBanquaire)
        {
            await dataRepository.AddAsync(carteBanquaire);

            return CreatedAtAction("GetById", new { id = carteBanquaire.CarteBanquaireId }, carteBanquaire);
        }

        /// <summary>
        /// Delete a single Carte Banquaire.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">When the Carte Banquaire id is valid</response>
        /// <response code="401">When the Carte Banquaire id is unauthorized</response>
        /// <response code="404">When the Carte Banquaire id is not valid</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCarteBanquaire(int id)
        {
            var carteBancaire = await dataRepository.GetByIdAsync(id);
            if (carteBancaire == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(carteBancaire.Value);

            return NoContent();
        }

        //private bool CarteBanquaireExists(int id)
        //{
        //    return (_context.CarteBanquaires?.Any(e => e.CarteBanquaireId == id)).GetValueOrDefault();
        //}
    }
}
