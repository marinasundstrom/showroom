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

namespace Showroom.Application.Consultants.Commands
{
    public class DeleteConsultantProfileCommand : IRequest
    {
        public DeleteConsultantProfileCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }

        class DeleteConsultantProfileCommandHandler : IRequestHandler<DeleteConsultantProfileCommand>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper mapper;
            private readonly IIdentityService identityService;

            public DeleteConsultantProfileCommandHandler(
                IApplicationDbContext context,
                IMapper mapper,
                IIdentityService identityService)
            {
                _context = context;
                this.mapper = mapper;
                this.identityService = identityService;
            }

            public async Task<Unit> Handle(DeleteConsultantProfileCommand request, CancellationToken cancellationToken)
            {
                var consultantProfile = await _context.ConsultantProfiles.FindAsync(request.Id);
                if (consultantProfile == null)
                {
                    throw new NotFoundException(nameof(ConsultantProfile), request.Id);
                }

                _context.ConsultantProfiles.Remove(consultantProfile);
                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
