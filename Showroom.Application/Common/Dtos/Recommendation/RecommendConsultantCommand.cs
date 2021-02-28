using System;
using System.ComponentModel.DataAnnotations;

namespace Showroom.Application.Common.Dtos
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
