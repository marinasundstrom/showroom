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

namespace Showroom.Application.Consultants.Commands
{
    public class UpdateConsultantProfileCommand : IRequest<ConsultantProfileDto>
    {
        public UpdateConsultantProfileCommand(UpdateConsultantProfileDto consultantProfile)
        {
            ConsultantProfile = consultantProfile;
        }

        public UpdateConsultantProfileDto ConsultantProfile { get; }

        class UpdateConsultantProfileCommandHandler : IRequestHandler<UpdateConsultantProfileCommand, ConsultantProfileDto>
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

            public async Task<ConsultantProfileDto> Handle(UpdateConsultantProfileCommand request, CancellationToken cancellationToken)
            {
                var consultantProfile = await _context.ConsultantProfiles.FindAsync(request.ConsultantProfile.Id);
                if (consultantProfile == null)
                {
                    throw new NotFoundException(nameof(ConsultantProfile), request.ConsultantProfile.Id);
                }

                consultantProfile = mapper.Map(request.ConsultantProfile, consultantProfile);

                if (consultantProfile.AvailableFromDate != null)
                {
                    consultantProfile.AvailableFromDate = consultantProfile.AvailableFromDate?.Date;
                }

                _context.Update(consultantProfile);

                await _context.SaveChangesAsync();

                return mapper.Map<ConsultantProfileDto>(consultantProfile);
            }
        }
    }
}
