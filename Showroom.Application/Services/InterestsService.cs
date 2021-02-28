using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Showroom.Application.Common.Interfaces;
using Showroom.Application.Dtos;
using Showroom.Domain.Entities;

namespace Showroom.Application.Services
{
    public class InterestsService
    {
        private readonly IApplicationDbContext _context;
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;

        public InterestsService(IApplicationDbContext context, IIdentityService identityService, IMapper mapper)
        {
            _context = context;
            this.identityService = identityService;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ClientConsultantInterestDto>> GetInterestsAsync()
        {
            var user = await identityService.GetUserAsync();

            await _context.Entry(user).Reference(c => c.Profile).LoadAsync();

            if (user.Profile is ManagerProfile)
            {
                return mapper.ProjectTo<ClientConsultantInterestDto>(
                    _context.ClientConsultantInterests
                    .Include(c => c.Consultant.Manager)
                    .Where(c => c.Consultant.ManagerId == user.Profile.Id));
            }
            else if (user.Profile is ClientProfile)
            {
                return mapper.ProjectTo<ClientConsultantInterestDto>(
                    _context.ClientConsultantInterests
                    .Where(c => c.ClientId == user.Profile.Id)
                    .AsQueryable());
            }

            return mapper.ProjectTo<ClientConsultantInterestDto>(
                     _context.ClientConsultantInterests
                     .AsQueryable());
        }

        public async Task<bool> HasShownInterestAsync(Guid consultantId)
        {
            var user = await identityService.GetUserAsync();

            await _context.Entry(user).Reference(c => c.Profile).LoadAsync();

            if (user.Profile is ClientProfile)
            {
                return await _context.ClientConsultantInterests
                    .AnyAsync(c => c.ClientId == user.Profile.Id && c.ConsultantId == consultantId);
            }

            throw new Exception();
        }

        public async Task ShowInterestAsync(ShowInterestCommand showInterest)
        {
            var user = await identityService.GetUserAsync();

            await _context.Entry(user).Reference(c => c.Profile).LoadAsync();

            if (user.Profile is ClientProfile)
            {
                var existingInterest = await _context.ClientConsultantInterests
                    .AnyAsync(c => c.ClientId == user.Profile.Id && c.ConsultantId == showInterest.ConsultantId);

                if (existingInterest)
                {
                    throw new Exception();
                }

                var clientConsultantInterest = mapper.Map<ClientConsultantInterest>(showInterest);
                clientConsultantInterest.Date = DateTime.Now;

                clientConsultantInterest.ClientId = user.ProfileId.GetValueOrDefault();

                _context.ClientConsultantInterests.Add(clientConsultantInterest);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RevokeInterestAsync(Guid consultantId)
        {
            var user = await identityService.GetUserAsync();

            await _context.Entry(user).Reference(c => c.Profile).LoadAsync();

            var clientConsultantInterest = await _context.ClientConsultantInterests
                    .FirstOrDefaultAsync(c => c.ClientId == user.Profile.Id && c.ConsultantId == consultantId);

            _context.ClientConsultantInterests.Remove(clientConsultantInterest);
            await _context.SaveChangesAsync();
        }
    }
}
