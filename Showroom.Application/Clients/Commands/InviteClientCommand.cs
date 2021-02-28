using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Showroom.Application.Common.Interfaces;
using Showroom.Application.Common.Dtos;
using Showroom.Application.Services;
using Showroom.Domain.Entities;
using Showroom.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using PasswordGenerator;
using Showroom.Shared;

namespace Showroom.Application.Clients.Commands
{
    public class InviteClientCommand : IRequest
    {
        public InviteClientCommand(Guid clientId)
        {
            ClientId = clientId;
        }

        public Guid ClientId { get; }

        class InviteClientCommandHandler : IRequestHandler<InviteClientCommand>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper mapper;
            private readonly UserManager<User> _userManager;
            private readonly IEmailSender emailSender;
            private readonly IIdentityService identityService;

            public InviteClientCommandHandler(
                IApplicationDbContext context,
                IMapper mapper,
                IIdentityService identityService,
                UserManager<User> userManager,
                IEmailSender emailSender)
            {
                _context = context;
                this.mapper = mapper;
                this.identityService = identityService;
                _userManager = userManager;
                this.emailSender = emailSender;
            }

            public async Task<Unit> Handle(InviteClientCommand request, CancellationToken cancellationToken)
            {
                var clientProfile = await _context.ClientProfiles.FindAsync(request.ClientId);

                if (clientProfile == null)
                {
                    throw new NotFoundException(nameof(ClientProfile), request.ClientId);
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

                return Unit.Value;
            }
        }
    }
}
