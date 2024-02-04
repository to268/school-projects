using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using static System.Net.Mime.MediaTypeNames;
using ApiClubMed.Models;
using Microsoft.AspNetCore.Authorization;

namespace ApiClubMed.Controllers
{
    [Route("api/client")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IDataRepository<Client> dataRepository;
        public ClientsController(IDataRepository<Client> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Get all the Clients.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200"></response>
        /// <response code="401"></response>
        [HttpGet]
        [Authorize(Policy = Policies.Admin)]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            return await dataRepository.GetAllAsync();
        }

        /// <summary>
        /// Get a single the Client.
        /// </summary>
        /// <param name="id">The id of the Client</param>
        /// <returns>Http response</returns>
        /// <response code="200">When the Client id is found</response>
        /// <response code="401">When the Client id is unauthorized</response>
        /// <response code="404">When the Client id is not found</response>
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var client = await dataRepository.GetByIdAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        /// <summary>
        /// Get a single Client by email.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="email">The name of the Client</param>
        /// <response code="200">When the Client email is found</response>
        /// <response code="401">When the Client email is unauthorized</response>
        /// <response code="404">When the Client email is not found</response>
        [HttpGet]
        [Route("[action]/{email}")]
        [ActionName("GetByEmail")]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Client>> GetClientByEmail(string email)
        {
            var client = await dataRepository.GetByStringAsync(email);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        /// <summary>
        /// Modify a single Client.
        /// </summary>
        /// <param name="id">The id of the Client</param>
        /// <returns>Http response</returns>
        /// <response code="204">When the Client and id is valid</response>
        /// <response code="401">When the Client and id is unauthorized</response>
        /// <response code="400">When the Client id is not the same as id</response>
        /// <response code="404">When id is not valid</response>
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutClient(int id, Client client)
        {
            if (id != client.ClientId)
            {
                return BadRequest();
            }

            var clientToUpdate = await dataRepository.GetByIdAsync(id);
            if (clientToUpdate == null)
            {
                return NotFound();
            }
            else
            {

                await dataRepository.UpdateAsync(clientToUpdate.Value, client);
                return NoContent();
            }
        }

        /// <summary>
        /// Add a single Client.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="201">When the Client is valid</response>
        /// <response code="401">When the Client is unauthorized</response>
        /// <response code="400">When the Client is not valid</response>
        [HttpPost]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {

            string salt = "";
            // Uses SHA256 to create the hash
            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                // Convert the string to a byte array first, to be processed
                byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(client.Password + salt);
                byte[] hashBytes = sha.ComputeHash(textBytes);

                // Convert back to a string, removing the '-' that BitConverter adds
                string hash = BitConverter
                    .ToString(hashBytes)
                    .Replace("-", String.Empty);

                client.Password = hash;
            }
            await dataRepository.AddAsync(client);

            return CreatedAtAction("GetById", new { id = client.ClientId }, client);
        }

        /// <summary>
        /// Hash a single Mdp.
        /// </summary>
        /// <returns>hash mdp</returns>
        /// <response code="400">When the Client is not valid</response>
        /// <response code="401">When the Client is not unauthorized</response>
        [HttpPost()]
        [Route("[action]")]
        [ActionName("HashMdp")]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> HashMdp(string mdp)
        {
            if (String.IsNullOrEmpty(mdp))
            {
                return BadRequest();
            }

            string salt = "";
            // Uses SHA256 to create the hash
            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                // Convert the string to a byte array first, to be processed
                byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(mdp + salt);
                byte[] hashBytes = sha.ComputeHash(textBytes);

                // Convert back to a string, removing the '-' that BitConverter adds
                string hash = BitConverter
                    .ToString(hashBytes)
                    .Replace("-", String.Empty);

                mdp = hash;
            }

            return mdp;
        }

        /// <summary>
        /// Delete a single Client.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">When the Client id is valid</response>
        /// <response code="401">When the Client id is unauthorized</response>
        /// <response code="404">When the Client id is not valid</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await dataRepository.GetByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(client.Value);

            return NoContent();
        }

        //private bool ClientExists(int id)
        //{
        //    return (_context.Clients?.Any(e => e.ClientId == id)).GetValueOrDefault();
        //}
    }
}
