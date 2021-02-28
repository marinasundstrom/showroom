using System;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Showroom.Application.Common.Interfaces;
using Showroom.Application.Dtos;
using Showroom.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Showroom.Domain.Exceptions;

namespace Showroom.Application.Services
{
    public class ConsultantManager
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper mapper;
        private readonly IIdentityService identityService;

        public ConsultantManager(
            IApplicationDbContext context,
            IMapper mapper,
            IEmailSender emailSender,
            IIdentityService identityService)
        {
            _context = context;
            this.mapper = mapper;
            this.identityService = identityService;
        }

        public async Task<IQueryable<ConsultantProfileDto>> GetConsultantProfilesAsync(string organizationId = null, string competenceAreaId = null, DateTime? availableFrom = null, bool justMyOrganization = false, int pageNumber = 0, int pageSize = 10)
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

            if (justMyOrganization)
            {
                if (user.Profile.OrganizationId != null)
                {
                    result = result.Where(e => e.OrganizationId == user.Profile.OrganizationId || e.OrganizationId == null);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(organizationId))
                {
                    var organization = await _context.Organizations.FindAsync(organizationId);
                    if (organization == null)
                    {
                        throw new NotFoundException(nameof(Organization), organizationId);
                    }

                    result = result.Where(e => e.OrganizationId == organizationId);
                }
            }

            if (competenceAreaId != null)
            {
                result = result.Where(e => e.CompetenceAreaId == competenceAreaId);
            }

            if (availableFrom != null)
            {
                availableFrom = availableFrom?.Date;
                result = result.Where(e => e.AvailableFromDate == null || availableFrom >= e.AvailableFromDate);
            }

            result = result
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return mapper.ProjectTo<ConsultantProfileDto>(result);
        }

        public async Task<ConsultantProfileDto> GetConsultantProfileAsync(Guid id)
        {
            var consultantProfile = await _context
                .ConsultantProfiles
                .Include(x => x.Organization)
                .Include(c => c.CompetenceArea)
                .Include(c => c.Manager)
                .FirstAsync(c => c.Id == id);

            if (consultantProfile == null)
            {
                throw new NotFoundException(nameof(ConsultantProfile), id);
            }

            return mapper.Map<ConsultantProfileDto>(consultantProfile);
        }

        public async Task<ConsultantProfileDto> CreateConsultantProfileAsync(AddConsultantProfileDto dto)
        {
            var consultantProfile = mapper.Map<ConsultantProfile>(dto);

            if (consultantProfile.AvailableFromDate != null)
            {
                consultantProfile.AvailableFromDate = consultantProfile.AvailableFromDate?.Date;
            }

            var user = await identityService.GetUserAsync();

            await _context.Entry(user).Reference(e => e.Profile).LoadAsync();

            if (dto.OrganizationId == null)
            {
                consultantProfile.OrganizationId = user.Profile.OrganizationId;
            }

            _context.ConsultantProfiles.Add(consultantProfile);
            await _context.SaveChangesAsync();

            return mapper.Map<ConsultantProfileDto>(consultantProfile);
        }

        public async Task<ConsultantProfileDto> UpdateConsultantProfileAsync(UpdateConsultantProfileDto dto)
        {
            var consultantProfile = await _context.ConsultantProfiles.FindAsync(dto.Id);
            if (consultantProfile == null)
            {
                throw new NotFoundException(nameof(ConsultantProfile), dto.Id);
            }

            consultantProfile = mapper.Map(dto, consultantProfile);

            if (consultantProfile.AvailableFromDate != null)
            {
                consultantProfile.AvailableFromDate = consultantProfile.AvailableFromDate?.Date;
            }

            _context.Update(consultantProfile);

            await _context.SaveChangesAsync();

            return mapper.Map<ConsultantProfileDto>(consultantProfile);
        }

        public async Task DeleteConsultantProfileAsync(Guid id)
        {
            var consultantProfile = await _context.ConsultantProfiles.FindAsync(id);
            if (consultantProfile == null)
            {
                throw new NotFoundException(nameof(ConsultantProfile), id);
            }

            _context.ConsultantProfiles.Remove(consultantProfile);
            await _context.SaveChangesAsync();
        }
    }
}
