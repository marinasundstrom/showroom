using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Showroom.Infrastructure.Persistence;
using Showroom.Application.Dtos;
using Showroom.Domain.Entities;
using Showroom.Shared;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Showroom.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompetenceAreasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper mapper;

        public CompetenceAreasController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/CompetenceAreas
        [HttpGet]
        public ActionResult<IEnumerable<CompetenceAreaDto>> GetCompetenceAreas()
        {
            return Ok(mapper.ProjectTo<CompetenceAreaDto>(_context.CompetenceAreas).AsEnumerable());
        }

        // GET: api/CompetenceAreas/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<CompetenceAreaDto>> GetCompetenceArea(string id)
        {
            var competenceArea = await _context.CompetenceAreas.FindAsync(id);

            if (competenceArea == null)
            {
                return NotFound();
            }

            return mapper.Map<CompetenceAreaDto>(competenceArea);
        }

        // PUT: api/CompetenceAreas/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Authorize(Roles = RoleConstants.Administrator)]
        public async Task<IActionResult> UpdateCompetenceArea(string id, CompetenceAreaDto vm)
        {
            if (id != vm.Id)
            {
                return BadRequest();
            }

            var competenceArea = await _context.CompetenceAreas.FindAsync(id);

            if (competenceArea == null)
            {
                return NotFound();
            }

            competenceArea = mapper.Map(vm, competenceArea);

            _context.Update(competenceArea);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompetenceAreaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CompetenceAreas
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        [Authorize(Roles = RoleConstants.Administrator)]
        public async Task<ActionResult<CompetenceAreaDto>> CreateCompetenceArea(CompetenceAreaDto vm)
        {
            var competenceArea = mapper.Map<CompetenceArea>(vm);

            competenceArea.Id = Guid.NewGuid().ToString();
            _context.CompetenceAreas.Add(competenceArea);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompetenceArea", new { id = competenceArea.Id }, mapper.Map<CompetenceAreaDto>(competenceArea));
        }

        // DELETE: api/CompetenceAreas/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Authorize(Roles = RoleConstants.Administrator)]
        public async Task<ActionResult<CompetenceAreaDto>> DeleteCompetenceArea(string id)
        {
            var competenceArea = await _context.CompetenceAreas.FindAsync(id);
            if (competenceArea == null)
            {
                return NotFound();
            }

            _context.CompetenceAreas.Remove(competenceArea);
            await _context.SaveChangesAsync();

            return mapper.Map<CompetenceAreaDto>(competenceArea);
        }

        private bool CompetenceAreaExists(string id)
        {
            return _context.CompetenceAreas.Any(e => e.Id == id);
        }
    }
}
