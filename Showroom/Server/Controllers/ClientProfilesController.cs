using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Showroom.Application.Clients;
using Showroom.Application.Clients.Commands;
using Showroom.Application.Clients.Queries;
using Showroom.Application.Common.Dtos;
using Showroom.Application.Services;
using Showroom.Domain.Exceptions;
using Showroom.Shared;

namespace Showroom.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = RoleConstants.AdministratorAndManager)]
    public class ClientProfilesController : ControllerBase
    {
        private readonly IMediator mediator;

        public ClientProfilesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: api/ClientProfiles
        [HttpGet]
        public async Task<ActionResult<IQueryable<ClientProfileDto>>> GetClientProfiles(string organizationId = null)
        {
            try
            {
                return Ok(await mediator.Send(new GetClientProfilesQuery(organizationId)));
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
                return Ok(await mediator.Send(new GetClientProfileQuery(id)));
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
                var dto2 = await mediator.Send(new UpdateClientProfileCommand(dto));

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
                var clientProfile = await mediator.Send(new CreateClientProfileCommand(dto));
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
                await mediator.Send(new DeleteClientProfileCommand(id));
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
            await mediator.Send(new InviteClientCommand(clientId));
        }
    }
}
