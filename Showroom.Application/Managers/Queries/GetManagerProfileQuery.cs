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

namespace Showroom.Application.Managers.Queries
{
    public class GetManagerProfileQuery : IRequest<ManagerProfileDto>
    {
        public GetManagerProfileQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }

        class GetManagerProfileQueryHandler : IRequestHandler<GetManagerProfileQuery, ManagerProfileDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper mapper;
            private readonly IIdentityService identityService;

            public GetManagerProfileQueryHandler(
                IApplicationDbContext context,
                IMapper mapper,
                IIdentityService identityService)
            {
                _context = context;
                this.mapper = mapper;
                this.identityService = identityService;
            }

            public async Task<ManagerProfileDto> Handle(GetManagerProfileQuery request, CancellationToken cancellationToken)
            {
                var managerProfile = await _context
                                .ManagersProfiles
                                .Include(x => x.Organization)
                                .Include(x => x.ManagerCompetenceAreas)
                                .FirstOrDefaultAsync(m => m.Id == request.Id);

                if (managerProfile == null)
                {
                    throw new NotFoundException(nameof(ManagerProfile), request.Id);
                }

                return mapper.Map<ManagerProfileDto>(managerProfile);
            }
        }
    }
}
