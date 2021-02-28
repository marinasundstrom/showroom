using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Newtonsoft.Json;

namespace Showroom.Domain.Entities
{
    public class UserProfile
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

        public Address Address { get; set; }

        public ICollection<UserProfileLink> Links { get; set; }

        public string Company { get; set; }

        public string Department { get; set; }

        public string Section { get; set; }

        public Organization Organization { get; set; }

        public string OrganizationId { get; set; }

        [NotMapped]
        public string ProfileType => GetType().Name.Replace("Profile", string.Empty);

        public DateTime DateCreated { get; set; }

        //public User CreatedBy { get; set; }

        //[ForeignKey(nameof(CreatedBy))]
        //public string CreatedBydId { get; set; }

        public bool IsActive { get; set; } = true;

        public string Note { get; set; }

        public string Slug { get; set; }
    }
}
