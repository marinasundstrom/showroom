using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Showroom.Server.Models
{
    public class ClientProfile : UserProfile
    {
        public ManagerProfile Reference { get; set; }

        [Required]
        [ForeignKey(nameof(Reference))]
        public Guid ReferenceId { get; set; }

        public ICollection<ConsultantRecommendation> Recommendations { get; set; }
        public ICollection<ClientConsultantInterest> Interests { get; set; }
    }
}
