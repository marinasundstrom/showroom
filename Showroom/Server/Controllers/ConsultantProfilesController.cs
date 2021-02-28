using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Showroom.Application.Consultants.Commands;
using Showroom.Application.Consultants.Queries;
using Showroom.Application.Common.Dtos;
using Showroom.Application.Services;
using Showroom.Domain.Exceptions;
using Showroom.Shared;
using Showroom.Application.Consultants;

namespace Showroom.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConsultantProfilesController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IProfileVideoService profileVideoService;
        private readonly IProfileImageService profileImageService;

        public ConsultantProfilesController(
            IMediator mediator,
            IProfileVideoService profileVideoService,
            IProfileImageService profileImageService)
        {
            this.mediator = mediator;
            this.profileVideoService = profileVideoService;
            this.profileImageService = profileImageService;
        }

        // GET: api/ConsultantProfiles
        [HttpGet]
        public async Task<ActionResult<IQueryable<ConsultantProfileDto>>> GetConsultantProfiles(string organizationId = null, string competenceAreaId = null, DateTime? availableFrom = null, bool justMyOrganization = false, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                return Ok(await mediator.Send(new GetConsultantProfilesQuery(organizationId, competenceAreaId, availableFrom, justMyOrganization, pageNumber, pageSize)));
            }
            catch (NotFoundException exc)
            {
                return Problem(exc.Message, statusCode: StatusCodes.Status404NotFound);
            }
        }

        // GET: api/ConsultantProfiles/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ConsultantProfileDto>> GetConsultantProfile(Guid id)
        {
            try
            {
                return Ok(await mediator.Send(new GetConsultantProfileQuery(id)));
            }
            catch (NotFoundException exc)
            {
                return Problem(exc.Message, statusCode: StatusCodes.Status404NotFound);
            }
        }

        // PUT: api/ConsultantProfiles/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Authorize(Roles = RoleConstants.AdministratorAndManager)]
        public async Task<ActionResult<ConsultantProfileDto>> UpdateConsultantProfile(Guid id, UpdateConsultantProfileDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            try
            {
                var dto2 = await mediator.Send(new UpdateConsultantProfileCommand(dto));

                return Ok(dto2);
            }
            catch (NotFoundException exc)
            {
                return Problem(exc.Message, statusCode: StatusCodes.Status404NotFound);
            }
        }

        // POST: api/ConsultantProfiles
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        [Authorize(Roles = RoleConstants.AdministratorAndManager)]
        public async Task<ActionResult<ConsultantProfileDto>> CreateConsultantProfile(AddConsultantProfileDto dto)
        {
            try
            {
                var consultantProfile = await mediator.Send(new CreateConsultantProfileCommand(dto));

                return CreatedAtAction("GetConsultantProfile", new { id = consultantProfile.Id }, consultantProfile);
            }
            catch (NotFoundException exc)
            {
                return Problem(exc.Message, statusCode: StatusCodes.Status404NotFound);
            }
        }

        // DELETE: api/ConsultantProfiles/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Authorize(Roles = RoleConstants.AdministratorAndManager)]
        public async Task<ActionResult> DeleteConsultantProfile(Guid id)
        {
            try
            {
                await mediator.Send(new DeleteConsultantProfileCommand(id));

                return Ok();
            }
            catch (NotFoundException exc)
            {
                return Problem(exc.Message, statusCode: StatusCodes.Status404NotFound);
            }
        }

        [HttpPost("{id}/ProfileVideo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> UploadVideo(Guid id, IFormFile formFile)
        {
            try
            {
                using (var stream = formFile.OpenReadStream())
                {
                    var fileName = formFile.FileName;
                    return await profileVideoService.UploadProfileVideoAsync(id, fileName, stream);
                }
            }
            catch (NotFoundException exc)
            {
                return Problem(exc.Message, statusCode: StatusCodes.Status404NotFound);
            }
        }

        [HttpDelete("{id}/ProfileVideo")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProfileVideo(Guid id)
        {
            try
            {
                await profileVideoService.DeleteProfileVideoAsync(id);

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

