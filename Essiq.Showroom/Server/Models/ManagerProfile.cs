using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Essiq.Showroom.Server.Models
{
    public class ManagerProfile : UserProfile
    {
        public string Headline { get; set; }

        public string ShortPresentation { get; set; }

        public string Presentation { get; set; }

        public ICollection<ManagerCompetenceArea> ManagerCompetenceAreas { get; set; }

        [NotMapped]
        public IEnumerable<CompetenceArea> CompetenceAreas2 => ManagerCompetenceAreas?.Select(x => x.CompetenceArea);
    }
}
