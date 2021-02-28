using System;

namespace Showroom.Application.Dtos
{
    public class ConsultantProfileDto : UserProfileDto
    {
        public CompetenceAreaDto CompetenceArea { get; set; }

        public string Headline { get; set; }

        public string ShortPresentation { get; set; }

        public string Presentation { get; set; }

        public ProfileShortDto Manager { get; set; }

        public DateTime? AvailableFromDate { get; set; }
    }
}
