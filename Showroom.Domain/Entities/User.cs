﻿using System;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

namespace Showroom.Domain.Entities
{

    public class User : IdentityUser
    {
        [Required]
        public string Name { get; set; }

        public DateTime RegistrationDate { get; set; }

        public string RefreshToken { get; set; }

        public UserProfile Profile { get; set; }

        public Guid? ProfileId { get; set; }
    }
}
