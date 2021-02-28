using System.Collections.Generic;
using System.Runtime.Serialization;
using Showroom.Application.Common.Dtos;

namespace Showroom.Application.Managers
{
    [KnownType(typeof(AddManagerProfileDto))]
    [KnownType(typeof(UpdateManagerProfileDto))]
    public abstract class ManagerProfileViewModelDto : UserProfileViewModelDto
    {
        public string Headline { get; set; }

        public string ShortPresentation { get; set; }

        public string Presentation { get; set; }

        public IList<string> CompetenceAreas { get; set; }
    }
}
