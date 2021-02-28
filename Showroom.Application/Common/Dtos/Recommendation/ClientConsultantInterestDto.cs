using System;
using System.ComponentModel.DataAnnotations;

namespace Showroom.Application.Common.Dtos
{
    public class ClientConsultantInterestDto
    {
        [Required]
        public ProfileShortDto Consultant { get; set; }

        [Required]
        public ProfileShortDto Client { get; set; }

        public string Message { get; set; }

        public DateTime Date { get; set; }
    }
}
