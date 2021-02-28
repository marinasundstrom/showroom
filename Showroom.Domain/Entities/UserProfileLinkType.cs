using System;
using System.ComponentModel.DataAnnotations;

namespace Showroom.Domain.Entities
{
    public class UserProfileLinkType
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
