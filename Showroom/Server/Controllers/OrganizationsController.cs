using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Showroom.Application.Dtos;
using Showroom.Domain.Entities;
using Showroom.Infrastructure.Persistence;
using Showroom.Shared;

namespace Showroom.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrganizationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper mapper;

        public OrganizationsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Organizations
        [HttpGet]
        public ActionResult<IEnumerable<OrganizationDto>> GetOrganizations()
        {
            return Ok(mapper.ProjectTo<OrganizationDto>(_context.Organizations).AsEnumerable());
        }

        // GET: api/Organizations/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<OrganizationDto>> GetOrganization(string id)
        {
            var organization = await _context.Organizations.FindAsync(id);

            if (organization == null)
            {
                return NotFound();
            }

            return mapper.Map<OrganizationDto>(organization);
        }

        // PUT: api/Organizations/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = RoleConstants.Administrator)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateOrganization(string id, OrganizationDto vm)
        {
            if (id != vm.Id)
            {
                return BadRequest();
            }

            var organization = await _context.Organizations.FindAsync(id);

            if (organization == null)
            {
                return NotFound();
            }

            organization = mapper.Map(vm, organization);

            _context.Update(organization);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationExists(id))
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

        // POST: api/Organizations
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Authorize(Roles = RoleConstants.Administrator)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<OrganizationDto>> CreateOrganization(OrganizationDto vm)
        {
            var organization = mapper.Map<Organization>(vm);

            organization.Id = Guid.NewGuid().ToString();
            _context.Organizations.Add(organization);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrganizationExists(organization.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrganization", new { id = organization.Id }, mapper.Map<OrganizationDto>(organization));
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = RoleConstants.Administrator)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<OrganizationDto>> DeleteOrganization(string id)
        {
            var organization = await _context.Organizations.FindAsync(id);
            if (organization == null)
            {
                return NotFound();
            }

            _context.Organizations.Remove(organization);
            await _context.SaveChangesAsync();

            return mapper.Map<OrganizationDto>(organization);
        }

        private bool OrganizationExists(string id)
        {
            return _context.Organizations.Any(e => e.Id == id);
        }
    }
}
