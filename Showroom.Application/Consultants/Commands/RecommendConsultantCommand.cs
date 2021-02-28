using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Showroom.Application.Common.Dtos;
using Showroom.Application.Common.Interfaces;
using Showroom.Application.Services;
using Showroom.Domain.Entities;
using Showroom.Domain.Exceptions;

namespace Showroom.Application.Consultants.Commands
{
    public class RecommendConsultantCommand : IRequest<ClientConsultantRecommendationDto>
    {
        public RecommendConsultantCommand(Guid consultantId, Guid clientId, string message)
        {
            ConsultantId = consultantId;
            ClientId = clientId;
            Message = message;
        }

        [Required]
        public Guid ConsultantId { get; set; }

        [Required]
        public Guid ClientId { get; set; }

        public string Message { get; set; }

        class RecommendConsultantCommandHandler : IRequestHandler<RecommendConsultantCommand, ClientConsultantRecommendationDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper mapper;
            private readonly IIdentityService identityService;

            public RecommendConsultantCommandHandler(
                IApplicationDbContext context,
                IMapper mapper,
                IIdentityService identityService)
            {
                _context = context;
                this.mapper = mapper;
                this.identityService = identityService;
            }

            public async Task<ClientConsultantRecommendationDto> Handle(RecommendConsultantCommand request, CancellationToken cancellationToken)
            {
                var existingRecommendation = await _context.ConsultantRecommendations
                   .Include(x => x.Consultant)
                   .Include(x => x.Client)
                   .FirstOrDefaultAsync(x => x.ClientId == request.ClientId
                   && x.ConsultantId == request.ConsultantId);

                if (existingRecommendation != null)
                {
                    throw new InvalidOperationException($"{existingRecommendation.Consultant.Id} has already been presented to {existingRecommendation.Client.Id}");
                }

                var user = await identityService.GetUserAsync();

                await _context.Entry(user).Reference(e => e.Profile).LoadAsync();
                await _context.Entry(user.Profile).Reference(e => e.Organization).LoadAsync();

                var consultantRecommendation = mapper.Map<ConsultantRecommendation>(request);

                consultantRecommendation.Manager = user.Profile as ManagerProfile;
                consultantRecommendation.Date = DateTime.Now;
                _context.ConsultantRecommendations.Add(consultantRecommendation);

                await _context.SaveChangesAsync();

                return mapper.Map<ClientConsultantRecommendationDto>(consultantRecommendation);
            }
        }
    }
}
