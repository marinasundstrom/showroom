using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Showroom.Domain.Entities
{
    public class ClientCase
    {
        [Key]
        public Guid Id { get; set; }

        public ManagerProfile Manager { get; set; }

        [Required]
        public Guid ManagerId { get; set; }

        public ClientProfile ClientProfile { get; set; }

        [Required]
        public Guid ClientProfileId { get; set; }

        public ICollection<ConsultantRecommendation> ConsultantRecommendations { get; set; }

        public string Text { get; set; }

        public DateTime DateOpened { get; set; }

        public DateTime? DateClosed { get; set; }

        public ClientCaseStatus Status { get; set; }
    }
}
