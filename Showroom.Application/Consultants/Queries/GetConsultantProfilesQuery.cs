using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Showroom.Application.Dtos;

namespace Showroom.Application.Consultants.Queries
{
    public class GetConsultantProfilesQuery : IRequest<IEnumerable<ConsultantProfileDto>>
    {
        public GetConsultantProfilesQuery()
        {
        }

        class GetConsultantProfilesQueryHandler : IRequestHandler<GetConsultantProfilesQuery, IEnumerable<ConsultantProfileDto>>
        {
            public Task<IEnumerable<ConsultantProfileDto>> Handle(GetConsultantProfilesQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
