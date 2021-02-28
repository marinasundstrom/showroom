using System;

namespace Showroom.Application.Dtos
{

    public class UpdateConsultantProfileDto : ConsultantProfileViewModelDto
    {
        public Guid Id { get; set; }
    }
}
