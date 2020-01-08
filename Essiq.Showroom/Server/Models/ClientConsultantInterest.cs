using System;
using System.ComponentModel.DataAnnotations;

namespace Essiq.Showroom.Server.Models
{
    public class ClientConsultantInterest
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public ClientProfile Client { get; set; }

        public Guid ClientId { get; set; }

        [Required]
        public ConsultantProfile Consultant { get; set; }

        public Guid ConsultantId { get; set; }

        public string Message { get; set; }

        public DateTime Date { get; set; }
    }
}
