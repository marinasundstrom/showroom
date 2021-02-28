using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Showroom.Application.Common.Interfaces;
using Showroom.Application.Dtos;
using Showroom.Application.Services;
using Showroom.Domain.Entities;
using Showroom.Domain.Exceptions;

namespace Showroom.Application.Consultants.Queries
{
    public class GetConsultantProfilesQuery : IRequest<IEnumerable<ConsultantProfileDto>>
    {
        public GetConsultantProfilesQuery(string organizationId = null, string competenceAreaId = null, DateTime? availableFrom = null, bool justMyOrganization = false, int pageNumber = 0, int pageSize = 10)
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

        class GetConsultantProfilesQueryHandler : IRequestHandler<GetConsultantProfilesQuery, IEnumerable<ConsultantProfileDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper mapper;
            private readonly IIdentityService identityService;

            public GetConsultantProfilesQueryHandler(
                IApplicationDbContext context,
                IMapper mapper,
                IIdentityService identityService)
            {
                _context = context;
                this.mapper = mapper;
                this.identityService = identityService;
            }

            public async Task<IEnumerable<ConsultantProfileDto>> Handle(GetConsultantProfilesQuery request, CancellationToken cancellationToken)
            {
                var user = await identityService.GetUserAsync();

                await _context.Entry(user).Reference(e => e.Profile).LoadAsync();
                if (user.Profile.OrganizationId != null)
                {
                    await _context.Entry(user.Profile).Reference(e => e.Organization).LoadAsync();
                }

                IQueryable<ConsultantProfile> result = _context
                        .ConsultantProfiles
                        .Include(x => x.Organization)
                        .Include(c => c.CompetenceArea)
                        .Include(c => c.Manager);

                if (request.JustMyOrganization)
                {
                    if (user.Profile.OrganizationId != null)
                    {
                        result = result.Where(e => e.OrganizationId == user.Profile.OrganizationId || e.OrganizationId == null);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(request.OrganizationId))
                    {
                        var organization = await _context.Organizations.FindAsync(request.OrganizationId);
                        if (organization == null)
                        {
                            throw new NotFoundException(nameof(Organization), request.OrganizationId);
                        }

                        result = result.Where(e => e.OrganizationId == request.OrganizationId);
                    }
                }

                if (request.CompetenceAreaId != null)
                {
                    result = result.Where(e => e.CompetenceAreaId == request.CompetenceAreaId);
                }

                if (request.AvailableFrom != null)
                {
                    request.AvailableFrom = request.AvailableFrom?.Date;
                    result = result.Where(e => e.AvailableFromDate == null || request.AvailableFrom >= e.AvailableFromDate);
                }

                result = result
                    .Skip((request.PageNumber) * request.PageSize)
                    .Take(request.PageSize);

                return mapper.ProjectTo<ConsultantProfileDto>(result);
            }
        }
    }
}
