using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Showroom.Application.Common.Dtos;
using Showroom.Application.Managers;
using Showroom.Application.Managers.Commands;
using Showroom.Application.Managers.Queries;
using Showroom.Application.Services;
using Showroom.Domain.Exceptions;
using Showroom.Shared;

namespace Showroom.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ManagersController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IProfileImageService profileImageService;

        public ManagersController(IMediator mediator, IProfileImageService profileImageService)
        {
            this.mediator = mediator;
            this.profileImageService = profileImageService;
        }

        // GET: api/Managers
        [HttpGet]
        public async Task<ActionResult<IQueryable<ManagerProfileDto>>> GetManagers(string organizationId = null)
        {
            try
            {
                return Ok(await mediator.Send(new GetManagerProfilesQuery(organizationId)));
            }
            catch (NotFoundException exc)
            {
                return Problem(exc.Message, statusCode: StatusCodes.Status404NotFound);
            }
        }

        // GET: api/Managers/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ManagerProfileDto>> GetManager(Guid id)
        {
            try
            {
                return Ok(await mediator.Send(new GetManagerProfileQuery(id)));
            }
            catch (NotFoundException exc)
            {
                return Problem(exc.Message, statusCode: StatusCodes.Status404NotFound);
            }
        }

        // PUT: api/Managers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Authorize(Roles = RoleConstants.AdministratorAndManager)]
        public async Task<IActionResult> UpdateManager(Guid id, UpdateManagerProfileDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            try
            {

                var dto2 = await mediator.Send(new UpdateManagerProfileCommand(dto));

                return Ok(dto2);
            }
            catch (NotFoundException exc)
            {
                return Problem(exc.Message, statusCode: StatusCodes.Status404NotFound);
            }
        }

        // POST: api/Managers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        [Authorize(Roles = RoleConstants.AdministratorAndManager)]
        public async Task<ActionResult<ManagerProfileDto>> CreateManager(AddManagerProfileDto dto)
        {
            try
            {
                var managerProfile = await mediator.Send(new CreateManagerProfileCommand(dto));
                return CreatedAtAction("GetManagerProfile", new { id = managerProfile.Id }, managerProfile);
            }
            catch (NotFoundException exc)
            {
                return Problem(exc.Message, statusCode: StatusCodes.Status404NotFound);
            }
        }

        // DELETE: api/Managers/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Authorize(Roles = RoleConstants.AdministratorAndManager)]
        public async Task<ActionResult<ManagerProfileDto>> DeleteManager(Guid id)
        {
            try
            {
                await mediator.Send(new DeleteManagerProfileCommand(id));
                return Ok();
            }
            catch (NotFoundException exc)
            {
                return Problem(exc.Message, statusCode: StatusCodes.Status404NotFound);
            }
        }


        [HttpPost("{id}/ProfileImage")]
        [HttpPut("{id}/ProfileImage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<IdentityError>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> UploadProfileImage(Guid id, IFormFile formFile)
        {
            try
            {
                using (var stream = formFile.OpenReadStream())
                {
                    var fileName = formFile.FileName;
                    return await profileImageService.UploadProfileImageAsync(id, fileName, stream);
                }
            }
            catch (NotFoundException exc)
            {
                return Problem(exc.Message, statusCode: StatusCodes.Status404NotFound);
            }
        }

        [HttpDelete("{id}/ProfileImage")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteProfileImage(Guid id)
        {
            try
            {
                await profileImageService.DeleteProfileImageAsync(id);

                return Ok();
            }
            catch (NotFoundException exc)
            {
                return Problem(exc.Message, statusCode: StatusCodes.Status404NotFound);
            }
        }
    }
}
