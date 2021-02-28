using System;

namespace Showroom.Application.Common.Dtos
{

    public class UpdateClientProfileDto : ClientProfileViewModelDto
    {
        public Guid Id { get; set; }
    }
}
