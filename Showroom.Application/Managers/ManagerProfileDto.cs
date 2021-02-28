using System.Collections.Generic;
using Showroom.Application.Common.Dtos;

namespace Showroom.Application.Managers
{
    public class ManagerProfileDto : UserProfileDto
    {
        public string Headline { get; set; }

        public string ShortPresentation { get; set; }

        public string Presentation { get; set; }

        public IEnumerable<CompetenceAreaDto> CompetenceAreas { get; set; }
    }
}
