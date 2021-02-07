using System;
using System.ComponentModel.DataAnnotations;

namespace Showroom.Server.Models
{
    public class ManagerCompetenceArea
    {
        [Key]
        public string Id { get; set; }

        public ManagerProfile Manager { get; set; }

        [Required]
        public Guid ManagerId { get; set; }

        public CompetenceArea CompetenceArea { get; set; }

        [Required]
        public string CompetenceAreaId { get; set; }
    }
}
