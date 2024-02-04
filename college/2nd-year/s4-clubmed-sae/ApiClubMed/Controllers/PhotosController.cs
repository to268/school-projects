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
    public class PhotosController : ControllerBase
    {
        private readonly IDataRepository<Photo> dataRepository;

        public PhotosController(IDataRepository<Photo> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Get all the Photos.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200"></response>
        /// <response code="401"></response>
        // GET: api/Photos
        [HttpGet]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        public async Task<ActionResult<IEnumerable<Photo>>> GetPhotos()
        {
            return await dataRepository.GetAllAsync();

        }

        /// <summary>
        /// Get a single Photo.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the Photo</param>
        /// <response code="200">When the Photo id is found</response>
        /// <response code="401">When the Photo id is unauthorized</response>
        /// <response code="404">When the Photo id is not found</response>
        // GET: api/Photos/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Photo>> GetPhoto(int id)
        {
            var photo = await dataRepository.GetByIdAsync(id);

            if (photo == null)
            {
                return NotFound();
            }

            return photo;
        }

        /// <summary>
        /// Get a single photo.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="link">The link of the photo</param>
        /// <response code="200">When the photo link is found</response>
        /// <response code="401">When the photo link is unauthorized</response>
        /// <response code="404">When the photo link is not found</response>
        // GET: api/GetPhotoByLibelle/Liens
        [HttpGet]
        [Route("[action]/{libelle}")]
        [ActionName("GetByLink")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Photo>> GetPhotoByLink(string link)
        {
            var photo = await dataRepository.GetByStringAsync(link);

            if (photo == null)
            {
                return NotFound();
            }

            return photo;
        }

        /// <summary>
        /// Modify a single photo.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="204">When the photo and id is valid</response>
        /// <response code="401">When the photo and id is unauthorized</response>
        /// <response code="400">When the photo id is not the same as id</response>
        /// <response code="404">When id is not valid</response>
        // PUT: api/Photos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutPhoto(int id, Photo photo)
        {
            if (id != photo.PhotoId)
            {
                return BadRequest();
            }

            var photoToUpdate = await dataRepository.GetByIdAsync(id);


            if (photoToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(photoToUpdate.Value, photo);
                return NoContent();
            }
        }

        /// <summary>
        /// Add a single photo.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="201">When the photo is valid</response>
        /// <response code="401">When the photo is unauthorized</response>
        /// <response code="400">When the photo is not valid</response>
        // POST: api/Photos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Photo>> PostPhoto(Photo photo)
        {
            await dataRepository.AddAsync(photo);

            return CreatedAtAction("GetById", new { id = photo.PhotoId }, photo);
        }

        /// <summary>
        /// Delete a single photo.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">When the photo id is valid</response>
        /// <response code="401">When the photo id is unauthorized</response>
        /// <response code="404">When the photo id is not valid</response>
        // DELETE: api/Photos/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            var photo = await dataRepository.GetByIdAsync(id);
            if (photo == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(photo.Value);

            return NoContent();
        }

        //private bool PhotoExists(int id)
        //{
        //    return (_context.Photos?.Any(e => e.PhotoId == id)).GetValueOrDefault();
        //}
    }
}
