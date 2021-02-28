using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Showroom.Application.Common.Interfaces;
using Showroom.Application.Common.Dtos;
using Showroom.Application.Services;
using Showroom.Domain.Entities;
using Showroom.Domain.Exceptions;

namespace Showroom.Application.Managers.Commands
{
    public class DeleteManagerProfileCommand : IRequest
    {
        public DeleteManagerProfileCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }

        class DeleteManagerProfileCommandHandler : IRequestHandler<DeleteManagerProfileCommand>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper mapper;
            private readonly IIdentityService identityService;

            public DeleteManagerProfileCommandHandler(
                IApplicationDbContext context,
                IMapper mapper,
                IIdentityService identityService)
            {
                _context = context;
                this.mapper = mapper;
                this.identityService = identityService;
            }

            public async Task<Unit> Handle(DeleteManagerProfileCommand request, CancellationToken cancellationToken)
            {
                var managerProfile = await _context.ConsultantProfiles.FindAsync(request.Id);
                if (managerProfile == null)
                {
                    throw new NotFoundException(nameof(ManagerProfile), request.Id);
                }

                _context.ConsultantProfiles.Remove(managerProfile);
                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
