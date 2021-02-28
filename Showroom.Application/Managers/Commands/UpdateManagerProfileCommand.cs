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

namespace Showroom.Application.Managers.Commands
{
    public class UpdateManagerProfileCommand : IRequest<ManagerProfileDto>
    {
        public UpdateManagerProfileCommand(UpdateManagerProfileDto managerProfile)
        {
            ManagerProfile = managerProfile;
        }

        public UpdateManagerProfileDto ManagerProfile { get; }

        class UpdateManagerProfileCommandHandler : IRequestHandler<UpdateManagerProfileCommand, ManagerProfileDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper mapper;

            public UpdateManagerProfileCommandHandler(
                IApplicationDbContext context,
                IMapper mapper)
            {
                _context = context;
                this.mapper = mapper;
            }

            public async Task<ManagerProfileDto> Handle(UpdateManagerProfileCommand request, CancellationToken cancellationToken)
            {
                var managerProfile = await _context.ManagersProfiles.FindAsync(request.ManagerProfile.Id);

                if (managerProfile == null)
                {
                    throw new NotFoundException(nameof(ManagerProfile), request.ManagerProfile.Id);
                }

                managerProfile = mapper.Map(request.ManagerProfile, managerProfile);

                _context.Update(managerProfile);

                await _context.SaveChangesAsync();

                return mapper.Map<ManagerProfileDto>(managerProfile);
            }
        }
    }
}
