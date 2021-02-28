using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using Showroom.Infrastructure.Persistence;
using Showroom.Application.Dtos;
using Showroom.Domain.Entities;
using Showroom.Application.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Showroom.Server.Controllers
{

    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender emailSender;
        private readonly IIdentityService identityService;
        private readonly IProfileImageService profileImageService;
        private readonly IMapper mapper;

        public UserController(
            ApplicationDbContext applicationDbContext,
            UserManager<User> userManager,
            IEmailSender emailSender,
            IIdentityService identityService,
            IProfileImageService profileImageService,
            IMapper mapper)
        {
            this.applicationDbContext = applicationDbContext;
            _userManager = userManager;
            this.emailSender = emailSender;
            this.identityService = identityService;
            this.profileImageService = profileImageService;
            this.mapper = mapper;
        }

        [HttpGet("{id?}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserProfileDto>> GetUserProfile(Guid? id)
        {
            if (id == null)
            {
                var user = await identityService.GetUserAsync();

                UserProfile up = null;
                if ((up = await applicationDbContext
                    .UserProfiles
                    .Include("Reference")
                    .Include("CompetenceArea")
                    .Include("CompetenceArea")
                    .Include("Manager")
                    .Include("Organization")
                    .FirstOrDefaultAsync(x => x.Id == user.ProfileId)) == null)
                {
                    up = new UserProfile()
                    {
                        FirstName = user.Name,
                        LastName = user.Name,
                        Email = user.Email
                    };
                }
                return CreateUserProfileDto(up);
            }
            else
            {

                var profile = await applicationDbContext.UserProfiles
                    .Include("Reference")
                    .Include("CompetenceArea")
                    .Include("CompetenceArea")
                    .Include("Manager")
                    .Include("Organization")
                    .FirstOrDefaultAsync(x => x.Id == id);

                return CreateUserProfileDto(profile);
            }
        }

        private ActionResult<UserProfileDto> CreateUserProfileDto(UserProfile up)
        {
            switch (up)
            {
                case ClientProfile cp:
                    return mapper.Map<ClientProfileDto>(cp);
                case ConsultantProfile ccp:
                    return mapper.Map<ConsultantProfileDto>(ccp);
                case ManagerProfile mp:
                    return mapper.Map<ManagerProfileDto>(mp);
                default:
                    return mapper.Map<UserProfileDto>(up);
            }
        }

        [HttpDelete]
        [Route("UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<IdentityError>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUserProfile()
        {
            var user = await identityService.GetUserAsync();

            var userProfile = applicationDbContext.UserProfiles.Find(user.ProfileId);

            applicationDbContext.UserProfiles.Remove(userProfile);

            await applicationDbContext.SaveChangesAsync();

            await _userManager.DeleteAsync(user);

            return Ok();
        }


        [HttpPut]
        [Route("UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<IdentityError>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UserProfileViewModelDto userProfile)
        {
            var user = await identityService.GetUserAsync();

            var up = applicationDbContext.UserProfiles.Find(user.ProfileId);

            up = mapper.Map(userProfile, up);

            applicationDbContext.UserProfiles.Update(up);

            await applicationDbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("ChangePassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<IdentityError>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangePassword([FromForm] string currentPassword, [FromForm] string newPassword)
        {
            var user = await identityService.GetUserAsync();

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
            }

            return Ok();
        }

        [HttpPost]
        [HttpPut]
        [Route("ProfileImage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<IdentityError>), StatusCodes.Status500InternalServerError)]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> UploadProfileImage(IFormFile formFile)
        {
            var user = await identityService.GetUserAsync();

            if (user == null)
            {
                return NotFound();
            }

            var consultantProfile = await applicationDbContext.UserProfiles.FindAsync(user.ProfileId);
            if (consultantProfile == null)
            {
                return NotFound();
            }

            using (var stream = formFile.OpenReadStream())
            {
                return await profileImageService.UploadProfileImageAsync(user.ProfileId.GetValueOrDefault(), formFile.FileName, stream);
            }
        }

        [HttpDelete]
        [Route("ProfileImage")]
        [Authorize]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProfileImage()
        {
            var user = await identityService.GetUserAsync();

            if (user == null)
            {
                return NotFound();
            }

            var consultantProfile = await applicationDbContext.UserProfiles.FindAsync(user.ProfileId);
            if (consultantProfile == null)
            {
                return NotFound();
            }

            await profileImageService.DeleteProfileImageAsync(user.ProfileId.GetValueOrDefault());

            return Ok();
        }

        [HttpPost]
        [Route("SendEmailConfirmation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<IdentityError>), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [AllowAnonymous]
        public async Task<ActionResult> SendEmailConfirmation(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            await emailSender.SendEmailAsync(
                user.Email,
                "ESSIQ Showroom - Please confirm your E-mail address",
                $"<a href=\"https://localhost:44386/user/confirm?email={email}&token={token}\">Click here</a> to confirm your e-mail address.</a>");

            return Ok();
        }

        [HttpPost]
        [Route("ConfirmEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<IdentityError>), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [AllowAnonymous]
        public async Task<ActionResult<IdentityResult>> ConfirmEmail(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                return StatusCode(500, result.Errors);
            }

            return result;
        }
    }
}
