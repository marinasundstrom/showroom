using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

using Newtonsoft.Json;

using NJsonSchema.Converters;

namespace Showroom.Server.Dtos
{
    [JsonConverter(typeof(JsonInheritanceConverter), "discriminator")]
    [KnownType(typeof(ClientProfileViewModelDto))]
    [KnownType(typeof(ConsultantProfileViewModelDto))]
    [KnownType(typeof(ManagerProfileViewModelDto))]
    [KnownType(typeof(UpdateUserProfileDto))]
    public abstract class UserProfileViewModelDto
    {
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

        public string OrganizationId { get; set; }

        public string Note { get; set; }
    }
}
