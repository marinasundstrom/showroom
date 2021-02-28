using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Showroom.Infrastructure.Persistence;
using Showroom.Application.Dtos;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Showroom.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultantGalleryController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ConsultantGalleryController(
            ApplicationDbContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ProfileShortPagedResult> GetConsultants(int pageNumber, int numberOfItemsPerPage = 10,
            string organizationId = null, string competenceAreaId = null, DateTime? availableFrom = null)
        {
            var queryResultPage = context.ConsultantProfiles.AsQueryable();

            if (organizationId != null)
            {
                queryResultPage = queryResultPage.Where(x => x.OrganizationId == organizationId);
            }

            if (competenceAreaId != null)
            {
                queryResultPage = queryResultPage.Where(x => x.CompetenceAreaId == competenceAreaId);
            }

            if (availableFrom != null)
            {
                availableFrom = availableFrom?.Date;
                queryResultPage = queryResultPage.Where(e => e.AvailableFromDate == null || availableFrom >= e.AvailableFromDate);
            }

            var totalCount = await queryResultPage.CountAsync();

            queryResultPage = queryResultPage
                .Skip(numberOfItemsPerPage * pageNumber)
                .Take(numberOfItemsPerPage);

            return new ProfileShortPagedResult()
            {
                Items = mapper.ProjectTo<ProfileShortDto>(queryResultPage.AsQueryable()),
                PageNumber = pageNumber,
                PageSize = await queryResultPage.CountAsync(),
                TotalItems = totalCount
            };

            // totalPage = (int) Math.Ceiling((double) imagesFound.Length / PageSize);
        }
    }

    public class ProfileShortPagedResult : IPagedResult<ProfileShortDto>
    {
        public IEnumerable<ProfileShortDto> Items { get; set; }

        public long TotalItems { get; set; }

        public long PageNumber { get; set; }

        public long PageSize { get; set; }
    }
}
