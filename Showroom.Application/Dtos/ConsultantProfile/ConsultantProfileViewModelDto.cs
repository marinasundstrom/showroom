using System;
using System.Runtime.Serialization;

namespace Showroom.Application.Dtos
{
    [KnownType(typeof(AddConsultantProfileDto))]
    [KnownType(typeof(UpdateConsultantProfileDto))]
    public abstract class ConsultantProfileViewModelDto : UserProfileViewModelDto
    {
        public string CompetenceAreaId { get; set; }

        public Guid ManagerId { get; set; }

        public string Headline { get; set; }

        public string ShortPresentation { get; set; }

        public string Presentation { get; set; }

        public DateTime? AvailableFromDate { get; set; }
    }
}
