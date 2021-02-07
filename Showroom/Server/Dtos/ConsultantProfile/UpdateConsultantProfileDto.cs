using System;

namespace Showroom.Server.Dtos
{

    public class UpdateConsultantProfileDto : ConsultantProfileViewModelDto
    {
        public Guid Id { get; set; }
    }
}
