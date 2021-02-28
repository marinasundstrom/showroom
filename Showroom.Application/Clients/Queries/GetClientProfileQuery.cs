using System;
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

namespace Showroom.Application.Clients.Queries
{
    public class GetClientProfileQuery : IRequest<ClientProfileDto>
    {
        public GetClientProfileQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }

        class GetClientProfileQueryHandler : IRequestHandler<GetClientProfileQuery, ClientProfileDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper mapper;
            private readonly IIdentityService identityService;

            public GetClientProfileQueryHandler(
                IApplicationDbContext context,
                IMapper mapper,
                IIdentityService identityService)
            {
                _context = context;
                this.mapper = mapper;
                this.identityService = identityService;
            }

            public async Task<ClientProfileDto> Handle(GetClientProfileQuery request, CancellationToken cancellationToken)
            {
                var clientProfile = await _context
                    .ClientProfiles
                    .Include(c => c.Reference)
                    .Include(x => x.Organization)
                    .FirstAsync(c => c.Id == request.Id);

                if (clientProfile == null)
                {
                    throw new NotFoundException(nameof(ClientProfile), request.Id);
                }

                return mapper.Map<ClientProfileDto>(clientProfile);
            }
        }
    }
}
