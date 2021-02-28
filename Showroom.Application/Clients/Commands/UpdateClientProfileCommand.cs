using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Showroom.Application.Common.Dtos;
using Showroom.Application.Common.Interfaces;
using Showroom.Application.Services;
using Showroom.Domain.Entities;
using Showroom.Domain.Exceptions;

namespace Showroom.Application.Clients.Commands
{
    public class UpdateClientProfileCommand : IRequest<ClientProfileDto>
    {
        public UpdateClientProfileCommand(UpdateClientProfileDto clientProfile)
        {
            ClientProfile = clientProfile;
        }

        public UpdateClientProfileDto ClientProfile { get; }

        class UpdateConsultantProfileCommandHandler : IRequestHandler<UpdateClientProfileCommand, ClientProfileDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper mapper;
            private readonly IIdentityService identityService;

            public UpdateConsultantProfileCommandHandler(
                IApplicationDbContext context,
                IMapper mapper,
                IIdentityService identityService)
            {
                _context = context;
                this.mapper = mapper;
                this.identityService = identityService;
            }

            public async Task<ClientProfileDto> Handle(UpdateClientProfileCommand request, CancellationToken cancellationToken)
            {
                var clientProfile = await _context.ClientProfiles.FindAsync(request.ClientProfile.Id);

                if (clientProfile == null)
                {
                    throw new NotFoundException(nameof(ClientProfile), request.ClientProfile.Id);
                }

                clientProfile = mapper.Map(request.ClientProfile, clientProfile);

                _context.Update(clientProfile);

                await _context.SaveChangesAsync();

                return mapper.Map<ClientProfileDto>(clientProfile);
            }
        }
    }
}
