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

namespace Showroom.Application.Clients.Queries
{
    public class GetClientProfilesQuery : IRequest<IEnumerable<ClientProfileDto>>
    {
        public GetClientProfilesQuery(string organizationId = null)
        {
            OrganizationId = organizationId;
        }

        public string OrganizationId { get; }

        class GetClientProfilesQueryHandler : IRequestHandler<GetClientProfilesQuery, IEnumerable<ClientProfileDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper mapper;
            private readonly IIdentityService identityService;

            public GetClientProfilesQueryHandler(
                IApplicationDbContext context,
                IMapper mapper,
                IIdentityService identityService)
            {
                _context = context;
                this.mapper = mapper;
                this.identityService = identityService;
            }

            public async Task<IEnumerable<ClientProfileDto>> Handle(GetClientProfilesQuery request, CancellationToken cancellationToken)
            {
                var user = await identityService.GetUserAsync();

                await _context.Entry(user).Reference(e => e.Profile).LoadAsync();
                if (user.Profile.OrganizationId != null)
                {
                    await _context.Entry(user.Profile).Reference(e => e.Organization).LoadAsync();
                }

                IQueryable<ClientProfile> result = _context
                        .ClientProfiles
                        .Include(x => x.Organization)
                        .Include(c => c.Reference);

                if (user.Profile.OrganizationId != null)
                {
                    result = result.Where(e => e.OrganizationId == user.Profile.OrganizationId || e.OrganizationId == null);
                }

                if (request.OrganizationId != null)
                {
                    var organization = await _context.Organizations.FindAsync(request.OrganizationId);
                    if (organization == null)
                    {
                        throw new NotFoundException(nameof(Organization), request.OrganizationId);
                    }

                    result = result.Where(e => e.OrganizationId == request.OrganizationId);
                }

                return mapper.ProjectTo<ClientProfileDto>(result);
            }
        }
    }
}
