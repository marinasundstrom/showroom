using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Showroom.Application.Common.Interfaces;
using Showroom.Application.Common.Dtos;
using Showroom.Application.Services;
using Showroom.Domain.Entities;
using Showroom.Domain.Exceptions;

namespace Showroom.Application.Consultants.Queries
{
    public class GetRecommendationsQuery : IRequest<IEnumerable<ClientConsultantRecommendationDto>>
    {
        class GetRecommendationsQueryHandler : IRequestHandler<GetRecommendationsQuery, IEnumerable<ClientConsultantRecommendationDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper mapper;
            private readonly IIdentityService identityService;

            public GetRecommendationsQueryHandler(
                IApplicationDbContext context,
                IMapper mapper,
                IIdentityService identityService)
            {
                _context = context;
                this.mapper = mapper;
                this.identityService = identityService;
            }

            public async Task<IEnumerable<ClientConsultantRecommendationDto>> Handle(GetRecommendationsQuery request, CancellationToken cancellationToken)
            {
                var user = await identityService.GetUserAsync();

                await _context.Entry(user).Reference(e => e.Profile).LoadAsync();

                var result = _context.ConsultantRecommendations
                         .Include(e => e.Manager)
                         .Include(e => e.Client)
                         .Include(e => e.Consultant)
                         .Include(x => x.Consultant.Organization)
                         .Include(x => x.Consultant.CompetenceArea)
                         .AsQueryable();

                if (user.Profile is ClientProfile)
                {
                    result = result.Where(e => e.ClientId == user.Profile.Id);
                }
                else if (user.Profile is ManagerProfile)
                {
                    result = result.Where(e => e.ManagerId == user.Profile.Id);
                }

                return mapper.ProjectTo<ClientConsultantRecommendationDto>(
                    (await result.ToListAsync()).AsQueryable());
            }
        }
    }
}
