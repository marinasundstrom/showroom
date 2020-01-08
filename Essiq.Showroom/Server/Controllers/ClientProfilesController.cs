using System;
using System.Linq;
using System.Threading.Tasks;

using Essiq.Showroom.Server.Dtos;
using Essiq.Showroom.Server.Services;
using Essiq.Showroom.Shared;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Essiq.Showroom.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = RoleConstants.AdministratorAndManager)]
    public class ClientProfilesController : ControllerBase
    {
        private readonly ClientManager clientManager;

        public ClientProfilesController(ClientManager clientManager)
        {
            this.clientManager = clientManager;
        }

        // GET: api/ClientProfiles
        [HttpGet]
        public async Task<ActionResult<IQueryable<ClientProfileDto>>> GetClientProfiles(string organizationId = null)
        {
            try
            {
                return Ok(await clientManager.GetClientProfilesAsync(organizationId));
            }
            catch (NotFoundException exc)
            {
                return Problem(exc.Message, statusCode: StatusCodes.Status404NotFound);
            }
        }

        // GET: api/ClientProfiles/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ClientProfileDto>> GetClientProfile(Guid id)
        {
            try
            {
                return Ok(await clientManager.GetClientProfileAsync(id));
            }
            catch (NotFoundException exc)
            {
                return Problem(exc.Message, statusCode: StatusCodes.Status404NotFound);
            }
        }

        // PUT: api/ClientProfiles/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [AllowAnonymous]
        public async Task<ActionResult<ClientProfileDto>> UpdateClientProfile(Guid id, UpdateClientProfileDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            try
            {
                var dto2 = await clientManager.UpdateClientProfileAsync(dto);

                return Ok(dto2);
            }
            catch (NotFoundException exc)
            {
                return Problem(exc.Message, statusCode: StatusCodes.Status404NotFound);
            }
        }

        // POST: api/ClientProfiles
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ClientProfileDto>> CreateClientProfile(AddClientProfileDto dto)
        {
            try
            {
                var clientProfile = await clientManager.CreateClientProfileAsync(dto);
                return CreatedAtAction("GetClientProfile", new { id = clientProfile.Id }, clientProfile);
            }
            catch (NotFoundException exc)
            {
                return Problem(exc.Message, statusCode: StatusCodes.Status404NotFound);
            }
        }

        // DELETE: api/ClientProfiles/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteClientProfile(Guid id)
        {
            try
            {
                await clientManager.DeleteClientProfileAsync(id);
                return Ok();
            }
            catch (NotFoundException exc)
            {
                return Problem(exc.Message, statusCode: StatusCodes.Status404NotFound);
            }
        }

        [HttpPost("InviteClient")]
        public async Task InviteClient(Guid clientId)
        {
            await clientManager.InviteClientAsync(clientId);
        }
    }
}
