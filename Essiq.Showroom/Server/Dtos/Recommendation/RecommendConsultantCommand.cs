using System;
using System.ComponentModel.DataAnnotations;

namespace Essiq.Showroom.Server.Dtos
{

    public class RecommendConsultantCommand
    {

        [Required]
        public Guid ConsultantId { get; set; }

        [Required]
        public Guid ClientId { get; set; }

        public string Message { get; set; }
    }
}
