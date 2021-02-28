using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PasswordGenerator;
using Showroom.Application.Common.Dtos;
using Showroom.Application.Common.Interfaces;
using Showroom.Application.Services;
using Showroom.Domain.Entities;
using Showroom.Domain.Exceptions;
using Showroom.Shared;

namespace Showroom.Application.Clients.Commands
{
    public class CreateClientProfileCommand : IRequest<ClientProfileDto>
    {
        public CreateClientProfileCommand(AddClientProfileDto clientProfile)
        {
            ClientProfile = clientProfile;
        }

        public AddClientProfileDto ClientProfile { get; }

        class CreateConsultantProfileCommandHandler : IRequestHandler<CreateClientProfileCommand, ClientProfileDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper mapper;
            private readonly UserManager<User> _userManager;
            private readonly IIdentityService identityService;

            public CreateConsultantProfileCommandHandler(
                IApplicationDbContext context,
                IMapper mapper,
                IIdentityService identityService, UserManager<User> userManager)
            {
                _context = context;
                this.mapper = mapper;
                this.identityService = identityService;
                _userManager = userManager;
            }

            public async Task<ClientProfileDto> Handle(CreateClientProfileCommand request, CancellationToken cancellationToken)
            {
                var clientProfile = mapper.Map<ClientProfile>(request.ClientProfile);

                var user1 = await identityService.GetUserAsync();

                await _context.Entry(user1).Reference(e => e.Profile).LoadAsync();

                if (request.ClientProfile.OrganizationId == null)
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
        }
    }
}
