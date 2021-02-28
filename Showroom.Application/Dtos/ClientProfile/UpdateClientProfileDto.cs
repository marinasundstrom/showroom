using System;

namespace Showroom.Application.Dtos
{

    public class UpdateClientProfileDto : ClientProfileViewModelDto
    {
        public Guid Id { get; set; }
    }
}
