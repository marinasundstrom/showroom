using System;
using System.Runtime.Serialization;

namespace Showroom.Application.Dtos
{
    [KnownType(typeof(AddClientProfileDto))]
    [KnownType(typeof(UpdateClientProfileDto))]
    public abstract class ClientProfileViewModelDto : UserProfileViewModelDto
    {
        public Guid ReferenceId { get; set; }
    }
}
