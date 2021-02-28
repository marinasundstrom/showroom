using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using Showroom.Infrastructure.Persistence;
using Showroom.Application.Dtos;
using Showroom.Application.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Showroom.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientConsultantInterestsController : ControllerBase
    {
        private readonly InterestsService interestsService;
        private readonly ApplicationDbContext _context;
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;

        public ClientConsultantInterestsController(
            InterestsService interestsService,
            ApplicationDbContext context,
            IIdentityService identityService,
            IMapper mapper)
        {
            this.interestsService = interestsService;
            _context = context;
            this.identityService = identityService;
            this.mapper = mapper;
        }

        // GET: api/ClientConsultantInterests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientConsultantInterestDto>>> GetInterests()
        {
            return Ok(
                await interestsService.GetInterestsAsync());
        }

        [HttpGet("HasInterest")]
        public async Task<bool> HasShownInterest(Guid consultantId)
        {
            return await interestsService.HasShownInterestAsync(consultantId);
        }

        [HttpPost("ShowInterest")]
        public async Task ShowInterest(ShowInterestCommand vm)
        {
            await interestsService.ShowInterestAsync(vm);

        }

        [HttpPost("RevokeInterest")]
        public async Task RevokeInterest(Guid consultantId)
        {
            await interestsService.RevokeInterestAsync(consultantId);
        }
    }
}
