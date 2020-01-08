
using System;
using System.ComponentModel.DataAnnotations;

namespace Essiq.Showroom.Server.Dtos
{
    public class UpdateInterestCommand
    {
        [Required]
        public Guid ConsultantId { get; set; }

        public string Message { get; set; }
    }
}
