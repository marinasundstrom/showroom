using System.ComponentModel.DataAnnotations;

namespace Showroom.Server.Dtos
{
    public class OrganizationDto
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public AddressDto Address { get; set; }
    }
}
