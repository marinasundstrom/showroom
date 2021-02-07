using System.Collections.Generic;

namespace Showroom.Server.Dtos
{
    public class ManagerProfileDto : UserProfileDto
    {
        public string Headline { get; set; }

        public string ShortPresentation { get; set; }

        public string Presentation { get; set; }

        public IEnumerable<CompetenceAreaDto> CompetenceAreas { get; set; }
    }
}
