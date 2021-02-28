using System;

namespace Showroom.Application.Common.Dtos
{
    public class ProfileShortDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string DisplayName { get; set; }

        public string Title { get; set; }

        public string Headline { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string ProfileImage { get; set; }
    }
}
