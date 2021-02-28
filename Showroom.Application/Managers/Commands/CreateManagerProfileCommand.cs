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

namespace Showroom.Application.Managers.Commands
{
    public class CreateManagerProfileCommand : IRequest<ManagerProfileDto>
    {
        public CreateManagerProfileCommand(AddManagerProfileDto managerProfile)
        {
            ManagerProfile = managerProfile;
        }

        public AddManagerProfileDto ManagerProfile { get; }

        class CreateManagerProfileCommandHandler : IRequestHandler<CreateManagerProfileCommand, ManagerProfileDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper mapper;
            private readonly IIdentityService identityService;

            public CreateManagerProfileCommandHandler(
                IApplicationDbContext context,
                IMapper mapper,
                IIdentityService identityService)
            {
                _context = context;
                this.mapper = mapper;
                this.identityService = identityService;
            }

            public async Task<ManagerProfileDto> Handle(CreateManagerProfileCommand request, CancellationToken cancellationToken)
            {
                var managerProfile = mapper.Map<ManagerProfile>(request.ManagerProfile);

                var user = await identityService.GetUserAsync();

                await _context.Entry(user).Reference(e => e.Profile).LoadAsync();

                if (request.ManagerProfile.OrganizationId == null)
                {
                    managerProfile.OrganizationId = user.Profile.OrganizationId;
                }

                _context.ManagersProfiles.Add(managerProfile);
                await _context.SaveChangesAsync();

                return mapper.Map<ManagerProfileDto>(managerProfile);
            }
        }
    }
}
