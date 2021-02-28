using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PasswordGenerator;
using Showroom.Application.Common.Interfaces;
using Showroom.Application.Common.Dtos;
using Showroom.Domain.Entities;
using Showroom.Domain.Exceptions;
using Showroom.Shared;
using ClientProfile = Showroom.Domain.Entities.ClientProfile;

namespace Showroom.Application.Services
{
    public class ClientManager
    {
        private readonly IApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IMapper mapper;
        private readonly IEmailSender emailSender;
        private readonly IIdentityService identityService;

        public ClientManager(
            IApplicationDbContext context,
            UserManager<User> userManager,
            IMapper mapper,
            IEmailSender emailSender,
            IIdentityService identityService)
        {
            _context = context;
            _userManager = userManager;
            this.mapper = mapper;
            this.emailSender = emailSender;
            this.identityService = identityService;
        }
        public async Task<ClientProfileDto> GetClientProfileAsync(Guid id)
        {
            var clientProfile = await _context
                .ClientProfiles
                .Include(c => c.Reference)
                .Include(x => x.Organization)
                .FirstAsync(c => c.Id == id);

            if (clientProfile == null)
            {
                throw new NotFoundException(nameof(ClientProfile), id);
            }

            return mapper.Map<ClientProfileDto>(clientProfile);
        }

        public async Task<IQueryable<ClientProfileDto>> GetClientProfilesAsync(string organizationId = null)
        {
            var user = await identityService.GetUserAsync();

            await _context.Entry(user).Reference(e => e.Profile).LoadAsync();
            if (user.Profile.OrganizationId != null)
            {
                await _context.Entry(user.Profile).Reference(e => e.Organization).LoadAsync();
            }

            IQueryable<ClientProfile> result = _context
                    .ClientProfiles
                    .Include(x => x.Organization)
                    .Include(c => c.Reference);

            if (user.Profile.OrganizationId != null)
            {
                result = result.Where(e => e.OrganizationId == user.Profile.OrganizationId || e.OrganizationId == null);
            }

            if (organizationId != null)
            {
                var organization = await _context.Organizations.FindAsync(organizationId);
                if (organization == null)
                {
                    throw new NotFoundException(nameof(Organization), organizationId);
                }

                result = result.Where(e => e.OrganizationId == organizationId);
            }

            return mapper.ProjectTo<ClientProfileDto>(result);
        }

        public async Task<ClientProfileDto> CreateClientProfileAsync(AddClientProfileDto dto)
        {
            var clientProfile = mapper.Map<ClientProfile>(dto);

            var user1 = await identityService.GetUserAsync();

            await _context.Entry(user1).Reference(e => e.Profile).LoadAsync();

            if (dto.OrganizationId == null)
            {
                clientProfile.OrganizationId = user1.Profile.OrganizationId;
            }

            var entry = _context.ClientProfiles.Add(clientProfile);
            await _context.SaveChangesAsync();

            var user = new Domain.Entities.User
            {
                Name = clientProfile.FirstName,
                UserName = clientProfile.Email,
                Email = clientProfile.Email,
                ProfileId = entry.Entity.Id
            };

            var password = new Password(20).Next();

            var result = await _userManager.CreateAsync(user, password);

            await _userManager.AddToRoleAsync(user, RoleConstants.Client);

            //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            //await emailSender.SendEmailAsync(
            //    user.Email,
            //    "ESSIQ Showroom - Please confirm your E-mail address",
            //    $"<a href=\"https://localhost:44386/user/confirm?email={user.Email}&token={token.Replace("=", "%3D")}\">Click here</a> to confirm your e-mail address.</a>");

            return mapper.Map<ClientProfileDto>(clientProfile);
        }

        public async Task InviteClientAsync(Guid clientId)
        {
            var clientProfile = await _context.ClientProfiles.FindAsync(clientId);

            if (clientProfile == null)
            {
                throw new NotFoundException(nameof(ClientProfile), clientId);
            }

            var password = new Password(true, true, true, true, 10).Next();

            var user = new User
            {
                UserName = clientProfile.Email,
                Email = clientProfile.Email,
                Name = string.IsNullOrEmpty(clientProfile.DisplayName) ? clientProfile.DisplayName : $"{clientProfile.FirstName} {clientProfile.LastName}"
            };

            await _userManager.CreateAsync(user, password);

            var email = "robert.sundstrom@outlook.com"; // clientProfile.Email;

            await emailSender.SendEmailAsync(email, "Invitation", $"Password: {password}\nWebsite: <URL>");
        }

        public async Task<ClientProfileDto> UpdateClientProfileAsync(UpdateClientProfileDto dto)
        {
            var clientProfile = await _context.ClientProfiles.FindAsync(dto.Id);

            if (clientProfile == null)
            {
                throw new NotFoundException(nameof(ClientProfile), dto.Id);
            }

            clientProfile = mapper.Map(dto, clientProfile);

            _context.Update(clientProfile);

            await _context.SaveChangesAsync();

            return mapper.Map<ClientProfileDto>(clientProfile);
        }

        public async Task DeleteClientProfileAsync(Guid id)
        {
            var clientProfile = await _context.ClientProfiles.FindAsync(id);

            if (clientProfile == null)
            {
                throw new NotFoundException(nameof(ClientProfile), id);
            }

            _context.ClientProfiles.Remove(clientProfile);

            await _context.SaveChangesAsync();

            var user = await _userManager.FindByNameAsync($"u{id}");
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
            }
        }
    }
}
