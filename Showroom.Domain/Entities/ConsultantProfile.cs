using System;
using System.ComponentModel.DataAnnotations;

namespace Showroom.Domain.Entities
{
    public class ConsultantProfile : UserProfile
    {
        public CompetenceArea CompetenceArea { get; set; }

        [Required]
        public string CompetenceAreaId { get; set; }

        public string Headline { get; set; }

        public string ShortPresentation { get; set; }

        public string Presentation { get; set; }

        public ManagerProfile Manager { get; set; }

        public Guid ManagerId { get; set; }

        public DateTime? AvailableFromDate { get; set; }
    }
}
