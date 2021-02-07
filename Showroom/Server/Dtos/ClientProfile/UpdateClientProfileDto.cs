using System;

namespace Showroom.Server.Dtos
{

    public class UpdateClientProfileDto : ClientProfileViewModelDto
    {
        public Guid Id { get; set; }
    }
}
