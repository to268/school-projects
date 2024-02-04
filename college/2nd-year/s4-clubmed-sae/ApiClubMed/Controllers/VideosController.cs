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
    public class VideosController : ControllerBase
    {
        private readonly IDataRepository<Video> dataRepository;

        public VideosController(IDataRepository<Video> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Get all the video.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200"></response>
        /// <response code="401"></response>
        // GET: api/Videos
        [HttpGet]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        public async Task<ActionResult<IEnumerable<Video>>> GetVideos()
        {
            return await dataRepository.GetAllAsync();

        }

        /// <summary>
        /// Get a single video.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param link="id">The id of the video</param>
        /// <response code="200">When the video id is found</response>
        /// <response code="401">When the video id is unauthorized</response>
        /// <response code="404">When the video id is not found</response>
        // GET: api/Videos/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Video>> GetVideo(int id)
        {
            var video = await dataRepository.GetByIdAsync(id);

            if (video == null)
            {
                return NotFound();
            }

            return video;
        }

        /// <summary>
        /// Get a single video.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="link">The link of the video</param>
        /// <response code="200">When the video link is found</response>
        /// <response code="401">When the video link is unauthorized</response>
        /// <response code="404">When the video link is not found</response>
        // GET: api/GetVideoByLibelle/Liens
        [HttpGet]
        [Route("[action]/{link}")]
        [ActionName("GetByLink")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Video>> GetVideoByLink(string link)
        {
            var video = await dataRepository.GetByStringAsync(link);

            if (video == null)
            {
                return NotFound();
            }

            return video;
        }

        /// <summary>
        /// Modify a single video.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="204">When the video and id is valid</response>
        /// <response code="401">When the video and id is unauthorized</response>
        /// <response code="400">When the video id is not the same as id</response>
        /// <response code="404">When id is not valid</response>
        // PUT: api/Videos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutVideo(int id, Video video)
        {
            if (id != video.VideoId)
            {
                return BadRequest();
            }

            var videoToUpdate = await dataRepository.GetByIdAsync(id);


            if (videoToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(videoToUpdate.Value, video);
                return NoContent();
            }
        }


        /// <summary>
        /// Add a single video.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="201">When the video is valid</response>
        /// <response code="401">When the video is unauthorized</response>
        /// <response code="400">When the video is not valid</response>
        // POST: api/Videos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Video>> PostVideo(Video video)
        {
            await dataRepository.AddAsync(video);

            return CreatedAtAction("GetById", new { id = video.VideoId }, video);
        }

        /// <summary>
        /// Delete a single video.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">When the video id is valid</response>
        /// <response code="401">When the video id is unauthorized</response>
        /// <response code="404">When the video id is not valid</response>
        // DELETE: api/Videos/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.ClientOrAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVideo(int id)
        {
            var video = await dataRepository.GetByIdAsync(id);
            if (video == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(video.Value);

            return NoContent();
        }

        //private bool VideoExists(int id)
        //{
        //    return (_context.Videos?.Any(e => e.VideoId == id)).GetValueOrDefault();
        //}
    }
}
