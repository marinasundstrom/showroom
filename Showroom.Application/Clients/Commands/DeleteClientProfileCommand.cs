using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Showroom.Application.Common.Dtos;
using Showroom.Application.Common.Interfaces;
using Showroom.Application.Services;
using Showroom.Domain.Entities;
using Showroom.Domain.Exceptions;

namespace Showroom.Application.Clients.Commands
{
    public class DeleteClientProfileCommand : IRequest
    {
        public DeleteClientProfileCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }

        class DeleteClientProfileCommandHandler : IRequestHandler<DeleteClientProfileCommand>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper mapper;
            private readonly UserManager<User> _userManager;
            private readonly IIdentityService identityService;

            public DeleteClientProfileCommandHandler(
                IApplicationDbContext context,
                IMapper mapper,
                IIdentityService identityService, UserManager<User> userManager)
            {
                _context = context;
                this.mapper = mapper;
                this.identityService = identityService;
                _userManager = userManager;
            }

            public async Task<Unit> Handle(DeleteClientProfileCommand request, CancellationToken cancellationToken)
            {
                var clientProfile = await _context.ClientProfiles.FindAsync(request.Id);

                if (clientProfile == null)
                {
                    throw new NotFoundException(nameof(ClientProfile), request.Id);
                }

                _context.ClientProfiles.Remove(clientProfile);

                await _context.SaveChangesAsync();

                var user = await _userManager.FindByNameAsync($"u{request.Id}");
                if (user != null)
                {
                    var result = await _userManager.DeleteAsync(user);
                }

                return Unit.Value;
            }
        }
    }
}
