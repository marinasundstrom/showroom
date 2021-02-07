using System;
using System.ComponentModel.DataAnnotations;

namespace Showroom.Server.Models
{
    public class UserProfileLinkType
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
