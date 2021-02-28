using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

using Newtonsoft.Json;

using NJsonSchema.Converters;
using Showroom.Application.Clients;
using Showroom.Application.Consultants;
using Showroom.Application.Managers;

namespace Showroom.Application.Common.Dtos
{
    [JsonConverter(typeof(JsonInheritanceConverter), "discriminator")]
    [KnownType(typeof(ClientProfileDto))]
    [KnownType(typeof(ConsultantProfileDto))]
    [KnownType(typeof(ManagerProfileDto))]
    public class UserProfileDto
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string DisplayName { get; set; }

        public string Title { get; set; }

        public string ProfileImage { get; set; }

        public string ProfileVideo { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Phone]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public AddressDto Address { get; set; }

        public string Company { get; set; }

        public string Department { get; set; }

        public string Section { get; set; }

        public ProfileOrganizationDto Organization { get; set; }

        public string Note { get; set; }
    }
}
