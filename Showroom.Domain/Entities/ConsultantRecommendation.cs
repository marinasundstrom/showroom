using System;
using System.ComponentModel.DataAnnotations;

namespace Showroom.Domain.Entities
{
    public class ConsultantRecommendation
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public ManagerProfile Manager { get; set; }

        public Guid ManagerId { get; set; }

        [Required]
        public ClientProfile Client { get; set; }

        public Guid ClientId { get; set; }

        [Required]
        public ConsultantProfile Consultant { get; set; }

        public Guid ConsultantId { get; set; }

        public Guid? ClientCaseId { get; set; }

        public ClientCase ClientCase { get; set; }

        public string Message { get; set; }

        public DateTime Date { get; set; }

        public DateTime? DateViewedByClient { get; set; }
    }
}
