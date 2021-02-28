using System;
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

namespace Showroom.Application.Consultants.Queries
{
    public class GetRecommendationQuery : IRequest<ClientConsultantRecommendationDto>
    {
        public GetRecommendationQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }

        class GetRecommendationQueryHandler : IRequestHandler<GetRecommendationQuery, ClientConsultantRecommendationDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper mapper;
            private readonly IIdentityService identityService;

            public GetRecommendationQueryHandler(
                IApplicationDbContext context,
                IMapper mapper,
                IIdentityService identityService)
            {
                _context = context;
                this.mapper = mapper;
                this.identityService = identityService;
            }

            public async Task<ClientConsultantRecommendationDto> Handle(GetRecommendationQuery request, CancellationToken cancellationToken)
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

                return mapper.Map<ClientConsultantRecommendationDto>(
                    await result.FirstOrDefaultAsync(x => x.Id == request.Id));
            }
        }
    }
}
