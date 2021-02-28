using System;
using System.Runtime.Serialization;
using Showroom.Application.Common.Dtos;

namespace Showroom.Application.Clients
{
    [KnownType(typeof(AddClientProfileDto))]
    [KnownType(typeof(UpdateClientProfileDto))]
    public abstract class ClientProfileViewModelDto : UserProfileViewModelDto
    {
        public Guid ReferenceId { get; set; }
    }
}
