using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Showroom.Application.Common.Dtos;
using Showroom.Application.Common.Interfaces;
using Showroom.Application.Services;
using Showroom.Domain.Entities;
using Showroom.Domain.Exceptions;

namespace Showroom.Application.Consultants.Queries
{
    public class GetConsultantProfileQuery : IRequest<ConsultantProfileDto>
    {
        public GetConsultantProfileQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }

        class GetConsultantProfileQueryHandler : IRequestHandler<GetConsultantProfileQuery, ConsultantProfileDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper mapper;
            private readonly IIdentityService identityService;

            public GetConsultantProfileQueryHandler(
                IApplicationDbContext context,
                IMapper mapper,
                IIdentityService identityService)
            {
                _context = context;
                this.mapper = mapper;
                this.identityService = identityService;
            }

            public async Task<ConsultantProfileDto> Handle(GetConsultantProfileQuery request, CancellationToken cancellationToken)
            {
                var consultantProfile = await _context
                   .ConsultantProfiles
                   .Include(x => x.Organization)
                   .Include(c => c.CompetenceArea)
                   .Include(c => c.Manager)
                   .FirstAsync(c => c.Id == request.Id);

                if (consultantProfile == null)
                {
                    throw new NotFoundException(nameof(ConsultantProfile), request.Id);
                }

                return mapper.Map<ConsultantProfileDto>(consultantProfile);
            }
        }
    }
}
