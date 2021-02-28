using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Showroom.Application.Common.Interfaces;
using Showroom.Application.Dtos;
using Showroom.Domain.Entities;

namespace Showroom.Application.Services
{
    public class RecommendationService
    {
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;

        public RecommendationService(IApplicationDbContext applicationDbContext, IIdentityService identityService, IMapper mapper)
        {
            this.applicationDbContext = applicationDbContext;
            this.identityService = identityService;
            this.mapper = mapper;
        }

        public async Task<IQueryable<ClientConsultantRecommendationDto>> GetRecommendationsAsync()
        {
            var user = await identityService.GetUserAsync();

            await applicationDbContext.Entry(user).Reference(e => e.Profile).LoadAsync();

            var result = applicationDbContext.ConsultantRecommendations
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

        public async Task<ClientConsultantRecommendationDto> RecommendConsultantAsync(RecommendConsultantCommand dto)
        {
            var existingRecommendation = await applicationDbContext.ConsultantRecommendations
               .Include(x => x.Consultant)
               .Include(x => x.Client)
               .FirstOrDefaultAsync(x => x.ClientId == dto.ClientId
               && x.ConsultantId == dto.ConsultantId);

            if (existingRecommendation != null)
            {
                throw new InvalidOperationException($"{existingRecommendation.Consultant.Id} has already been presented to {existingRecommendation.Client.Id}");
            }

            var user = await identityService.GetUserAsync();

            await applicationDbContext.Entry(user).Reference(e => e.Profile).LoadAsync();
            await applicationDbContext.Entry(user.Profile).Reference(e => e.Organization).LoadAsync();

            var consultantRecommendation = mapper.Map<ConsultantRecommendation>(dto);

            consultantRecommendation.Manager = user.Profile as ManagerProfile;
            consultantRecommendation.Date = DateTime.Now;
            applicationDbContext.ConsultantRecommendations.Add(consultantRecommendation);

            await applicationDbContext.SaveChangesAsync();

            return mapper.Map<ClientConsultantRecommendationDto>(consultantRecommendation);
        }
    }
}
