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

namespace Showroom.Application.Consultants.Commands
{
    public class CreateConsultantProfileCommand : IRequest<ConsultantProfileDto>
    {
        public CreateConsultantProfileCommand(AddConsultantProfileDto consultantProfile)
        {
            ConsultantProfile = consultantProfile;
        }

        public AddConsultantProfileDto ConsultantProfile { get; }

        class CreateConsultantProfileCommandHandler : IRequestHandler<CreateConsultantProfileCommand, ConsultantProfileDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper mapper;
            private readonly IIdentityService identityService;

            public CreateConsultantProfileCommandHandler(
                IApplicationDbContext context,
                IMapper mapper,
                IIdentityService identityService)
            {
                _context = context;
                this.mapper = mapper;
                this.identityService = identityService;
            }

            public async Task<ConsultantProfileDto> Handle(CreateConsultantProfileCommand request, CancellationToken cancellationToken)
            {
                var consultantProfile = mapper.Map<ConsultantProfile>(request.ConsultantProfile);

                if (consultantProfile.AvailableFromDate != null)
                {
                    consultantProfile.AvailableFromDate = consultantProfile.AvailableFromDate?.Date;
                }

                var user = await identityService.GetUserAsync();

                await _context.Entry(user).Reference(e => e.Profile).LoadAsync();

                if (request.ConsultantProfile.OrganizationId == null)
                {
                    consultantProfile.OrganizationId = user.Profile.OrganizationId;
                }

                _context.ConsultantProfiles.Add(consultantProfile);
                await _context.SaveChangesAsync();

                return mapper.Map<ConsultantProfileDto>(consultantProfile);
            }
        }
    }
}
