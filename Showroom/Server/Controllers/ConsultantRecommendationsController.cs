using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Showroom.Application.Common.Dtos;
using Showroom.Application.Consultants.Queries;
using Showroom.Application.Services;
using Showroom.Infrastructure.Persistence;
using Showroom.Shared;

namespace Showroom.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = RoleConstants.AdministratorAndManager)]
    public class ConsultantRecommendationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public ConsultantRecommendationsController(IMediator mediator, ApplicationDbContext context, IIdentityService identityService, IMapper mapper)
        {
            _context = context;
            this.identityService = identityService;
            this.mapper = mapper;
            this.mediator = mediator;
        }

        // GET: api/ConsultantRecommendations
        [HttpGet]
        [ProducesDefaultResponseType]
        [Authorize(Roles = RoleConstants.Client)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IQueryable<ClientConsultantRecommendationDto>>> GetConsultantRecommendations()
        {
            return Ok(await mediator.Send(new GetRecommendationsQuery()));
        }

        // GET: api/ConsultantRecommendations/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ClientConsultantRecommendationDto>> GetConsultantRecommendation(string id)
        {
            var consultantRecommendation = await _context.ConsultantRecommendations.FindAsync(id);

            if (consultantRecommendation == null)
            {
                return NotFound();
            }

            return mapper.Map<ClientConsultantRecommendationDto>(consultantRecommendation);
        }

        //// PUT: api/ConsultantRecommendations/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //// more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPut("{id}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesDefaultResponseType]
        //public async Task<IActionResult> PutConsultantRecommendation(Guid id, ConsultantRecommendation consultantRecommendation)
        //{
        //    if (id != consultantRecommendation.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(consultantRecommendation).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ConsultantRecommendationExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/ConsultantRecommendations
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ClientConsultantRecommendationDto>> PostConsultantRecommendation(GetRecommendationQuery dto)
        {
            var consultantRecommendation = await mediator.Send(dto);

            return CreatedAtAction("GetConsultantRecommendation", new { id = consultantRecommendation.Id },
                mapper.Map<ClientConsultantRecommendationDto>(consultantRecommendation));
        }

        // DELETE: api/ConsultantRecommendations/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ClientConsultantRecommendationDto>> DeleteConsultantRecommendation(Guid id)
        {
            var consultantRecommendation = await _context.ConsultantRecommendations.FindAsync(id);
            if (consultantRecommendation == null)
            {
                return NotFound();
            }

            _context.ConsultantRecommendations.Remove(consultantRecommendation);
            await _context.SaveChangesAsync();

            return mapper.Map<ClientConsultantRecommendationDto>(consultantRecommendation);
        }

        private bool ConsultantRecommendationExists(Guid id)
        {
            return _context.ConsultantRecommendations.Any(e => e.Id == id);
        }
    }
}
