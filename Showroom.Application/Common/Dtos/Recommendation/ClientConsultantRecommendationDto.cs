using System;
using Showroom.Application.Consultants;

namespace Showroom.Application.Common.Dtos
{
    public class ClientConsultantRecommendationDto
    {
        public Guid Id { get; set; }

        public ConsultantProfileDto Consultant { get; set; }

        public ProfileShortDto Client { get; set; }

        public ProfileShortDto Manager { get; set; }

        public string Message { get; set; }

        public DateTime Date { get; set; }
    }
}
