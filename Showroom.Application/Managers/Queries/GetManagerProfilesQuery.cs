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
    public class GetManagerProfilesQuery : IRequest<IEnumerable<ManagerProfileDto>>
    {
        public GetManagerProfilesQuery(string organizationId = null, string competenceAreaId = null, DateTime? availableFrom = null, bool justMyOrganization = false, int pageNumber = 0, int pageSize = 10)
        {
            OrganizationId = organizationId;
            CompetenceAreaId = competenceAreaId;
            AvailableFrom = availableFrom;
            JustMyOrganization = justMyOrganization;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public string OrganizationId { get; }
        public string CompetenceAreaId { get; }
        public DateTime? AvailableFrom { get; private set; }
        public bool JustMyOrganization { get; }
        public int PageNumber { get; }
        public int PageSize { get; }

        class GetManagerProfilesQueryHandler : IRequestHandler<GetManagerProfilesQuery, IEnumerable<ManagerProfileDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper mapper;
            private readonly IIdentityService identityService;

            public GetManagerProfilesQueryHandler(
                IApplicationDbContext context,
                IMapper mapper,
                IIdentityService identityService)
            {
                _context = context;
                this.mapper = mapper;
                this.identityService = identityService;
            }

            public async Task<IEnumerable<ManagerProfileDto>> Handle(GetManagerProfilesQuery request, CancellationToken cancellationToken)
            {
                var user = await identityService.GetUserAsync();

                await _context.Entry(user).Reference(e => e.Profile).LoadAsync();

                var result = _context
                    .ManagersProfiles
                    .Include(x => x.Organization)
                    .Include(x => x.ManagerCompetenceAreas)
                    .AsQueryable();

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

                return mapper.ProjectTo<ManagerProfileDto>(result);
            }
        }
    }
}
